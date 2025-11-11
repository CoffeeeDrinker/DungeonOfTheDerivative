using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoClickDoor : MonoBehaviour
{
    public Transform player;
    public Vector2 newLocation;
    public string newScene;
    private bool playerIsHere = false;

    public Animator TransitionAnims;
    public GameObject transitionSprite;

    public List<GameObject> newTiles;
    public List<GameObject> oldTiles;

    void Update()
    {
        //If player is nearby and the player clicks a button teleport them to new area
        if (playerIsHere && transitionSprite.GetComponent<SpriteRenderer>().sprite == null)
        {
            //If we need to open a new scene the user will go to that
            if (newScene != null && newScene != "")
            {
                SceneManager.LoadScene(newScene);
            }
            player.gameObject.GetComponent<PlayerMovement>().enabled = false;

            //Play Transition Animation
            TransitionAnims.ResetTrigger("closeTransition");
            TransitionAnims.SetTrigger("openTransition");
        }

        //Transport player to new location
        if (playerIsHere && TransitionAnims.GetCurrentAnimatorStateInfo(0).IsName("Transition") && TransitionAnims.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {
            NewHall();
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

    public void EnableNewLocation()
    {
        for (int i = 0; i < oldTiles.Count; i++)
        {
            oldTiles[i].SetActive(false);
        }

        for (int i = 0; i < newTiles.Count; i++)
        {
            newTiles[i].SetActive(true);
        }
    }

    public void NewHall()
    {
        if (playerIsHere)
        {
            player.position = newLocation;

            TransitionAnims.ResetTrigger("openTransition");
            TransitionAnims.SetTrigger("closeTransition");
            transitionSprite.GetComponent<SpriteRenderer>().sprite = null;

            player.gameObject.GetComponent<PlayerMovement>().enabled = true;
            EnableNewLocation();
        }
    }
}
