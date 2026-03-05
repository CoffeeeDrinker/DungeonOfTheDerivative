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
    public List<GameObject> reallyOldTiles;

    public float percentTransparent;
    private Color transparentColor;
    private Color completelyTransparent;

    private bool changedLayer = false;

    void Start()
    {
        transparentColor = new Color(1f, 1f, 1f, percentTransparent);
        completelyTransparent = new Color(1f, 1f, 1f, 0f);
        /*
        if (!gameObject.GetComponent<BoxCollider2D>().enabled)
        {
            changedLayer = true;
        } */
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
            //If the collider isn't active, then the layer shouldn't change
            /*
            if (!gameObject.GetComponent<BoxCollider2D>().enabled)
            {
                changedLayer = true;
            } */

            //If player is nearby, change the layer they are on (make certain areas semi-transparent)
            if (playerIsHere && !changedLayer && transitionSprite.GetComponent<SpriteRenderer>().sprite == null)
            {
                changedLayer = true;
                ChangeLayer();
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    public void SetChangedLayer(bool a)
    {
        changedLayer = a;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        playerIsHere = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        changedLayer = false;
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
            for (int j = 0; j < oldTiles[i].transform.childCount; j++)
            {
                if (oldTiles[i].transform.GetChild(j).GetComponent<Tilemap>() != null)
                {
                    oldTiles[i].transform.GetChild(j).GetComponent<Tilemap>().color = transparentColor;
                    oldTiles[i].transform.GetChild(j).GetComponent<TilemapCollider2D>().enabled = false;
                } else if(oldTiles[i].transform.GetChild(j).name == "AdditionalColliders")
                {
                    oldTiles[i].transform.GetChild(j).gameObject.SetActive(false);
                }
            }
        }

        for (int i = 0; i < newTiles.Count; i++)
        {
            for (int j = 0; j < newTiles[i].transform.childCount; j++)
            {
                if (newTiles[i].transform.GetChild(j).GetComponent<Tilemap>() != null)
                {
                    newTiles[i].transform.GetChild(j).GetComponent<Tilemap>().color = Color.white;
                    newTiles[i].transform.GetChild(j).GetComponent<TilemapCollider2D>().enabled = true;
                }
                else if (newTiles[i].transform.GetChild(j).name == "AdditionalColliders")
                {
                    newTiles[i].transform.GetChild(j).gameObject.SetActive(true);
                }
                else if(newTiles[i].transform.GetChild(j).GetComponent<BoxCollider2D>() != null)
                {
                    newTiles[i].transform.GetChild(j).GetComponent<BoxCollider2D>().enabled = true;
                    newTiles[i].transform.GetChild(j).GetComponent<Door>().SetChangedLayer(false);
                }
            }
        }

        for (int i = 0; i < reallyOldTiles.Count; i++)
        {
            for (int j = 0; j < reallyOldTiles[i].transform.childCount; j++)
            {
                try
                {
                    reallyOldTiles[i].transform.GetChild(j).GetComponent<Tilemap>().color = completelyTransparent;
                    reallyOldTiles[i].transform.GetChild(j).GetComponent<TilemapCollider2D>().enabled = false;
                }
                catch { }
            }
        }
    }
}
