using EasyTextEffects;
using EasyTextEffects.Effects;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class Cutscene : MonoBehaviour
{
    public float walkSpeed;

    private bool playerIsHere = false;
    private GameObject player;
    private PlayerMovement playerScript;

    public string cutsceneName;
    public List<CutsceneAction> cutsceneActions = new List<CutsceneAction>();
    public List<GameObject> NPCs;

    private int i = 0;
    private bool cutsceneStarted = false;
    private CutsceneAction currentAction;

    //Dialogue
    public GameObject dialogueTextBox;
    public TextMeshProUGUI dialogueText;
    public TextEffect typewriterInstance;
    
    private int currentDialogueLineI = 0;

    //Typewriter
    public Effect_Color typewriterEffect;
    private bool typewriterEffectComplete = true;
    public float dialogueCooldownTime;
    private float startTime;

    private bool talkNow = true;   
    private bool dialogueDone = false;
    private bool animStarted = false;
    private string currentAnim = "idleDown";

    void Start()
    {
        //cutsceneActions.Add(new CutsceneAction(NPCs[0], new Vector2(-1f, -1.5f), null));
        //cutsceneActions.Add(new CutsceneAction(NPCs[0], Vector2.zero, new Dialogue(new List<string>() {"Hello I am Pedro.", "I used to be a girl scout.", "I will now walk away." })));
        //cutsceneActions.Add(new CutsceneAction(NPCs[0], new Vector2(-8f, -1.5f), null));
    }

    void Update()
    {
        if(playerIsHere && !cutsceneStarted) //Start the cutscene
        {
            //Freeze player movement and animation
            playerScript.PlayAnimation("idle" + playerScript.currentAnim.Substring(4));
            playerScript.move = Vector3.zero;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<Collider2D>().enabled = false;
            playerScript.enabled = false;

            //Summon the NPCs
            foreach(GameObject NPC in NPCs)
            {
                NPC.SetActive(true);
            }

            cutsceneStarted = true;

        } else if (playerIsHere && cutsceneStarted)
        {
            currentAction = cutsceneActions[i];
            if (currentAction.position != Vector2.zero && currentAction.position != null)
            {
                //Start walk animation if not already started
                if (!animStarted)
                {
                    if (currentAction.position != Vector2.zero && currentAction.position != null)
                    {
                        string anim = "";
                        if (currentAction.position.x > currentAction.character.transform.position.x)
                            anim = "walkRight";
                        else if (currentAction.position.x < currentAction.character.transform.position.x)
                            anim = "walkLeft";
                        PlayAnimation(anim, currentAction.character.GetComponent<Animator>());
                    }
                }
                animStarted = true;

                //Move the character to the position
                currentAction.character.transform.position = Vector2.MoveTowards(currentAction.character.transform.position, currentAction.position, walkSpeed * Time.deltaTime);
            }
            else if (currentAction.dialogue != null)
            {
                //Stop animation
                PlayAnimation("idleDown", currentAction.character.GetComponent<Animator>());

                //Play the dialogue
                if (talkNow)
                {
                    //Decide what is displayed in text box
                    if (currentDialogueLineI >= currentAction.dialogue.NumLines() && typewriterEffectComplete)
                    {
                        //Reset dialogue
                        dialogueTextBox.SetActive(false);
                        currentDialogueLineI = 0;
                        dialogueText.text = "";
                        typewriterInstance.StopManualEffects();
                        dialogueDone = true;
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
                            AddDialogueToTextBox();
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

                //Check if player is nearby, facing NPC, and clicks/presses E
                talkNow = Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0);
            }

            //Check if the action is done
            if (currentAction.character.transform.position == new Vector3(currentAction.position.x, currentAction.position.y, 0) || dialogueDone)
            {
                i++;
                dialogueDone = false;
                animStarted = false;

                if(i >= cutsceneActions.Count)
                {
                    //End the cutscene
                    player.GetComponent<Collider2D>().enabled = true;
                    playerScript.enabled = true;
                    //Despawn the NPCs
                    foreach (GameObject NPC in NPCs)
                    {
                        NPC.SetActive(false);
                    }
                    gameObject.SetActive(false);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            playerScript = other.gameObject.GetComponent<PlayerMovement>();
            playerIsHere = true;
        }
    }

    public void PlayAnimation(string anim, Animator npcAnimator)
    {
        npcAnimator.ResetTrigger(currentAnim);
        npcAnimator.SetTrigger(anim);
        currentAnim = anim;
    }

    public void AddDialogueToTextBox()
    {
        //Get the line
        string line = currentAction.dialogue.GetLine(currentDialogueLineI);

        //Replace <player_name> with player name if needed
        if (line.IndexOf("<player_name>") > -1 && playerScript.playerName.Length > 0)
        {
            line = line.Substring(0, line.IndexOf("<player_name>")) + playerScript.playerName + line.Substring(line.IndexOf(">") + 1);
        }

        //If pedro is saying this, then make the text red
        if (line.IndexOf("<pedro_is_speaking>") > -1)
        {
            line = line.Substring(line.IndexOf(">") + 1);
            dialogueText.color = Color.red;
        }
        else
        {
            dialogueText.color = Color.black;
        }

        //Actually put the dialogue in the text box thingy
        dialogueText.text = line;
    }

    public void SetTypewriterComplete()
    {
        typewriterEffectComplete = true;
    }
}
