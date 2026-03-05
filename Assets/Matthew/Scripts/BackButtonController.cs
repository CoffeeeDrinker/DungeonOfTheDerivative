using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonController : MonoBehaviour
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
        clicked = true;
    }

    public void Unclick()
    {
        clicked = false;
    }

    public bool IsClicked()
    {
        return clicked;
    }
}