using EasyTextEffects;
using EasyTextEffects.Effects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class StorybookScript : MonoBehaviour
{
    //Story Stuff
    public List<string> storyLines = new List<string>();
    public List<Sprite> storySprites = new List<Sprite>();
    public TextAsset storyTextAsset;
    private string file;
    private int storyLineI = 0;

    //UI Stuff
    public GameObject storyBookStuff;
    public TextEffect typewriterInstance;
    public TextMeshProUGUI storyTextBox;
    public Image storyImageBox;

    //Typewriter Stuff
    public Effect_Color typewriterEffect;
    private bool typewriterEffectComplete = true;

    //Player Stuff
    public PlayerMovement playerScript;

    void Start()
    {
        //Get story from text file
        file = storyTextAsset.text;
        while (file.Length > 1)
        {
            storyLines.Add(file.Substring(0, file.IndexOf("\n")));
            file = file.Substring(file.IndexOf("\n")+1);
        }
    }

    public void NextPage()
    {
        //Check if story should end
        if (storyLines.Count-1 <= storyLineI && typewriterEffectComplete)
        {
            //End the story
            storyBookStuff.SetActive(false);
            playerScript.enabled = true;
        }
        else
        {
            //If typewriter is finished, go to next page
            if (typewriterEffectComplete)
            {
                typewriterEffectComplete = false;
                storyTextBox.text = storyLines[++storyLineI];
                typewriterInstance.Refresh();
                typewriterInstance.StartManualEffects();
                storyImageBox.sprite = storySprites[storyLineI];
            }
            else
            {
                //If typewriter isn't finished, finish it
                typewriterEffectComplete = true;
                typewriterInstance.Refresh();
                typewriterInstance.StopManualEffects();
            }
        }
    }

    public void PrevPage()
    {
        typewriterEffectComplete = false;
        if(storyLineI > 0)
        {
            storyTextBox.text = storyLines[--storyLineI];
            storyImageBox.sprite = storySprites[storyLineI];
        }
        typewriterInstance.Refresh();
        typewriterInstance.StartManualEffects();
    }

    public void SetTypewriterComplete()
    {
        typewriterEffectComplete = true;
    }
}
