using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/* How to use this class guide:
 * Create a gameobject and assign this script to it.
 * Give the script the text file for all the questions and the list of images that go with the questions.
 * 
 * You can then get a random math problem with GetRandProblem(string), with the sring parameter being the subject (Algebra 1, Geometry, etc).
 * Get the name of the image associated with a problem using GetProblem().Substring(0, m.GetProblem().IndexOf("_image"))
 * Compare this with the list of problem images. 
 */

public class MathProblemGetter : MonoBehaviour
{
    public string mathPath = Application.dataPath + "/NickFolder/MathProblems/MathProblems.txt";
    public TextAsset mathProblemsFile;
    public List<Sprite> images;
    private string text;
    
    public List<MathProblem> problems = new List<MathProblem>();

    void Start()
    {
        string[] lines = File.ReadAllLines(mathPath);
        text = mathProblemsFile.text;
        string subj, prob, answ, ansInputText;
        GameObject answerInput = null;
        int i = 0;

        while (text.Length > 1 && i < lines.Length)
        {
            subj = text.Substring(0, text.IndexOf("\n"));
            text = text.Substring(text.IndexOf("\n") + 1);
            subj = lines[i];
            i++;

            prob = text.Substring(0, text.IndexOf("~")-1);
            text = text.Substring(text.IndexOf("~") + 3);
            prob = "";

            while (lines[i] != "~")
            {
                prob += lines[i];
                i++;
            }
            i++;

            answ = text.Substring(0, text.IndexOf("~~")-2);
            answ = "";
            text = text.Substring(text.IndexOf("~~")+4);

            while (lines[i] != "~~")
            {
                answ += lines[i];
                i++;
            }
            i++;

            ansInputText = text.Substring(0, text.IndexOf("~~~")-2);
            ansInputText = "";

            while (lines[i] != "~~~")
            {
                ansInputText += lines[i];
                i++;
            }
            i++;

            text = text.Substring(text.IndexOf("~~~") + 5);

            switch (ansInputText)
            {
                case "1x2 Matrix":
                    answerInput = AnswerInputManager.Instance.GetAnswerInput(E_AnswerInputs.matrix_1x2);
                    break;
                case "1x3 Matrix":
                    answerInput = AnswerInputManager.Instance.GetAnswerInput(E_AnswerInputs.matrix_1x3);
                    break;
                case "2x1 Matrix":
                    answerInput = AnswerInputManager.Instance.GetAnswerInput(E_AnswerInputs.matrix_2x1);
                    break;
                case "2x2 Matrix":
                    answerInput = AnswerInputManager.Instance.GetAnswerInput(E_AnswerInputs.matrix_2x2);
                    break;
                case "2x3 Matrix":
                    answerInput = AnswerInputManager.Instance.GetAnswerInput(E_AnswerInputs.matrix_2x3);
                    break;
                case "3x1 Matrix":
                    answerInput = AnswerInputManager.Instance.GetAnswerInput(E_AnswerInputs.matrix_3x1);
                    break;
                case "3x3 Matrix":
                    answerInput = AnswerInputManager.Instance.GetAnswerInput(E_AnswerInputs.matrix_3x3);
                    break;
                case "4x1 Matrix":
                    answerInput = AnswerInputManager.Instance.GetAnswerInput(E_AnswerInputs.matrix_4x1);
                    break;
                case "Number":
                    answerInput = AnswerInputManager.Instance.GetAnswerInput(E_AnswerInputs.number);
                    break;
                case "Variables":
                    answerInput = AnswerInputManager.Instance.GetAnswerInput(E_AnswerInputs.variables);
                    break;
                case "Options":
                    answerInput = AnswerInputManager.Instance.GetAnswerInput(E_AnswerInputs.options);
                    break;
            }

            problems.Add(new MathProblem(subj, prob, answ, answerInput));
        }
    }

    private void Update()
    {
        /*
        if (Input.GetKey(KeyCode.Space))
        {
            MathProblem m = GetRandProblem("Geometry");
            m.PrintProblem();
            if(m.GetProblem().IndexOf("_image") > 0)
            {
                Debug.Log(m.GetProblem().Substring(0, m.GetProblem().IndexOf("_image")) +" "+ images[0].name);
                Debug.Log(m.GetProblem().Substring(0, m.GetProblem().IndexOf("_image")) == images[0].name);
                testImage.sprite = images[0];
            }
        } */
    }

    public MathProblem GetRandProblem(string subject)
    {
        int count = 0;
        int i = Random.Range(0, problems.Count);
        while(true){
            if (problems[i].GetSubject().Trim() == subject.Trim())
                return problems[i];
            i = Random.Range(0, problems.Count);
            count++;
            if (count > 999)
                return null;
        }
    }

    public Sprite GetProblemImage(MathProblem prob)
    {
        Debug.Log(prob.GetProblem().Substring(0, prob.GetProblem().IndexOf("_image")));
        if(prob == null || prob.GetProblem().IndexOf("_image") < 0)
        {
            return null;
        }
        else
        {
            for (int i = 0; i < images.Count; i++)
            {   
                if (prob.GetProblem().Substring(prob.GetProblem().LastIndexOf("\n") + 1, prob.GetProblem().IndexOf("_image") - prob.GetProblem().LastIndexOf("\n") - 1).Trim() == images[i].name.Trim().Substring(0, images[i].name.Trim().IndexOf("_image")))
                    return images[i];
            }
        }
        return null;
    }

    public string GetProblemWithoutImage(MathProblem prob)
    {
        if(prob.GetProblem().Contains("_image"))
        {
            return "";
        } else
        {
            return prob.GetProblem();
        }
    }

    public GameObject GetProblemAnswerInput(MathProblem prob)
    {
        return prob.GetAnswerInput();
    }
}
