using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public Vector2 velocity;
    public float stopTime;

    private float timer;
    private GameObject surroundingColliders;

    private

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = velocity;
        surroundingColliders = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if(rigidBody.velocity == Vector2.zero)
        {
            timer += Time.deltaTime;
            if(timer >= stopTime)
            {
                //Enable colliders to stop player from falling off platform
                surroundingColliders.SetActive(true);

                timer = 0;
                velocity.x *= -1;
                velocity.y *= -1;
                rigidBody.velocity = velocity;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "TeleportCollider")
        {
            if (surroundingColliders.GetComponent<Collider2D>().IsTouching(collision) && transform.GetComponent<Collider2D>().IsTouching(collision))
            {
                //Stop platform for a moment
                rigidBody.velocity = Vector2.zero;

                //Disable colliders around platform
                surroundingColliders.SetActive(false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Make Player move with platform
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().MoveWithPlatform(rigidBody.velocity);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Stop Player from moving with platform
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().StopMovingWithPlatform();
        }
    }
}


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public Vector2 velocity;
    public float stopTime;

    private float timer;
    private List<Collider2D> surroundingColliders = new List<Collider2D>();

    private

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = velocity;

        foreach (Collider2D collider in transform.GetChild(0).GetComponents<Collider2D>())
        {
            surroundingColliders.Add(collider);
        }
    }

    private void Update()
    {
        if(rigidBody.velocity == Vector2.zero)
        {
            timer += Time.deltaTime;
            if(timer >= stopTime)
            {
                timer = 0;
                velocity.x *= -1;
                velocity.y *= -1;
                rigidBody.velocity = velocity;

                //Enable colliders to stop player from falling off platform
                foreach (Collider2D collider in surroundingColliders)
                {
                    collider.enabled = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Change direction of platform
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "TeleportCollider")
        {
            //Stop platform for a moment
            rigidBody.velocity = Vector2.zero;

            //Disable colliders around platform
        }
        else if (collision.gameObject.tag == "Player") Debug.Log("Player on platform");
        {
            //Disable colliders to allow player to leave
            foreach (Collider2D collider in surroundingColliders)
            {
                collider.enabled = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Make Player move with platform
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().MoveWithPlatform(rigidBody.velocity);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Stop Player from moving with platform
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().StopMovingWithPlatform();
        }
    }
}
*/