using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    float mouseX;
    float mouseY;

    float xRotation = 0f;
    float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;    //Si fuese sumando, lo haría al revés.
        xRotation = Mathf.Clamp(xRotation, -30f, 10f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -35f, 35f);
        playerBody.rotation = Quaternion.Euler(0f, yRotation, 0f);

        //playerBody.Rotate(Vector3.up * mouseX); OTRA FORMA
    }
}
