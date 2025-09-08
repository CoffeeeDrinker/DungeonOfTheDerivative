using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridSlot : MonoBehaviour
{
    Item currItem;
    int amount;
    Image itemSprite;
    TextMeshProUGUI amountText;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] HeldItemManager heldItemManager;

    void Start()
    {
        currItem = null;
        itemSprite = transform.GetChild(0).GetComponent<Image>();
        amountText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        itemSprite.sprite = defaultSprite;
        amount = 0;
    }


    void Update()
    {
        
    }

    public bool HasItem()
    {
        Debug.Log(currItem);
        return currItem != null;
    }

    public string GetItemName()
    {
        if(currItem == null)
        {
            return "";
        } else
        {
            return currItem.GetName();
        }
    }

    public void AddItem(Item item, int newAmount)
    {
        if(currItem != null && currItem.GetName() == item.GetName())
        {
            amount += newAmount;
        } else
        {
            currItem = item;
            amount = newAmount;
            itemSprite.sprite = item.GetSprite();
        }
       
        if(amount > 1)
        {
            amountText.text = amount.ToString();
        }
    }

    public void RemoveItem()
    {
        currItem = null;
        itemSprite.sprite = defaultSprite;
        amount = 0;
        amountText.text = "";
    }

    public void OnClick(BaseEventData dataStuff)
    {
        PointerEventData pointerDataStuff = dataStuff as PointerEventData;
         
        if(pointerDataStuff.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        } else if(pointerDataStuff.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    void OnRightClick()
    {
        if(heldItemManager.HoldingItem())
        {
            if(currItem != null && heldItemManager.GetItemName() != currItem.GetName())
            {
                return;
            }
            AddItem(heldItemManager.GetItem(), 1);
            if(heldItemManager.GetItemAmount() == 1)
            {
                heldItemManager.RemoveItem();
            } else
            {
                heldItemManager.AddItem(currItem, heldItemManager.GetItemAmount() - 1, GetComponent<GridSlot>());
            }
        }
    }

    void OnLeftClick()
    {
        if (heldItemManager.HoldingItem())
        {
            if (currItem == null || heldItemManager.GetItemName() == currItem.GetName())
            {
                AddItem(heldItemManager.GetItem(), heldItemManager.GetItemAmount());
                heldItemManager.RemoveItem();
            }
            else
            {
                Item item = heldItemManager.GetItem();
                int tempAmount = heldItemManager.GetItemAmount();
                heldItemManager.RemoveItem();
                heldItemManager.AddItem(currItem, amount, GetComponent<GridSlot>());
                RemoveItem();
                AddItem(item, tempAmount);
            }
        }
        else
        {
            if (currItem == null)
            {
                return;
            }

            heldItemManager.AddItem(currItem, amount, GetComponent<GridSlot>());
            RemoveItem();
        }
    }

}
