using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class CG_LimitedStickTranslate : MonoBehaviour
{
    public Transform Stick;
    public Collider boundaryCollider;

    private float initialX;
    private float initialY;
    private float initialZ;
    private Vector3 halfExtents;


    private void Start()
    {
        initialX = transform.localPosition.x - 0.02f;
        initialY = transform.localPosition.y;
        initialZ = transform.localPosition.z;
        halfExtents = Stick.GetComponent<Renderer>().bounds.extents;

    }

    private void LateUpdate()
    {
        Vector3 localPosition = Stick.localPosition;

        localPosition.y = initialY;
        localPosition.z = initialZ;

        Vector3 worldPosition = Stick.parent.TransformPoint(localPosition);

        Bounds bounds = boundaryCollider.bounds;

        worldPosition.x = Mathf.Clamp(worldPosition.x, bounds.min.x, bounds.max.x);

        Stick.localPosition = Stick.parent.InverseTransformPoint(worldPosition);
    }
}
