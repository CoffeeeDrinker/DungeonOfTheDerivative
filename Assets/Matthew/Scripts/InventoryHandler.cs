using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : UIController
{
    [SerializeField] Inventory inventory;
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
        inventory.ToggleInventory();
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
