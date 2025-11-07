using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackOptionHandler : MonoBehaviour
{

    bool clicked;
    [SerializeField] GameObject player;
    [SerializeField] int order;
    PlayerController playerController;
    public GameObject attackOption;
    Move move;
    // Start is called before the first frame update
    void Start()
    {
        clicked = false;
        playerController = player.GetComponent<PlayerController>();
        move = playerController.GetMoveList()[order];


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

    public void SetOrder(int order)
    {
        this.order = order;
        move = playerController.GetMoveList()[order];
    }
}
