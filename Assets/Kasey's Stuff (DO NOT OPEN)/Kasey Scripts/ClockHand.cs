using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;

public class ClockHand : MonoBehaviour
{
    public Vector3 pivotPoint;
    public float rotationSpeed;

    private float rotateDirection; //1 for counterclockwise, -1 for clockwise, 0 for no rotation
    private float rotationAngle = 180;

    public float goToAngle;

    public Tilemap tilemap;
    public List<Tile> bottom, left, right, top;

    void Update()
    {
        if ((int)rotationAngle == (int)goToAngle)
        {
            //Stop rotation and snap to the correct angle
            transform.RotateAround(pivotPoint, Vector3.forward, rotateDirection * Mathf.Abs(rotationAngle-goToAngle));
            rotateDirection = 0;

            //Fix colliders
            FixColliders(goToAngle);
            tilemap.RefreshAllTiles();
        }
        else
        {
            //Set rotation direction to something other than 0 if we need to start rotating
            if (rotateDirection == 0)
            {
                GetNewRotateDirection();

                //Set all colliders active while spinning
                ActivateAllColliders();
                tilemap.RefreshAllTiles();

            }
        }

        //Actually spin
        transform.RotateAround(pivotPoint, Vector3.forward, rotateDirection * rotationSpeed * Time.deltaTime);

        //Fix rotation angle so it doesn't go above 360 or below 0
        if (rotationAngle >= 360)
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

    public void FixColliders(float rotationAngle)
    {
        if(rotationAngle == 0 || rotationAngle == 360)
        {
            SetColliders(bottom, Tile.ColliderType.Sprite);
            SetColliders(left, Tile.ColliderType.Sprite);
            SetColliders(right, Tile.ColliderType.Sprite);
            SetColliders(top, Tile.ColliderType.None);
        }
        else if(rotationAngle == 90)
        {
            SetColliders(bottom, Tile.ColliderType.Sprite);
            SetColliders(left, Tile.ColliderType.Sprite);
            SetColliders(right, Tile.ColliderType.None);
            SetColliders(top, Tile.ColliderType.Sprite);
        }
        else if(rotationAngle == 180)
        {
            SetColliders(bottom, Tile.ColliderType.None);
            SetColliders(left, Tile.ColliderType.Sprite);
            SetColliders(right, Tile.ColliderType.Sprite);
            SetColliders(top, Tile.ColliderType.Sprite);
        }
        else if(rotationAngle == 270)
        {
            SetColliders(bottom, Tile.ColliderType.Sprite);
            SetColliders(left, Tile.ColliderType.None);
            SetColliders(right, Tile.ColliderType.Sprite);
            SetColliders(top, Tile.ColliderType.Sprite);
        }
    }

    public void SetColliders(List<Tile> tiles, Tile.ColliderType type)
    {
        foreach(Tile tile in tiles)
        {
            tile.colliderType = type;
        }
    }

    public void ActivateAllColliders()
    {
        foreach (Tile tile in bottom)
            tile.colliderType = Tile.ColliderType.Sprite;
        foreach (Tile tile in left)
            tile.colliderType = Tile.ColliderType.Sprite;
        foreach (Tile tile in right)
            tile.colliderType = Tile.ColliderType.Sprite;
        foreach (Tile tile in top)
            tile.colliderType = Tile.ColliderType.Sprite;
    }
}
