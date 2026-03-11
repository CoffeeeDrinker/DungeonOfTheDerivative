using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneAction
{
    public GameObject character;
    public Vector2 position;
    public Dialogue dialogue;

    public CutsceneAction(GameObject character, Vector2 position, Dialogue dialogue)
    {
        this.character = character;
        this.position = position;
        this.dialogue = dialogue;
    }

    public void PlayAnimation(string anim, Animator npcAnimator)
    {
        npcAnimator.ResetTrigger("idleDown");
        npcAnimator.SetTrigger(anim);
    }
}
