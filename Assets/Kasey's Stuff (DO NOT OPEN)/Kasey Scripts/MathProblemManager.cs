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
        Debug.Log(mathProblemGetterScript.GetProblemImage(currentProb).name);
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

    //CODE HERE IS TEMPORARY UNTIL THE BATTLE SYSTEM IS COMPLETE
    public void CheckAnswer()
    {
        Debug.Log(answerInput.text.Trim() == currentProb.GetAnswer());
        answerInput.text = "";
        drawscreenStuff.SetActive(false);
    }
}
