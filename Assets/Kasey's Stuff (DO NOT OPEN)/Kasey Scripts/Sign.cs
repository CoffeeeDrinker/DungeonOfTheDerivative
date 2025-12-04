using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public Transform player;
    private bool playerIsHere = false;

    public GameObject actualSign;
    public TextMeshProUGUI classNameText;
    public TextMeshProUGUI teacherNameText;

    public string className, teacherName;

    void Update()
    {
        //Check if the player is there and they click
        if (playerIsHere && Input.GetMouseButtonDown(0) && !actualSign.activeSelf)
        {
            //Make sure sign says the correct information
            classNameText.text = className;
            teacherNameText.text = teacherName;

            //Show the sign
            actualSign.SetActive(true);
        }
        else if (playerIsHere && (Input.GetKeyDown(KeyCode.Escape) || (Input.GetMouseButtonDown(0) && actualSign.activeSelf)))
        {
            //Hide the sign
            actualSign.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        playerIsHere = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        playerIsHere = false;
        actualSign.SetActive(false);
    }
}
