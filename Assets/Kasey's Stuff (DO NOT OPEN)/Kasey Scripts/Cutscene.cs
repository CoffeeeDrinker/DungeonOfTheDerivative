using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class Cutscene : MonoBehaviour
{
    private bool playerIsHere = false;
    private GameObject player;
    private PlayerMovement playerScript;

    public string cutsceneName;
    public List<CutsceneAction> cutsceneActions = new List<CutsceneAction>();
    public List<GameObject> NPCs;

    private int i = 0;
    private bool cutsceneStarted = false;
    private CutsceneAction currentAction;

    void Start()
    {
        cutsceneActions.Add(new CutsceneAction(NPCs[0], new Vector2(-1f, -1.5f), null));
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

            //Start walk anim (if needed)
            currentAction = cutsceneActions[i];
            if (currentAction.position != Vector2.zero && currentAction.position != null)
            {
                string anim = "";
                if (currentAction.position.x > currentAction.character.transform.position.x)
                    anim = "walkRight";
                //You left of here
                PlayAnimation(anim, currentAction.character.GetComponent<Animator>());
            }
            else if (currentAction.dialogue != null)
            {
                //Play idle animation
                PlayAnimation("idleDown", currentAction.character.GetComponent<Animator>());
            }

            cutsceneStarted = true;

        } else if (playerIsHere && cutsceneStarted)
        {
            if (currentAction.position != Vector2.zero && currentAction.position != null)
            {
                //Move the character to the position
                currentAction.character.transform.position = Vector2.MoveTowards(currentAction.character.transform.position, currentAction.position, 1.5f * Time.deltaTime);
            }
            else if (currentAction.dialogue != null)
            {
                //Play the dialogue
            }

            if (currentAction.character.transform.position == new Vector3(currentAction.position.x, currentAction.position.y, 0))
            {
                //i++;
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
        npcAnimator.SetTrigger(anim);
    }
}
