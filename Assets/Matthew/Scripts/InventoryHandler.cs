using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : UIController
{
    bool clicked;
    // Start is called before the first frame update
    void Start()
    {
        clicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClicked()
    {
        clicked = true;
        //Open the inventory
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
