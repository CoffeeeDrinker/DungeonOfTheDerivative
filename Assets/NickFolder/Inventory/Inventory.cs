using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] ItemManager itemManager;
    [SerializeField] Grid grid;
    [SerializeField] GameObject canvas;
    [SerializeField] HeldItemManager heldItemManager;
    [SerializeField] PlayerMovement playerMovement;
    float playerSpeed = 2000;
    bool inventoryOpen;
    List<Item> currItems;

    // Start is called before the first frame update
    void Start()
    {
        inventoryOpen = false;
        currItems = new List<Item>();
        canvas.GetComponent<Canvas>().enabled = false;
    }

    public void ToggleInventory()
    {
        canvas.GetComponent<Canvas>().enabled = !inventoryOpen;
        inventoryOpen = !inventoryOpen;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(!inventoryOpen)
            {
                playerSpeed = playerMovement.speed;
                playerMovement.speed = 0;
            } else
            {
                playerMovement.speed = playerSpeed;
                playerMovement.enabled = true;
            }

            if (inventoryOpen && heldItemManager.HoldingItem())
            {
                grid.AddItem(heldItemManager.GetItem(), heldItemManager.GetItemAmount());
                heldItemManager.RemoveItem();
            }

            canvas.GetComponent<Canvas>().enabled = !inventoryOpen;
            inventoryOpen = !inventoryOpen;
        }
    }

    public void AddItem(ItemNameEnum itemName, int amount)
    {
        Item item = gameObject.AddComponent<Item>();
        item.SetName(itemName);
        item.SetSprite(itemManager.GetSprite(itemName));
        currItems.Add(item);
        grid.AddItem(item, amount);
    }

    public List<Item> GetCurrItems()
    {
        return currItems;
    }
}
