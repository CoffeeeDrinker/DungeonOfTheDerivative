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
    ItemEffectsBackground itemEffectsBackground;
    GameObject itemEffectsBackgroundImage;
    TextMeshProUGUI amountText;
    [SerializeField] ItemManager itemManager;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] HeldItemManager heldItemManager;
    [SerializeField] SoundManager soundManager;

    void Start()
    {
        currItem = null;
        itemSprite = transform.GetChild(0).GetComponent<Image>();
        amountText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        itemEffectsBackground = transform.GetChild(2).GetComponent<ItemEffectsBackground>();
        itemEffectsBackgroundImage = transform.GetChild(2).gameObject;
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

    public int GetItemAmount()
    {
        return amount;
    }

    public ItemNameEnum GetItemName()
    {
        if(currItem == null)
        {
            return ItemNameEnum.ERROR;
        } else
        {
            return currItem.GetName();
        }
    }

    public void AddItem(Item item, int newAmount)
    {
        if(item.GetName() == ItemNameEnum.fred)
        {
            soundManager.PlaySound(SoundEnums.fredIdle);
        }

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
        } else
        {
            amountText.text = "";
        }
    }

    public void SubtractItem()
    {
        amount--;
        if(amount <= 0)
        {
            RemoveItem();
        } else if(amount > 1)
        {
            amountText.text = amount.ToString();
        } else
        {
            amountText.text = "";
        }
    }

    public void RemoveItem()
    {
        if(currItem != null && currItem.GetName() == ItemNameEnum.fred)
        {
            soundManager.StopSound(SoundEnums.fredIdle);
        }

        currItem = null;
        itemSprite.sprite = defaultSprite;
        itemEffectsBackground.ClearBackground();
        itemEffectsBackgroundImage.SetActive(false);
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
        } else if(currItem != null)
        {
            if(itemEffectsBackgroundImage.activeSelf)
            {
                itemEffectsBackground.ClearBackground();
                itemEffectsBackgroundImage.SetActive(false);
            } else
            {
                itemEffectsBackgroundImage.SetActive(true);
                itemEffectsBackground.LoadItemBackground(currItem.GetName());
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
