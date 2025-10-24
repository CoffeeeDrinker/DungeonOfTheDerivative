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
    private bool answered;

    public void StartDraw()
    {
        drawscreenStuff.SetActive(true);
        DeleteDrawings();
        answerInput.text = "";
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
    public int CheckAnswer() //0 = correct, 1 = incorrect, 2 = no answer yet
    {
        if (answered && currentProb.answer.Trim() == answerInput.text.Trim())
        {
            Debug.Log("correct answer");
            drawscreenStuff.SetActive(false);
            return 0;
        }
        else if(answered && currentProb.answer.Trim() != answerInput.text.Trim())
        {
            drawscreenStuff.SetActive(false);
            return 1;
        }
        return 2;
    }

    public void Answer()
    {
        answered = true;
    }

    public void UnAnswer()
    {
        answered = false;
    }
}
