using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] ItemManager itemManager;
    [SerializeField] Grid grid;
    [SerializeField] GameObject canvas;
    bool inventoryOpen;
    List<Item> currItems;

    // Start is called before the first frame update
    void Start()
    {
        inventoryOpen = false;
        currItems = new List<Item>();
        canvas.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            canvas.GetComponent<Canvas>().enabled = !inventoryOpen;
            inventoryOpen = !inventoryOpen;
        }
    }

    public void AddItem(string itemName, int amount)
    {
        Item item = gameObject.AddComponent<Item>();
        item.SetName(itemName);
        Debug.Log(itemName);
        Debug.Log(item.GetName() + "?");
        item.SetSprite(itemManager.GetSprite(itemName));
        currItems.Add(item);
        grid.AddItem(item, amount);
    }
}
