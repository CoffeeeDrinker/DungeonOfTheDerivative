using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[DefaultExecutionOrder(10)] //this class's Start() method is called later
public class AttackOptionHandler : MonoBehaviour
{

    bool clicked;
    [SerializeField] GameObject player;
    [SerializeField] int order;
    [SerializeField] GameObject textField;
    TextMeshProUGUI text;
    PlayerController playerController;
    public GameObject attackOption;
    Move move;
    // Start is called before the first frame update
    void Start()
    {
        clicked = false;
        text = textField.GetComponent<TextMeshProUGUI>();
        playerController = player.GetComponent<PlayerController>();
        Debug.Log("is this real: " + playerController.name);
        Debug.Log("Ts not real: " + playerController.GetMoveList()[0].name);
        move = playerController.GetMoveList()[order];
        text.SetText(move.name);
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
