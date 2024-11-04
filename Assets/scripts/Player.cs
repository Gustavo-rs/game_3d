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
    Transform cameraTransform; // Refer�ncia para o Transform da c�mera

    float verticalRotation = 0f;

    void Start()
    {
        rd.freezeRotation = true;
    }

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        // Captura o input de movimento
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += cameraTransform.forward; // Movimento para frente com base na c�mera
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection -= cameraTransform.forward; // Movimento para tr�s com base na c�mera
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection -= cameraTransform.right; // Movimento para a esquerda com base na c�mera
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += cameraTransform.right; // Movimento para a direita com base na c�mera
        }

        // Ignora o movimento vertical da c�mera para evitar que o jogador se mova para cima ou para baixo
        moveDirection.y = 0;
        transform.Translate(moveDirection.normalized * speed * Time.deltaTime, Space.World);

        // Comando de pulo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rd.AddForce(0, jumpSpeed, 0, ForceMode.Impulse);
        }

        // Rota��o Horizontal do Player (alinhado com a c�mera)
        float mouseX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        transform.Rotate(0, mouseX, 0);

        // Rota��o Vertical da C�mera
        float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        // Movimento r�pido ao pressionar V
        if (Input.GetKeyDown(KeyCode.V))
        {
            transform.Translate(moveDirection.normalized * 10, Space.World);
        }

        // Simula��o de tiro
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Vector3 fwd = cameraTransform.forward; // Tiro segue a dire��o da c�mera

            if (Physics.Raycast(cameraTransform.position, fwd, out hit, shootRange))
            {
                if (hit.collider != null && hit.collider.CompareTag("enemy"))
                {
                    Debug.Log("Bateu no inimigo!");
                }
            }
        }
    }
}
