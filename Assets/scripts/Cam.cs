using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    // Variáveis
    public Transform player;
    public float mouseSensitivity = 2f;
    public float cameraDistance = 5f; // Distância inicial da câmera ao player
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

        // Rotaciona a câmera em torno do seu eixo X local
        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        // Rotaciona o objeto do jogador e a câmera em torno do eixo Y
        player.Rotate(Vector3.up * inputX);

        // Calcula a posição desejada da câmera
        Vector3 desiredCameraPosition = player.position - transform.forward * cameraDistance;

        // Detecta colisões entre o jogador e a posição desejada da câmera
        RaycastHit hit;
        if (Physics.Linecast(player.position, desiredCameraPosition, out hit))
        {
            // Ajusta a posição da câmera para o ponto de colisão, evitando ultrapassar a parede
            transform.position = hit.point + transform.forward * 0.1f; // Ajuste pequeno para não sobrepor o colisor
        }
        else
        {
            // Define a posição da câmera para a distância desejada se não houver colisão
            transform.position = desiredCameraPosition;
        }
    }
}
