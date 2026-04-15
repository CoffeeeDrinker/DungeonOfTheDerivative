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
        activeMove = moveHolder.GetComponent<MoveContainer>().GetMove();
    }

    // Update is called once per frame
    void Update()
    {
        if(textObject.GetComponent<TextMeshProUGUI>().text != activeMove.name)
            textObject.GetComponent<TextMeshProUGUI>().text = activeMove.name;
    }

    public void SetMove(Move move)
    {
        activeMove = move;
    }

    public Move GetMove() { return activeMove; }

    public void OnClick()
    {
        ActiveMoveMaster.moveMaster.Click(this);
    }
}
