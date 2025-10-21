using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : UIController
{
    bool clicked = false;
   

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonPressed()
    {
        if (TurnSystem.turnIndex == 0)
        {//only accepts clicks if it's player's turn
            clicked = true;
        }
    }

    public bool IsClicked()
    {
        return clicked;
    }

    public void Unclick()
    {
        clicked = false;
    }

    public void SetActive(bool active)
    {
        attackController.ToggleAttackOptions(active);
    }
}
