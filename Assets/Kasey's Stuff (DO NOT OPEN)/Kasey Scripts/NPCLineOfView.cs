using UnityEngine;

public class NPCLineOfView : MonoBehaviour
{
    private NPCDialogueScript npc;
    private bool alreadyBattled = false;

    void Start()
    {
        npc = transform.parent.GetComponent<NPCDialogueScript>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && !alreadyBattled)
        {
            npc.talkNow = true;
            npc.playerIsHere = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        npc.playerIsHere = false;
    }
}
