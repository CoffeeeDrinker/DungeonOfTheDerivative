using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CutsceneAssigner : MonoBehaviour
{
    public List<Cutscene> cutscenes = new List<Cutscene>();
    public TextAsset cutsceneData;
    
    private int NPCIndex, x, y;

    void Start()
    {
        string text = cutsceneData.text;
        for(int i = 0; i < cutscenes.Count; i++)
        {
            text = text.Substring(0, text.IndexOf("~"));
            Int32.TryParse(text.Substring(0, text.IndexOf(" ")), out NPCIndex);
            Int32.TryParse(text.Substring(text.IndexOf(" ") + 1, text.IndexOf(",")), out x);

            cutscenes[i].cutsceneActions.Add(new CutsceneAction(cutscenes[i].NPCs[NPCIndex], new Vector2(x, y), null));
        }
    }
}
