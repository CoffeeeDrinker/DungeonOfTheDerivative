using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Number : MonoBehaviour, AnswerInput
{
    [SerializeField] TMP_InputField input;
    public string GetInput()
    {
        return input.text;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void ResetField()
    {
        input.text = "?";
    }
}
