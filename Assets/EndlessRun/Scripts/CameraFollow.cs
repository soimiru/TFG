using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    private float smoothSpeed = 0.2f;
    private Vector3 offset = new Vector3(0f, -1f, -10f);

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }


}
