using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathProblemGetter : MonoBehaviour
{
    public TextAsset mathProblemsFile;
    public List<Sprite> images;
    private string text;
    
    public List<MathProblem> problems = new List<MathProblem>();

    void Start()
    {
        text = mathProblemsFile.text;
        string subj, prob, answ;

        while (text.Length > 1)
        {
            subj = text.Substring(0, text.IndexOf("\n"));
            text = text.Substring(text.IndexOf("\n") + 1);

            prob = text.Substring(0, text.IndexOf("~")-1);
            text = text.Substring(text.IndexOf("~") + 3);

            answ = text.Substring(0, text.IndexOf("~~")-1);
            text = text.Substring(text.IndexOf("~~")+4);

            problems.Add(new MathProblem(subj, prob, answ));
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            MathProblem m = GetRandProblem("Geometry");
            m.PrintProblem();
            if(m.GetProblem().IndexOf("_image") > 0)
            {
                Debug.Log(m.GetProblem().Substring(0, m.GetProblem().IndexOf("_image")) +" "+ images[0].name);
                Debug.Log(m.GetProblem().Substring(0, m.GetProblem().IndexOf("_image")) == images[0].name);
            }
        }
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
        for (int i = 0; i < images.Count; i++)
        {
            if (prob.GetProblem().Substring(0, prob.GetProblem().IndexOf("_image")) == images[i].name);
                return images[i];
        }
        return null;
        //You left off here, today finish the getting the image for a math problem script
    }
}
