using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunHandler : UIController
{
    // Start is called before the first frame update
    bool clicked;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonClicked()
    {
        clicked = true;
    }

    public bool IsClicked()
    {
        return clicked;
    }

    public void Unclick()
    {
        clicked = false;
    }
}
