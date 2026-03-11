using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerInputManager : MonoBehaviour
{
    public static AnswerInputManager Instance;
    [SerializeField] GameObject matrix_1x2;
    [SerializeField] GameObject matrix_1x3;
    [SerializeField] GameObject matrix_2x1;
    [SerializeField] GameObject matrix_2x2;
    [SerializeField] GameObject matrix_2x3;
    [SerializeField] GameObject matrix_3x1;
    [SerializeField] GameObject matrix_3x3;
    [SerializeField] GameObject matrix_4x1;
    [SerializeField] GameObject number;
    [SerializeField] GameObject variables;
    [SerializeField] GameObject options;

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
            case E_AnswerInputs.matrix_1x2:
                return matrix_1x2;
            case E_AnswerInputs.matrix_1x3:
                return matrix_1x3;
            case E_AnswerInputs.matrix_2x1:
                return matrix_2x1;
            case E_AnswerInputs.matrix_2x2:
                return matrix_2x2;
            case E_AnswerInputs.matrix_2x3:
                return matrix_2x3;
            case E_AnswerInputs.matrix_3x1:
                return matrix_3x1;
            case E_AnswerInputs.matrix_3x3:
                return matrix_3x3;
            case E_AnswerInputs.matrix_4x1:
                return matrix_4x1;
            case E_AnswerInputs.number:
                return number;
            case E_AnswerInputs.variables:
                return variables;
            case E_AnswerInputs.options:
                return options;
            default:
                return null;
        }
    }
}
