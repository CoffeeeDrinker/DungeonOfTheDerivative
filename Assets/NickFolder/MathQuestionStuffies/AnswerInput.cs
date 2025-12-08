using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AnswerInput
{
    public string GetInput();
    public GameObject GetGameObject();
    public void ResetField();
}
