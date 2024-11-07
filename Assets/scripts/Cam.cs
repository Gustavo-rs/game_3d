using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    // Vari�veis
    public Transform player;
    public float mouseSensitivity = 2f;
    public float cameraDistance = 5f; // Dist�ncia inicial da c�mera ao player
    float cameraVerticalRotation = 0f;

    void Start()
    {
        // Trava e oculta o cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Coleta entrada do mouse
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotaciona a c�mera em torno do seu eixo X local
        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        // Rotaciona o objeto do jogador e a c�mera em torno do eixo Y
        player.Rotate(Vector3.up * inputX);

        // Calcula a posi��o desejada da c�mera
        Vector3 desiredCameraPosition = player.position - transform.forward * cameraDistance;

        // Detecta colis�es entre o jogador e a posi��o desejada da c�mera
        RaycastHit hit;
        if (Physics.Linecast(player.position, desiredCameraPosition, out hit))
        {
            // Ajusta a posi��o da c�mera para o ponto de colis�o, evitando ultrapassar a parede
            transform.position = hit.point + transform.forward * 0.1f; // Ajuste pequeno para n�o sobrepor o colisor
        }
        else
        {
            // Define a posi��o da c�mera para a dist�ncia desejada se n�o houver colis�o
            transform.position = desiredCameraPosition;
        }
    }
}
