using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgoundMovement : MonoBehaviour
{
    public Transform target;
    public float ratio = 1.0f;

    public float downOffset;
    public float upOffset;
    public float downBorder;
    public float upBorder;

    private Vector2 offset = new Vector2 (0, 0);

    void Update ()
    {
        float diff = target.position.y - downBorder;
        if (diff < 0)
            diff = 0.0f;
        if (upBorder - target.position.y < 0)
            diff = upBorder - downBorder;
        float k = diff / (upBorder - downBorder);

        offset.y = upOffset * k + downOffset * (1.0f - k);
        offset.x = target.position.x * ratio;

        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
}
