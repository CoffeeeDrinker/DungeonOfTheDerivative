using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public abstract class Question
{
    static List<Question> questionList;
    public string questionText;
    public float answer;
    public readonly string type;
    public abstract Question GenerateQuestion(string type);
    public abstract Question GenerateQuestion(string type, IQuestionSkeleton skeleton);
    public string GetQuestion()
    {
        return questionText;
    }

    public float GetAnswer()
    {
        return answer;
    }
}

class Algebra1Question : Question
{
    static List<Alg1QuestionSkeleton> questionTypes = new List<Alg1QuestionSkeleton>() {
        new Alg1QuestionSkeleton(
            3,
            "The line modeled by the equation y = $x + $ goes through the point ($, a). What is the value of a?",
            (vals) => //answer generation instructions
            {
                //Precondition: length of vals is equal to number of $ in question text
                float m = vals[0];
                float b = vals[1];
                float x = vals[2];
                return m*x + b;
            },
            () => //given number generation instructions
            {
                List<float> nums = new List<float>();
                for(int i = 0; i < 3; i++)
                {
                    nums.Add(UnityEngine.Random.Range(-10, 10));
                }
                return nums;
            }
        ), new Alg1QuestionSkeleton(
            6,
            "It is known that $x + $y = $ and $x + $y = $. What is the sum of x and y?",
            (vals) => //answer generation instructions
            {
                //Precondition: length of vals is equal to number of $ in question text
                float a1 = vals[0];
                float b1 = vals[1];
                float c1 = vals[2];
                float a2 = vals[3];
                float b2 = vals[4];
                float c2 = vals[5];
                float x = (c1*b2-c2*b1)/(a1*b2-a2*b1);
                float y = (c1-(a1*x))/b1;
                return x+y;
            },
            () => //answer generation instructions
            {
                List<float> vals = new List<float>();
                for(int i = 0; i < 6; i++)
                {
                    float rand = (UnityEngine.Random.Range(-10, 10));
                    while(rand == 0)
                    {
                        rand = UnityEngine.Random.Range(-10, 10);
                    }
                    vals.Add(rand);
                }
                while(vals[3]/vals[0] == vals[4] / vals[1])
                {
                    vals[0]++;
                }
                return vals;
            }
        )

    };
    private List<float> vals;
    //private string questionText;
    
    public override Question GenerateQuestion(string type)
    {
        Alg1QuestionSkeleton question = questionTypes[UnityEngine.Random.Range(0, questionTypes.Count)];
        vals = ((IQuestionSkeleton)question).GenerateNumbers();
        questionText = ((IQuestionSkeleton)question).ConstructQuestion(vals);
        answer = ((IQuestionSkeleton)question).GetAnswer(vals);
        return this;
    }

    public override Question GenerateQuestion(string type, IQuestionSkeleton skeleton)
    {
        Alg1QuestionSkeleton question;
        if (skeleton is Alg1QuestionSkeleton)
        {
            question = (Alg1QuestionSkeleton)skeleton;
        } 
        else
        {
            throw new Exception(); //this shouldn't happen, make sure you're using the right question generation code!
        }
        vals = skeleton.GenerateNumbers();
        questionText = skeleton.ConstructQuestion(vals);
        answer = skeleton.GetAnswer(vals);
        return this;
    }
}

public interface IQuestionSkeleton 
{
    public string ConstructQuestion(List<float> vals); //Precondition: vals contains only numbers within the specified acceptable range and contains exactly how many vals are needed 
    public float GetAnswer(List<float> vals);

    public List<float> GenerateNumbers();
}

public readonly struct Alg1QuestionSkeleton : IQuestionSkeleton
{
    readonly public int numVals;
    readonly public string question;
    readonly Func<List<float>, float> findAnswer; //Stores how to find the answer. Takes a list of ints as input, returns the answer as a float
    readonly Func<List<float>> generateNumbers; //Stores how to generate random values
    public Alg1QuestionSkeleton(int num, string q, Func<List<float>, float> method, Func<List<float>> genNums)
    {
        numVals = num; question = q; findAnswer = method; generateNumbers = genNums;
    }
    string IQuestionSkeleton.ConstructQuestion(List <float> vals)
    {
        string q = new string(question);
        int j = 0; //index to traverse vals
        for (int i = 0; i < q.Length; i++)
        {
            if (q.Substring(i, 1).Equals("$"))
            {
                q = q.Substring(0, i) + vals[j] + q.Substring(i + 1);
                j++;
            }
        }
        return q;
    }
    float IQuestionSkeleton.GetAnswer(List<float> vals)
    {
        string q = new string(question);
        int j = 0; //index to traverse vals
        for (int i = 0; i < q.Length; i++)
        {
            if (q.Substring(i, 1).Equals("$"))
            {
                q = q.Substring(0, i) + vals[j] + q.Substring(i + 1);
                j++;
            }
        }

        return findAnswer(vals);
    }

    List<float> IQuestionSkeleton.GenerateNumbers()
    {
        return generateNumbers();
    }
}