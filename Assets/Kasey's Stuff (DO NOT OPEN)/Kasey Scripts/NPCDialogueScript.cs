using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogueScript : MonoBehaviour
{
    //Player stuff
    public Transform player;
    public PlayerMovement playerScript;

    //NPC and NPC Dialogue Stuff
    //public string NPCName;
    public TextAsset allDialogueText;
    public List<Dialogue> NPCDialogue = new List<Dialogue>();
    private bool playerIsHere = false;

    private int currentDialogueI = 0;
    private int currentDialogueLineI = 0;

    //Dialogue Text Box Stuff
    public GameObject dialogueTextBox;
    public Text dialogueText;

    //All code in the Start method gathers all the dialogue from the text file and assigns it to the NPC
    void Start()
    {
        //Convert text file to string
        string file = allDialogueText.text;

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
            else //If the name isn't correct skip this person
            {
                
            }
            file = "";
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsHere && PlayerFacingNPC() && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)))
        {
            dialogueTextBox.SetActive(true);
            dialogueText.text = "";
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
}
