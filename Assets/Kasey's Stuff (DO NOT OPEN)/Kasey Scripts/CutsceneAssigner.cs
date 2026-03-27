using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CutsceneAssigner : MonoBehaviour
{
    public List<Cutscene> cutscenes = new List<Cutscene>();
    public TextAsset cutsceneData;

    private int NPCIndex;
    private float x, y;
    private Dialogue dialogue = null;

    void Start()
    {
        string text = cutsceneData.text;
        for(int i = 0; i < cutscenes.Count; i++)
        {
            dialogue = null;
            while (text.Length > 1 && text.IndexOf("~")!=0)
            {
                //Get character
                int.TryParse(text.Substring(0, text.IndexOf(" ")), out NPCIndex);

                //Get position
                float.TryParse(text.Substring(text.IndexOf(" ") + 1, text.IndexOf(",") - text.IndexOf(" ") - 1), out x);
                text = text.Substring(text.IndexOf(",") + 1);
                float.TryParse(text.Substring(0, text.IndexOf(" ")), out y);

                //Get dialogue
                text = text.Substring(text.IndexOf(" ") + 1);
                if (text.Substring(0, text.IndexOf("\n")).Trim() != "null")
                {
                    //Actually get the dialogue
                    dialogue = new Dialogue();
                    string dialogueText = text.Substring(1, text.Substring(1).IndexOf("\"")).Trim();
                    while(dialogueText.Length > 0)
                    {
                        dialogue.AddLine(dialogueText.Substring(0, dialogueText.IndexOf(",")));
                        try
                        {
                            dialogueText = dialogueText.Substring(dialogueText.IndexOf(",") + 2);
                        }
                        catch (Exception)
                        {
                            break;
                            throw;
                        }
                    }
                }
                text = text.Substring(text.IndexOf("\n") + 1);

                if(text.IndexOf("~") == 0)
                {
                    text = text.Substring(1);
                    cutscenes[i].cutsceneActions.Add(new CutsceneAction(cutscenes[i].NPCs[NPCIndex], new Vector2(x, y), dialogue));
                    break;
                }
                //Actually add the action to the cutscene
                cutscenes[i].cutsceneActions.Add(new CutsceneAction(cutscenes[i].NPCs[NPCIndex], new Vector2(x, y), dialogue));
            }
        }
    }

    public void SetAllTypewritersComplete()
    {
        foreach (Cutscene cutscene in cutscenes)
        {
            cutscene.SetTypewriterComplete();
        }
    }
}