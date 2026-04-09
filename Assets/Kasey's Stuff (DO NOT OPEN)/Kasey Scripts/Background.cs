using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private Vector2 startPos;
    public float parallaxEffect; //0 to 1
    public GameObject cam;
    private Vector2 moveDist;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        moveDist.x = cam.transform.position.x * parallaxEffect;
        moveDist.y = cam.transform.position.y * parallaxEffect;
        transform.position = new Vector2(startPos.x + moveDist.x, startPos.y + moveDist.y);
    }
}