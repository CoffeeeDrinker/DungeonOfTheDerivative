using EasyTextEffects.Editor.MyBoxCopy.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewFloorScript : MonoBehaviour
{
    public GameObject chooseFloorScreen;
    public Image background;
    public GameObject upButton, downButton;

    public Transform player;
    private bool playerIsHere = false;

    public Animator TransitionAnims;
    public GameObject transitionSprite;
    public Sprite lastInTransition;

    public int currentFloor = 1;
    public int nextFloor;

    public List<GameObject> floors;
    public List<Sprite> backgrounds;

    public Sprite grayUpArrow;
    public Sprite grayDownArrow;
    public Sprite redUpArrow;
    public Sprite redDownArrow;

    void Update()
    {
        //If player is nearby and the player clicks a button open the screen
        if (playerIsHere && Input.GetMouseButtonDown(0))
        {
            //Open the screen
            chooseFloorScreen.SetActive(true);
            player.gameObject.GetComponent<PlayerMovement>().enabled = false;

            //Make sure screen has the right buttons/text
            background.sprite = backgrounds[currentFloor];
            if (currentFloor == floors.Count - 1) //If we are on the top floor, disable up button
            {
                upButton.GetComponent<Image>().sprite = grayUpArrow;
                upButton.GetComponent<Button>().enabled = false;
            } else if(currentFloor == 0) //Ife we are on the botton floor, disable down button
            {
                downButton.GetComponent<Image>().sprite = grayDownArrow;
                downButton.GetComponent<Button>().enabled = false;
            }
            else //Make sure both buttons are enabled
            {
                upButton.GetComponent<Image>().sprite = redUpArrow;
                downButton.GetComponent<Image>().sprite = redDownArrow;
                upButton.SetActive(true);
                downButton.SetActive(true);
            }
        }

        //Transport player to new location when transition animation ends
        if (transitionSprite.GetComponent<SpriteRenderer>().sprite == lastInTransition)
        {
            //End Transition
            TransitionAnims.ResetTrigger("openTransition");
            TransitionAnims.SetTrigger("closeTransition");
            transitionSprite.GetComponent<SpriteRenderer>().sprite = null;

            //Enable player movement
            player.gameObject.GetComponent<PlayerMovement>().enabled = true;

            //Enable the next floor
            floors[currentFloor].SetActive(false);
            floors[nextFloor].SetActive(true);
            currentFloor = nextFloor;
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

    public void SendUpstairs()
    {
        nextFloor = currentFloor + 1;
        PlayTransition();
    }

    public void SendDownstairs()
    {
        nextFloor = currentFloor - 1;
        PlayTransition();
    }

    public void CloseChooseFloorScreen()
    {
        chooseFloorScreen.SetActive(false);
        player.gameObject.GetComponent<PlayerMovement>().enabled = true;
    }

    public void PlayTransition()
    {
        chooseFloorScreen.SetActive(false);
        TransitionAnims.ResetTrigger("closeTransition");
        TransitionAnims.SetTrigger("openTransition");
    }
}
