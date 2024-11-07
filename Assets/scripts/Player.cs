using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float jumpSpeed;
    [SerializeField]
    Rigidbody rd;
    [SerializeField]
    float rotateSpeed;
    [SerializeField]
    float shootRange = 100f;
    [SerializeField]
    Transform cameraTransform;
    [SerializeField]
    Camera playerCamera;
    [SerializeField]
    Text ammoText;
    [SerializeField]
    Text healthText;

    [SerializeField]
    GameObject wallPrefab;

    private List<Skill> skills = new List<Skill>();
    private DashSkill dashSkill;
    private VerticalDashSkill verticalDashSkill;
    private WallSkill wallSkill;

    [SerializeField]
    int maxAmmo = 10;
    private int currentAmmo;
    private bool isReloading = false;
    private float reloadTime = 2f;

    float verticalRotation = 0f;

    [SerializeField]
    float normalFOV = 60f;
    [SerializeField]
    float zoomFOV = 30f;

    [SerializeField]
    int maxHealth = 50;
    private int currentHealth;

    void Start()
    {
        rd.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        dashSkill = gameObject.AddComponent<DashSkill>();
        skills.Add(dashSkill);

        verticalDashSkill = gameObject.AddComponent<VerticalDashSkill>();
        skills.Add(verticalDashSkill);

        wallSkill = gameObject.AddComponent<WallSkill>();
        wallSkill.SetWallPrefab(wallPrefab);
        skills.Add(wallSkill);

        currentAmmo = maxAmmo;
        UpdateAmmoText();

        currentHealth = maxHealth;
        UpdateHealthText();

        playerCamera.fieldOfView = normalFOV;
    }

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += cameraTransform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection -= cameraTransform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection -= cameraTransform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += cameraTransform.right;
        }

        moveDirection.y = 0;
        transform.Translate(moveDirection.normalized * speed * Time.deltaTime, Space.World);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rd.AddForce(0, jumpSpeed, 0, ForceMode.Impulse);
        }

        float mouseX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        transform.Rotate(0, mouseX, 0);

        float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        if (Input.GetKeyDown(KeyCode.E) && moveDirection != Vector3.zero)
        {
            dashSkill.Activate(moveDirection.normalized);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            verticalDashSkill.Activate();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            wallSkill.Activate();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !isReloading)
        {
            if (currentAmmo > 1)
            {
                Shoot();
            }
            else
            {
                StartCoroutine(Reload());
            }
        }

        HandleZoom();
    }

    void HandleZoom()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            playerCamera.fieldOfView = zoomFOV;
        }
        else
        {
            playerCamera.fieldOfView = normalFOV;
        }
    }

    void Shoot()
    {
        currentAmmo--;
        UpdateAmmoText();
        Debug.Log("Balas restantes: " + currentAmmo);

        RaycastHit hit;
        Vector3 fwd = cameraTransform.forward;

        if (Physics.Raycast(cameraTransform.position, fwd, out hit, shootRange))
        {
            if (hit.collider != null && hit.collider.CompareTag("enemy"))
            {
                Debug.Log("Acertou o inimigo!");
                StartCoroutine(HitEffect(hit.collider.gameObject));
            }
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        UpdateAmmoText();
        Debug.Log("Recarregando...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
        UpdateAmmoText();
        Debug.Log("Recarga completa. Balas: " + currentAmmo);
    }

    private IEnumerator HitEffect(GameObject enemy)
    {
        Renderer enemyRenderer = enemy.GetComponent<Renderer>();
        if (enemyRenderer != null)
        {
            Color originalColor = enemyRenderer.material.color;
            enemyRenderer.material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            enemyRenderer.material.color = originalColor;
        }

        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeHit();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Vida Atual: " + currentHealth);
        UpdateHealthText();

        if (currentHealth <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Vida: " + currentHealth;
        }
    }

    void UpdateAmmoText()
    {
        if (isReloading)
        {
            ammoText.text = "Recarregando...";
        }
        else
        {
            ammoText.text = "Balas: " + currentAmmo;
        }
    }
}
