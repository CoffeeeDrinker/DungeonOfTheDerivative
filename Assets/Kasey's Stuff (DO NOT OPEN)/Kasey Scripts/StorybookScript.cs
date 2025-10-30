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
    public List<Texture> storySprites = new List<Texture>();
    public TextAsset storyTextAsset;
    private string file;
    private int storyLineI = 0;

    //UI Stuff
    public GameObject storyBookStuff;
    public TextEffect typewriterInstance;
    public TextMeshProUGUI storyTextBox;
    public RawImage storyImageBox;

    //Typewriter Stuff
    public Effect_Color typewriterEffect;
    private bool typewriterEffectComplete = true;

    //Holy Grail Stuff
    public Canvas canvas;
    public List<GameObject> holyGrailPrefabs;
    private int summonNum = 0;

    public Transform holyGrailHolder;
    public List<GameObject> holyGrails;
    public List<float> realHolyGrailSpeed;
    public List<float> realHolyGrailRotationSpeed;

    public Transform blackHoleCenter;

    public float holyGrailSpeed;
    public float holyGrailRotationSpeed;
    Vector3 randLocation = new Vector3(0, 0, 0);

    //Player Stuff
    public PlayerMovement playerScript;

    //Pedro
    public Image Pedro;
    public float PedroSpeed;
    private Vector3 PedroLocation;

    void Start()
    {
        //Get story from text file
        file = storyTextAsset.text;
        while (file.Length > 1)
        {
            storyLines.Add(file.Substring(0, file.IndexOf("\n")));
            file = file.Substring(file.IndexOf("\n")+1);
        }

        //Set Pedro Location
        PedroLocation = new Vector3(Pedro.transform.localPosition.x, -30f, 0f);
    }

    void Update()
    {
        //Check if we should summon the Holy Grails (line is 13)
        if (storyLineI == 13)
        {
            //Levitate Pedro
            Pedro.gameObject.SetActive(true);
            Pedro.transform.localPosition = Vector3.MoveTowards(Pedro.transform.localPosition, PedroLocation, PedroSpeed * Time.deltaTime);

            //SUMMON THE HOLY GRAILS
            if (Pedro.transform.localPosition == PedroLocation && summonNum%2 == 0)
            {
                SummonHolyGrail();
            }
            summonNum++;
        } else if (holyGrailHolder.childCount > 0)
        {
            for (int i = 0; i < holyGrails.Count; i++)
            {
                Destroy(holyGrails[i]);
                holyGrails.RemoveAt(i);
                realHolyGrailSpeed.RemoveAt(i);
                realHolyGrailRotationSpeed.RemoveAt(i);
            }
            Pedro.gameObject.SetActive(false);
        }

            for (int i = 0; i < holyGrails.Count; i++)
            {
                //Move the holy grail
                holyGrails[i].transform.position = Vector3.MoveTowards(holyGrails[i].transform.position, blackHoleCenter.position, realHolyGrailSpeed[i] * Time.deltaTime);
                holyGrails[i].transform.Rotate(new Vector3(0, 0, realHolyGrailRotationSpeed[i] * Time.deltaTime));

                //Check if we should delete the holy grail
                if (holyGrails[i].transform.position == blackHoleCenter.position)
                {
                    Destroy(holyGrails[i]);
                    holyGrails.RemoveAt(i);
                    realHolyGrailSpeed.RemoveAt(i);
                    realHolyGrailRotationSpeed.RemoveAt(i);
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
                storyImageBox.texture = storySprites[storyLineI];
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
            storyImageBox.texture = storySprites[storyLineI];
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

        //Get speeds
        realHolyGrailSpeed.Add(holyGrailSpeed*Random.Range(0.75f, 1.5f));
        if(Random.Range(-1f, 1f) < 0)
        {
            realHolyGrailRotationSpeed.Add(-1*holyGrailRotationSpeed*Random.Range(0.75f, 1.5f));
        }
        else
        {
            realHolyGrailRotationSpeed.Add(holyGrailRotationSpeed * Random.Range(0.75f, 1.5f));
        }
    }
}
