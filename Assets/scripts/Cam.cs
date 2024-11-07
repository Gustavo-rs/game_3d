using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 2f;
    public float cameraDistance = 5f;
    float cameraVerticalRotation = 0f;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        player.Rotate(Vector3.up * inputX);

        Vector3 desiredCameraPosition = player.position - transform.forward * cameraDistance;

        RaycastHit hit;
        if (Physics.Linecast(player.position, desiredCameraPosition, out hit))
        {
            transform.position = hit.point + transform.forward * 0.1f;
        }
        else
        {
            transform.position = desiredCameraPosition;
        }
    }
}
