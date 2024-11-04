using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private List<Skill> skills = new List<Skill>();
    private DashSkill dashSkill;

    float verticalRotation = 0f;

    void Start()
    {
        rd.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        dashSkill = gameObject.AddComponent<DashSkill>();
        skills.Add(dashSkill);
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

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
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
    }
}
