using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public GameObject combatSystem;
    public GameObject player;
    public Vector3 combatSystemTransform;

    private NPCDialogueScript currentNPC;

    public Animator TransitionAnims;
    public GameObject transitionSprite;

    private bool needCombatActive = false;

    void Update()
    {
        if (needCombatActive && !combatSystem.activeSelf && TransitionAnims.GetCurrentAnimatorStateInfo(0).IsName("Transition") && TransitionAnims.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {
            //Close Animation
            TransitionAnims.ResetTrigger("openTransition");
            TransitionAnims.SetTrigger("closeTransition");
            transitionSprite.GetComponent<SpriteRenderer>().sprite = null;

            player.GetComponent<SpriteRenderer>().enabled = false;
            combatSystem.transform.localPosition = player.transform.localPosition + combatSystemTransform;
            combatSystem.SetActive(true);

            needCombatActive = false;
        }
    }

    public void StartCombat(NPCDialogueScript currentNPC)
    {
        //Play Transition Animation
        TransitionAnims.ResetTrigger("closeTransition");
        TransitionAnims.SetTrigger("openTransition");

        this.currentNPC = currentNPC;
        needCombatActive= true;
    }

    public void CloseCombatSystem()
    {
        //Play Transition Animation
        TransitionAnims.ResetTrigger("closeTransition");
        TransitionAnims.SetTrigger("openTransition");

        player.GetComponent<SpriteRenderer>().enabled = true;
        combatSystem.SetActive(false);
        currentNPC.DefeatDialogue();

        currentNPC = null;
    }
}
