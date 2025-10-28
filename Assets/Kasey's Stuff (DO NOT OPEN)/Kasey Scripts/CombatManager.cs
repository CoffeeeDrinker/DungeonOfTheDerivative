using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public GameObject combatSystem;
    public GameObject player;
    public Vector3 combatSystemTransform;

    private NPCDialogueScript currentNPC;

    public void StartCombat(NPCDialogueScript currentNPC)
    {
        this.currentNPC = currentNPC;
        combatSystem.SetActive(true);
        combatSystem.transform.localPosition = player.transform.localPosition + combatSystemTransform;
    }

    public void CloseCombatSystem()
    {
        combatSystem.SetActive(false);
        currentNPC.DefeatDialogue();

        currentNPC = null;
    }
}
