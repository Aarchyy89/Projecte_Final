using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    private Vector2 _delta;

    private bool _isMoving;
    private bool _isRotating;

    private float _xRotation;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private Camera CameraZoom;
    [SerializeField] private float sensitivity;

    private void Awake()
    {
        _xRotation = transform.rotation.eulerAngles.x;
        CameraZoom = Camera.main;
    }

    public void OnLook (InputAction.CallbackContext context)
    {
        _delta = context.ReadValue<Vector2>();
    }

    public void OnMove (InputAction.CallbackContext context)
    {
        _isMoving = context.started || context.performed;
    }
    
    public void OnRotate (InputAction.CallbackContext context)
    {
        _isRotating = context.started || context.performed;
    }

    private void LateUpdate()
    {
        if (_isMoving)
        {
            var position = transform.right * (_delta.x * -movementSpeed);
            position += transform.up * (_delta.y * -movementSpeed);
            transform.position += position * Time.deltaTime;
        }

        if (_isRotating)
        {
            transform.Rotate(new Vector3(_xRotation, -_delta.x * rotationSpeed, 0.0f));
            transform.rotation = Quaternion.Euler(_xRotation, transform.rotation.eulerAngles.y, 0.0f);
        }

        if (CameraZoom.orthographicSize > 1 && CameraZoom.orthographicSize < 7)
        {
            CameraZoom.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        }
        else if (CameraZoom.orthographicSize > 1)
        {
            CameraZoom.orthographicSize = 6.99f;
        }      
        else if (CameraZoom.orthographicSize < 7)
        {
            CameraZoom.orthographicSize = 1.01f;
        }
    }
}
