using EasyTextEffects;
using EasyTextEffects.Effects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogueScript : MonoBehaviour
{
    //Player stuff
    public Transform player;
    public PlayerMovement playerScript;
    public bool stopMovementWhenTalking;
    public bool startCombatPostDialogue;

    //NPC and NPC Dialogue Stuff
    public List<Dialogue> NPCDialogue = new List<Dialogue>();
    public bool playerIsHere = false;

    private int currentDialogueI = 0;
    private int currentDialogueLineI = 0;

    //Dialogue Text Box Stuff
    public GameObject dialogueTextBox;
    public TextMeshProUGUI dialogueText;
    public TextEffect typewriterInstance;

    //Typewriter
    public Effect_Color typewriterEffect;
    private bool typewriterEffectComplete = true;
    public float dialogueCooldownTime;
    private float startTime;

    //Managers
    public MathProblemManager problemManager;
    public CombatManager combatManager;

    public bool talkNow = false;

    void Update()
    {
        if (talkNow)
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

                //Check if combat should start
                if (startCombatPostDialogue)
                    combatManager.StartCombat(gameObject.GetComponent<NPCDialogueScript>());
                else
                    playerScript.enabled = true;
            }
            else
            {
                if (!problemManager.drawscreenStuff.activeSelf && !combatManager.combatSystem.activeSelf)
                {
                    //Activate dialogue box
                    typewriterInstance.Refresh();
                    dialogueTextBox.SetActive(true);
                    if (typewriterEffectComplete) //If the typewriter effect is already done/not active
                    {
                        typewriterInstance.StopManualEffects();
                        startTime = Time.fixedTime;
                        AddDialogueToTextBox();
                        //dialogueText.text = NPCDialogue[currentDialogueI].GetLine(currentDialogueLineI);
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

        if(player.position.y > this.transform.position.y)
        {
            this.GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder - 1;
        }

        //Check if player is nearby, facing NPC, and clicks/presses E
        talkNow = playerIsHere && PlayerFacingNPC() && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0));
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
        //currentDialogueI = 0;
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

    public void SetDialogueI(int i)
    {
        currentDialogueI = i;
    }

    public void DefeatDialogue()
    {
        startCombatPostDialogue = false;
        dialogueText.text = "";

        /*
        //Activate dialogue box
        typewriterInstance.Refresh();
        dialogueTextBox.SetActive(true);

        //Set current dialogue object
        currentDialogueI = 1;
        currentDialogueLineI = 0;
        dialogueText.text = NPCDialogue[currentDialogueI].GetLine(currentDialogueLineI);
        typewriterInstance.Refresh();

        typewriterEffectComplete = false;
        startTime = Time.fixedTime;
        typewriterInstance.StartManualEffects();
        */
        currentDialogueI = 1;
        currentDialogueLineI = 0;

        talkNow = true;
    }

    public void AddDialogueToTextBox()
    {
        //Get the line
        string line = NPCDialogue[currentDialogueI].GetLine(currentDialogueLineI);

        //Replace <player_name> with player name if needed
        if (line.IndexOf("<player_name>") > -1 && playerScript.playerName.Length > 0)
        {
            line = line.Substring(0, line.IndexOf("<player_name>")) + playerScript.playerName + line.Substring(line.IndexOf(">")+1);
        }

        //If pedro is saying this, then make the text red
        if (line.IndexOf("<pedro_is_speaking>") > -1)
        {
            line = line.Substring(line.IndexOf(">")+1);
            dialogueText.color = Color.red;
        }
        else
        {
            dialogueText.color = Color.black;
        }

        //Actually put the dialogue in the text box thingy
        dialogueText.text = line;
    }
}


/*
Kasey
I regret my life decisions. For some stupid reason I decided to take 3 math classes this year and I am suffering. Help me.
does this work?
Hi there <player_name>. bla bla bla
~
That didn't help. :(
~~
*/