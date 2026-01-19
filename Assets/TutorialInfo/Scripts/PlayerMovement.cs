using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller; // Kéo Character Controller vào đây ở Inspector
    public float speed = 12f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 100f;

    Vector3 velocity;
    float xRotation = 0f;

    void Start()
    {
        // Khóa chuột vào giữa màn hình để xoay camera dễ hơn
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 1. Xoay Camera bằng chuột
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // 2. Di chuyển bằng phím WASD
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // 3. Xử lý trọng lực (Rơi xuống đất)
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}