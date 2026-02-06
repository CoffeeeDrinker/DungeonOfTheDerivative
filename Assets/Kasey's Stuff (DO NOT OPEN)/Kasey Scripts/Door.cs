using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    public string doorType;

    public Transform player;
    public Vector2 newLocation;
    public string newScene;
    private bool playerIsHere = false;

    public Animator TransitionAnims;
    public GameObject transitionSprite;

    public List<GameObject> newTiles;
    public List<GameObject> oldTiles;

    public float percentTransparent;
    private Color transparentColor;

    private bool changedLayer = false;

    void Start()
    {
        transparentColor = new Color(1f, 1f, 1f, percentTransparent);
    }

    void Update()
    {
        if (doorType == "click" || doorType == "")
        {
            //If player is nearby and the player clicks a button teleport them to new area
            if (playerIsHere && Input.GetMouseButtonDown(0))
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
        } else if(doorType == "noclick")
        {
            //If player is nearby teleport them to new area
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
        } else if (doorType == "layerChange")
        {
            //If player is nearby, change the layer they are on (make certain areas semi-transparent)
            if (playerIsHere && !changedLayer && transitionSprite.GetComponent<SpriteRenderer>().sprite == null)
            {
                changedLayer = true;
                ChangeLayer();
            }
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

    public void ChangeLayer()
    {
        for (int i = 0; i < oldTiles.Count; i++)
        {
            for(int j = 0; j < oldTiles[i].transform.childCount; j++)
            {
                try
                {
                    oldTiles[0].transform.GetChild(0).GetComponent<Tilemap>().color = transparentColor;
                }
                catch { }
            }
        }

        for (int i = 0; i < newTiles.Count; i++)
        {
            newTiles[i].SetActive(true);
            for (int j = 0; j < oldTiles[i].transform.childCount; j++)
            {
                try
                {
                    newTiles[i].transform.GetChild(j).GetComponent<Tilemap>().color = Color.white;
                }
                catch { }
            }
        }
    }
}
