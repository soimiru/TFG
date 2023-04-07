using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    private float smoothSpeed = 0.2f;
    private Vector3 offset = new Vector3(0f, -0.25f, -10f);

    /// <summary>
    /// Sirve para que la cámara siga al target seleccionado.
    /// </summary>
    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }


}
