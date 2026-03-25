using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Matrix1x2 : MonoBehaviour, AnswerInput
{
    [SerializeField] TMP_InputField i1_1;
    [SerializeField] TMP_InputField i1_2;

    public string GetInput()
    {
        return "[" + i1_1.text + "," + i1_2.text + "]";
    }

    public void ResetField()
    {
        i1_1.text = "";
        i1_2.text = "";
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}
