using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class MathProblemManager : MonoBehaviour
{
    //Player Stuff
    public Transform player;

    //Draw Screen stuff
    public MathProblemGetter mathProblemGetterScript;
    public GameObject drawscreenStuff;
    public Text problemTextBox;
    public Image problemImage;
    public GameObject answerInputGameObject;
    public AnswerInput answerInput;

    public MathProblem currentProb;
    private bool answered;
    private bool correct;

    public void StartDraw()
    {
        drawscreenStuff.SetActive(true);
        DeleteDrawings();
        drawscreenStuff.transform.position = player.position;
        currentProb = mathProblemGetterScript.GetRandProblem("Algebra 1");
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


        answerInputGameObject = currentProb.GetAnswerInput();
        answerInputGameObject.SetActive(true);
        answerInput = answerInputGameObject.GetComponent<AnswerInput>();
    }

    public void DeleteDrawings()
    {
        Debug.Log("AAAAA");
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

        if (answered && correct)
        {
            return 0;
        }
        else if(answered && !correct)
        {
            return 1;
        }
        return 2;
    }

    public void OnSubmit()
    {

        Answer();
        string input = answerInput.GetInput();
        correct = input == currentProb.GetAnswer();
        if (correct)
        {
            Debug.Log("Right answer!");
        }
        else
        {
            Debug.Log("Wrong answer, Mr. Huff hates you");
        }
        Debug.Log("a");
        answerInput.ResetField();
        Debug.Log("b");
        answerInput.GetGameObject().SetActive(false);
        Debug.Log("c");
        drawscreenStuff.SetActive(false);
        Debug.Log("d");
        DeleteDrawings();
        Debug.Log("?");
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
