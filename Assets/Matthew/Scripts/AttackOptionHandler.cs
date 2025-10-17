using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOptionHandler : MonoBehaviour
{
    bool clicked;
    [SerializeField] int maxDmg;
    [SerializeField] int minDmg;
    [SerializeField] int staminaCost;
    public GameObject attackOption;
    Move move;
    // Start is called before the first frame update
    void Start()
    {
        clicked = false;
        move = new Move(minDmg, maxDmg, staminaCost);
        attackOption.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActive(bool active)
    {
        attackOption.SetActive(active);
    }

    public void Unclick() //unclicks the button
    {
        clicked = false;
    }

    public bool IsClicked()
    {
        return clicked;
    }

    public void OnButtonPressed()
    {
        clicked = true;
    }

    public Move GetMove()
    {
        return move;
    }
}
