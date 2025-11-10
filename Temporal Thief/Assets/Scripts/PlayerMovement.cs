using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f; // walk speed
    public float gravity = -9.81f; // gravity pull
    public float jumpHeight = 1f; // jump strength

    [Header("Mouse Look Settings")]
    public float mouseSensitivity = 100f; // how fast we turn
    public Transform cameraTransform; // drag your Camera here

    private CharacterController controller;
    private Vector3 velocity; // for gravity
    private bool isGrounded;
    private float xRotation = 0f; // camera up/down rotation

    void Start()
    {
        controller = GetComponent<CharacterController>(); // grab capsule controller
        Cursor.lockState = CursorLockMode.Locked; // lock cursor to center
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }

    void HandleMovement()
    {
        // check ground
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // input (WASD)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // move relative to player forward
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // rotate camera up/down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // rotate player left/right
        transform.Rotate(Vector3.up * mouseX);
    }
}
