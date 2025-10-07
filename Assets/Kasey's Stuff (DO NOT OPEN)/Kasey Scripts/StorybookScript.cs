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

    //Holy Grail Stuff
    public Canvas canvas;
    public List<GameObject> holyGrailPrefabs;

    public Transform holyGrailHolder;
    public List<GameObject> holyGrails;

    public Transform blackHoleCenter;

    public float holyGrailSpeed;
    public float holyGrailRotationSpeed;
    Vector3 randLocation = new Vector3(0, 0, 0);

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

    void Update()
    {
        //Check if we should summon the Holy Grails (line is 13)
        if (storyLineI == 2)
        {
            //SUMMON THE HOLY GRAILS
            SummonHolyGrail();
        }
        
        for(int i = 0; i < holyGrails.Count; i++)
        {
            //Move the holy grail
            holyGrails[i].transform.position = Vector3.MoveTowards(holyGrails[i].transform.position, blackHoleCenter.position, holyGrailSpeed * Time.deltaTime);
            holyGrails[i].transform.Rotate(new Vector3(0, 0, holyGrailRotationSpeed * Time.deltaTime));

            //Check if we should delete the holy grail
            if (holyGrails[i].transform.position == blackHoleCenter.position)
            {
                Destroy(holyGrails[i]);
                holyGrails.RemoveAt(i);
            }
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

    public void SummonHolyGrail()
    {
        int randNum = Random.Range(0, 6);
        switch (randNum){
            case 0: //Spawn above screen
                randLocation.x = Random.Range(-200, canvas.pixelRect.width+200);
                randLocation.y = Random.Range(canvas.pixelRect.height, canvas.pixelRect.height+200);
                break;
            case 1: //Spawn below screen
                randLocation.x = Random.Range(-200, canvas.pixelRect.width + 200);
                randLocation.y = Random.Range(-200, 0);
                break;
            case 2: //Spawn left of screen
                randLocation.x = Random.Range(-200, 0);
                randLocation.y = Random.Range(-200, canvas.pixelRect.height + 200);
                break;
            case 3: //Spawn right of screen
                randLocation.x = Random.Range(canvas.pixelRect.width, canvas.pixelRect.width + 200);
                randLocation.y = Random.Range(-200, canvas.pixelRect.height + 200);
                break;
        }
        holyGrails.Add(Instantiate(holyGrailPrefabs[Random.Range(0, holyGrailPrefabs.Count)], randLocation, Quaternion.identity, holyGrailHolder));
    }
}
