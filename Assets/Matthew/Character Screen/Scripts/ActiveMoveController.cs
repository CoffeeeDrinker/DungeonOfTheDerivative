using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ActiveMoveController : MonoBehaviour
{
    [SerializeField] GameObject moveHolder;
    [SerializeField] GameObject textObject;
    Move activeMove;
    // Start is called before the first frame update
    void Start()
    {
        activeMove = moveHolder.GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        if(textObject.GetComponent<TextMeshPro>().text != activeMove.name)
            textObject.GetComponent<TextMeshPro>().text = activeMove.name;
    }
}
