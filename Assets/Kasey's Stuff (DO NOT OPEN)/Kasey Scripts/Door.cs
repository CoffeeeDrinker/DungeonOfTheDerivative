using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public Transform player;
    public Vector2 newLocation;
    public string newScene;
    private bool playerIsHere = false;

    void Update()
    {
        //If player is nearby and the player clicks a button teleport them to new area
        if (playerIsHere && Input.GetMouseButtonDown(0))
        {
            //If we need to open a new scene the user will go to that
            if (newScene != null && newScene != "")
            {
                SceneManager.LoadScene(newScene);
            }

            //Transport player to new location
            player.position = newLocation;
            playerIsHere = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        playerIsHere = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        playerIsHere = false;
    }
}
