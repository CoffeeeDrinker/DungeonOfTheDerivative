using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ClockHand : MonoBehaviour
{
    public Vector3 pivotPoint;
    public float rotationSpeed;

    private float rotateDirection; //1 for counterclockwise, -1 for clockwise, 0 for no rotation
    private float rotationAngle = 180;

    public float goToAngle;

    void Update()
    {
        if ((int)rotationAngle == (int)goToAngle)
        {
            transform.RotateAround(pivotPoint, Vector3.forward, rotateDirection * Mathf.Abs(rotationAngle-goToAngle));
            rotateDirection = 0;
        }
        else
        {
            if(rotateDirection == 0)
            {
                GetNewRotateDirection();
            }
        }

        transform.RotateAround(pivotPoint, Vector3.forward, rotateDirection * rotationSpeed * Time.deltaTime);

        if(rotationAngle >= 360)
        {
            rotationAngle -= 360;
        } else if (rotationAngle <= 0)
        {
            rotationAngle += 360;
        }
        rotationAngle -= rotateDirection * rotationSpeed * Time.deltaTime;
    }

    private void GetNewRotateDirection()
    {
        float angle = goToAngle - rotationAngle;
        if(angle < 0)
        {
            angle += 360;
        }

        if(angle-rotationAngle < 0)
        {
            rotateDirection = -1;
        }
        else
        {
            rotateDirection = 1;
        }

        
    }
}
