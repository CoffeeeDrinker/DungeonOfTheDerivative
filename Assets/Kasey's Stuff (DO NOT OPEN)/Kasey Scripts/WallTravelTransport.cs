using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkableTile : MonoBehaviour
{
    public GameObject player;

    private PlayerMovement playerScript;
    private bool move;
    //private bool moveToWall;
    //private bool moveToGround;

    public Vector3 wallTilePosition;
    public Vector3 groundTilePosition;

    public GameObject wallWalkTilemap;
    public GameObject groundWalkTilemap;

    public float moveSpeed;

    private void Start()
    {
        playerScript = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if(groundTilePosition == Vector3.zero)
        {
            if (move)
            {
                //Freeze player movement and animation
                FreezePlayer();

                //Change tilemap colliders
                groundWalkTilemap.GetComponent<TilemapCollider2D>().enabled = false;

                //Move player towards wall tile
                player.transform.position = Vector3.MoveTowards(player.transform.position, wallTilePosition, moveSpeed * Time.deltaTime);
            }

            if (wallTilePosition == player.transform.position)
            {
                move = false;

                //Fix tilemap colliders
                groundWalkTilemap.GetComponent<TilemapCollider2D>().enabled = false;
                wallWalkTilemap.GetComponent<TilemapCollider2D>().enabled = true;

                //Re-enable player movement and animation
                UnFreezePlayer();
            }
        }
        else
        {
            if (move)
            {
                //Freeze player movement and animation
                FreezePlayer();

                //Change tilemap colliders
                wallWalkTilemap.GetComponent<TilemapCollider2D>().enabled = false;

                //Move player towards ground tile
                player.transform.position = Vector3.MoveTowards(player.transform.position, groundTilePosition, moveSpeed * Time.deltaTime);
            }

            if (groundTilePosition == player.transform.position)
            {
                move = false;

                //Fix tilemap colliders
                groundWalkTilemap.GetComponent<TilemapCollider2D>().enabled = true;
                wallWalkTilemap.GetComponent<TilemapCollider2D>().enabled = false;

                //Re-enable player movement and animation
                UnFreezePlayer();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        move = col.tag == "TeleportCollider";
        //moveToGround = col.tag == "TeleportCollider";
    }

    public void FreezePlayer()
    {
        playerScript.PlayAnimation("idleDown");
        playerScript.move = Vector3.zero;
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        player.GetComponent<Collider2D>().enabled = false;
        playerScript.enabled = false;
    }

    public void UnFreezePlayer()
    {
        player.GetComponent<Collider2D>().enabled = true;
        playerScript.enabled = true;
    }
}
