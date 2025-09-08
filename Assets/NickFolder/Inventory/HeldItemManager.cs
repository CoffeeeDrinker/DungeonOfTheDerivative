using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeldItemManager : MonoBehaviour
{
    Item currItem;
    int amount;
    GridSlot prevSlot;
    Image itemSprite;

    [SerializeField] GameObject itemSpriteObject;    
    [SerializeField] Sprite defaultSprite;

    void Start()
    {
        itemSprite = itemSpriteObject.GetComponent<Image>();
        itemSprite.color = new Color(255, 255, 255, 0);
        currItem = null;
        amount = 0;
        prevSlot = null;
    }

    // Update is called once per frame
    void Update()
    {
        itemSpriteObject.transform.position = Input.mousePosition;
    }

    public bool HoldingItem()
    {
        return currItem != null;
    }

    public string GetItemName()
    {
        if(currItem == null)
        {
            return "ERROR: Not holding item";
        } else
        {
            return currItem.GetName();
        }
    }

    public int GetItemAmount()
    {
        return amount;
    }

    public Item GetItem()
    {
        return currItem;
    }

    public void AddItem(Item item, int itemAmount, GridSlot gridSlot)
    {
        currItem = item;
        amount = itemAmount;
        prevSlot = gridSlot;
        itemSprite.sprite = item.GetSprite();
        itemSprite.color = new Color(255, 255, 255, 255);
    }

    public void RemoveItem()
    {
        currItem = null;
        amount = 0;
        prevSlot = null;
        itemSprite.sprite = defaultSprite;
        itemSprite.color = new Color(255, 255, 255, 0);
    }
}
