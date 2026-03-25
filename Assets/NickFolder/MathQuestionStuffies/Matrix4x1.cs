using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Matrix4x1 : MonoBehaviour, AnswerInput
{
    [SerializeField] TMP_InputField i1_1;
    [SerializeField] TMP_InputField i2_1;
    [SerializeField] TMP_InputField i3_1;
    [SerializeField] TMP_InputField i4_1;

    public string GetInput()
    {
        return "[" + i1_1.text + "]" + "[" + i2_1.text + "]" + "[" + i3_1.text + "]" + "[" + i4_1.text + "]";
    }

    public void ResetField()
    {
        i1_1.text = "";
        i2_1.text = "";
        i3_1.text = "";
        i4_1.text = "";
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}
