using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("--- Player Components ---")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform feetTransform;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private LayerMask floorMask;

    [Space]
    [Header("--- Player Attributes ---")]
    [SerializeField] private float speed;
    [SerializeField] private float sensitivity;
    [SerializeField] private float jumpForce;

    // Private Attributes
    private Vector3 playerMovementInput;
    private Vector2 playerMouseInput;
    private float xRot;


    void Update()
    {
        // Capturamos los inputs del jugador y los almacenamos en las Variables de tipo Vector3 y Vector2
        playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MovePlayerCamera();
    }


    /// <summary>
    /// M�todo para mover al jugador hacia la direcci�n que est� pulsado con el teclado
    /// </summary>
    private void MovePlayer()
    {
        Vector3 moveVector = transform.TransformDirection(playerMovementInput) * speed;
        _rigidbody.velocity = new Vector3(moveVector.x, _rigidbody.velocity.y, moveVector.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Comprobamos que el transform que simula nuestros pies est� chocando con la layerMask que le indicamos
            if (Physics.CheckSphere(feetTransform.position, 0.1f, floorMask))
            {
                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Aplicamos un impulso hacia arriba
            }
        }
    }

    /// <summary>
    /// M�todo para rotar al player y la c�mara
    /// </summary>
    private void MovePlayerCamera()
    {
        xRot -= playerMouseInput.y * sensitivity;
        xRot = Math.Clamp(xRot, -60f, 60f); // Definimos la m�xima rotaci�n de la c�mara hacia arriba y abajo

        transform.Rotate(0f, playerMouseInput.x * sensitivity, 0f); // Rotamos al jugador

        playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f); // Rotamos a la c�mara
    }

}
