using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 50f; // Velocidad de movimiento
    public float lookSpeed = 2f; // Velocidad de rotación
    public float verticalLookLimit = 80f; // Límite de rotación vertical (en grados)

    private float pitch = 0f; // Control del ángulo vertical manualmente

    void Start()
    {
        this.enabled = false;
    }

    void Update()
    {
        // Movimiento con WASD
        float moveX = Input.GetAxis("Horizontal"); // A/D
        float moveZ = Input.GetAxis("Vertical");   // W/S
        float moveY = 0f;

        // Movimiento vertical (subir/bajar con Q/E)
        if (Input.GetKey(KeyCode.E)) moveY = 1f;
        if (Input.GetKey(KeyCode.Q)) moveY = -1f;

        Vector3 move = transform.right * moveX + transform.forward * moveZ + transform.up * moveY;
        transform.position += move * moveSpeed * Time.deltaTime;

        // Rotación de la cámara con el mouse
        if (Time.timeScale != 0f)
        {
            float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

            // Rotar horizontalmente
            transform.Rotate(Vector3.up * mouseX, Space.World);

            // Rotar verticalmente con límites
            pitch -= mouseY; // Reducir el ángulo al mover el mouse hacia arriba
            pitch = Mathf.Clamp(pitch, -verticalLookLimit, verticalLookLimit); // Limitar entre el rango permitido

            // Aplicar la rotación vertical
            transform.localRotation = Quaternion.Euler(pitch, transform.localEulerAngles.y, 0f);
        }
    }

    public void StartSimulation()
    {
        this.enabled = true;
        // Bloquear y ocultar el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
