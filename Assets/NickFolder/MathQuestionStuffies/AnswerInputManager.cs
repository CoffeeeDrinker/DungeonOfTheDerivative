using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerInputManager : MonoBehaviour
{
    public static AnswerInputManager Instance;
    [SerializeField] GameObject matrix;
    [SerializeField] GameObject number;

    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            Instance = this;
        }
    }

    public GameObject GetAnswerInput(E_AnswerInputs type)
    {
        switch(type)
        {
            case E_AnswerInputs.matrix:
                Debug.Log("hi");
                return matrix;
            case E_AnswerInputs.number:
                Debug.Log("Work ):< ):< ):<");
                return number;
            default:
                return null;
        }
    }
}
