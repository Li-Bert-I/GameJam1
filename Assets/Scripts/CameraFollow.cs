using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 baseOffset;
    public Vector3 downOffset;
    public Vector3 upOffset;
    public float downBorder;
    public float upBorder;

    void FixedUpdate()
    {
        float diff = target.position.y - downBorder;
        if (diff < 0)
            diff = 0.0f;
        if (upBorder - target.position.y < 0)
            diff = upBorder - downBorder;
        float k = diff / (upBorder - downBorder);
        Vector3 desiredPosition = target.position + baseOffset + upOffset * k + downOffset * (1.0f - k);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

    }
}
