using EasyTextEffects;
using EasyTextEffects.Effects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogueScript : MonoBehaviour
{
    //Player stuff
    public Transform player;
    public PlayerMovement playerScript;
    public bool stopMovementWhenTalking;

    //NPC and NPC Dialogue Stuff
    public TextAsset allDialogueText;
    public List<Dialogue> NPCDialogue = new List<Dialogue>();
    private bool playerIsHere = false;

    private int currentDialogueI = 0;
    private int currentDialogueLineI = 0;
    private string file;

    //Dialogue Text Box Stuff
    public GameObject dialogueTextBox;
    public TextMeshProUGUI dialogueText;
    public TextEffect typewriterInstance;

    //Typewriter
    public Effect_Color typewriterEffect;
    private bool typewriterEffectComplete = true;
    public float dialogueCooldownTime;
    private float startTime;

    //All code in the Start method gathers all the dialogue from the text file and assigns it to the NPC
    void Start()
    {
        /*
        //Convert text file to string
        file = allDialogueText.text;

        //Loop until there is no more dialgoue
        while (file.Length > 0) 
        {
            //Check if NPC name is correct
            if (transform.name.Trim() == file.Substring(0, file.IndexOf("\n")).Trim())
            {
                //Delete the name from the file
                file = file.Substring(file.IndexOf("\n") + 1);

                //Get Dialogue objects to add to the NPCDialogue
                while (file.IndexOf("~") != 0)
                {
                    //Create new Dialogue object and add lines to it
                    Dialogue newDialogue = new Dialogue();
                    while (file.IndexOf("~") > 1)
                    {
                        newDialogue.AddLine(file.Substring(0, file.IndexOf("\n")).Trim());
                        file = file.Substring(file.IndexOf("\n") + 1);
                    }
                    NPCDialogue.Add(newDialogue);
                    file = file.Substring(file.IndexOf("~") + 1);
                }
                file = file.Substring(file.IndexOf("~") + 1);
            }
            else
            {
                file = file.Substring(file.IndexOf("~~") + 2).Trim();
            }
        } 
        */
    }

    // Update is called once per frame
    void Update()
    {
        //Check if player is nearby, facing NPC, and clicks/presses E
        if (playerIsHere && PlayerFacingNPC() && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)))
        {
            //Disable Player Movement
            if (stopMovementWhenTalking)
            {
                playerScript.enabled = false;
                playerScript.PlayAnimation("idle" + playerScript.currentAnim.Substring(4));
                playerScript.move = new Vector3(0, 0, 0);
            }

            //Decide what is displayed in text box
            if (currentDialogueLineI >= NPCDialogue[currentDialogueI].NumLines() && typewriterEffectComplete)
            {
                //Reset dialogue
                dialogueTextBox.SetActive(false);
                currentDialogueLineI = 0;
                dialogueText.text = "";
                typewriterInstance.StopManualEffects();

                //Check if combat should start (Finish this when Blond Guy 3 has combat fully implemented)
                //StartCombat();

                //Enable Player Movement
                playerScript.enabled = true;
            }
            else
            {
                //Activate dialogue box
                typewriterInstance.Refresh();
                dialogueTextBox.SetActive(true);
                if (typewriterEffectComplete) //If the typewriter effect is already done/not active
                {
                    typewriterInstance.StopManualEffects();
                    startTime = Time.fixedTime;
                    dialogueText.text = NPCDialogue[currentDialogueI].GetLine(currentDialogueLineI);
                    typewriterInstance.Refresh();

                    typewriterInstance.StartManualEffects();
                    currentDialogueLineI++;
                    typewriterEffectComplete = false;
                }
                else //End typewriter effect
                {
                    typewriterEffectComplete = true;
                    typewriterInstance.StopManualEffects();
                }
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
        dialogueTextBox.SetActive(false);
        dialogueText.text = "";
        typewriterInstance.Refresh();
        currentDialogueLineI = 0;
        currentDialogueI = 0;
    }

    public void PrintAllDialogue()
    {
        foreach (Dialogue dialogue in NPCDialogue)
        {
            Debug.Log(dialogue.GetAllLines());
        }
    }

    //Returns true if the player is facing the NPC
    public bool PlayerFacingNPC()
    {
        Vector2 playerLocationRelatedToNPC = player.position - transform.position;

        //Check if next to NPC on horizontal
        if(Mathf.Abs(playerLocationRelatedToNPC.y) < 0.5)
        {
            //See if player is on left or right of NPC
            if (playerLocationRelatedToNPC.x < 0)
                return playerScript.currentFacing.ToLower() == "right";
            else
                return playerScript.currentFacing.ToLower() == "left";

        } else if (Mathf.Abs(playerLocationRelatedToNPC.x) < 0.5) //Check if next to NPC on vertical
        {
            //See if player is above or below NPC
            if (playerLocationRelatedToNPC.y < 0)
                return playerScript.currentFacing.ToLower() == "up";
            else
                return playerScript.currentFacing.ToLower() == "down";
        }

        //If all else fails return false
        return false;
    }

    public void SetTypewriterComplete()
    {
        typewriterEffectComplete = true;
    }
}
