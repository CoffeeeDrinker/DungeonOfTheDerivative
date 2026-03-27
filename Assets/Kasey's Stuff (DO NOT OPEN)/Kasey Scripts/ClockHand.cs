using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ClockHand : MonoBehaviour
{
    public Vector3 pivotPoint;
    public float rotationSpeed;

    public float rotateDirection; //1 for counterclockwise, -1 for clockwise, 0 for no rotation

    void Update()
    {
        transform.RotateAround(pivotPoint, Vector3.forward, rotateDirection * rotationSpeed * Time.deltaTime);
    }
}
