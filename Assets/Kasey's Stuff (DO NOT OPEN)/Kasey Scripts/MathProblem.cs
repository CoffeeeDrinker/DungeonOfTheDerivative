using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MathProblem
{
    public string subject;
    public string problem;
    public Sprite image = null;
    public GameObject answerInput;
    public string answer;
    List<string> variables;
    public MathProblem()
    {

    }

    public MathProblem(string subject, string problem, string answer, GameObject answerInput)
    {
        this.subject = subject.Trim();
        this.problem = problem.Trim();
        this.answer = answer.Trim();
        this.answerInput = answerInput;
    }

    public MathProblem(string subject, string problem, string answer, GameObject answerInput, List<string> variables)
    {
        this.subject = subject.Trim();
        this.problem = problem.Trim();
        this.answer = answer.Trim();
        this.answerInput = answerInput;
        this.variables = variables;
    }

    public string GetSubject()
    {
        return subject;
    }

    public string GetProblem()
    {
        return problem;
    }

    public string GetAnswer()
    {
        return answer;
    }

    public void PrintProblem()
    {
        Debug.Log("Subject: "+subject+"\nProblem: "+problem+ "\nAnswer: " + answer);
    }

    public GameObject GetAnswerInput()
    {
        return answerInput;
    }

    public List<string> GetVariables()
    {
        return variables;
    }
}

