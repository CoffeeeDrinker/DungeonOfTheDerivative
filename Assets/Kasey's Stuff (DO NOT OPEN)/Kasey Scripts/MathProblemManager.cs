using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathProblemManager : MonoBehaviour
{
    //Player Stuff
    public Transform player;

    //Draw Screen stuff
    public MathProblemGetter mathProblemGetterScript;
    public GameObject drawscreenStuff;
    public Text problemTextBox;
    public Image problemImage;
    public InputField answerInput;

    public MathProblem currentProb;

    public void StartDraw()
    {
        drawscreenStuff.SetActive(true);
        drawscreenStuff.transform.position = player.position;
        currentProb = mathProblemGetterScript.GetRandProblem("Geometry");
        //Check if there is an image
        if (mathProblemGetterScript.GetProblemImage(currentProb) == null)
        {
            //Show text
            problemTextBox.text = currentProb.GetProblem();
        }
        else
        {
            //Show image
            problemImage.sprite = mathProblemGetterScript.GetProblemImage(currentProb);
            problemTextBox.text = mathProblemGetterScript.GetProblemWithoutImage(currentProb);
        }
    }

    public void DeleteDrawings()
    {
        GameObject holder = null;
        for (int i = 0; i < drawscreenStuff.transform.childCount; i++)
        {
            if (drawscreenStuff.transform.GetChild(i).name == "DrawEraseThingyHolder")
                holder = drawscreenStuff.transform.GetChild(i).gameObject;
        }

        if (holder != null)
        {
            for(int i = 0; i<holder.transform.childCount; i++)
            {
                Destroy(holder.transform.GetChild(i).gameObject);
            }
        }
    }

    //CODE HERE IS TEMPORARY UNTIL THE BATTLE SYSTEM IS COMPLETE
    public void CheckAnswer()
    {
        answerInput.text = "";
        drawscreenStuff.SetActive(false);
        DeleteDrawings();
        player.GetComponent<PlayerMovement>().enabled = true;
    }
}
