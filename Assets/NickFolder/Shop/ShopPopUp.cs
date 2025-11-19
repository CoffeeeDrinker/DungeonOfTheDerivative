using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopUp : MonoBehaviour
{
    ItemNameEnum currItem = ItemNameEnum.ERROR;
    Shop shop;
    [SerializeField] Image itemSprite;
    [SerializeField] Grid grid;
    [SerializeField] ItemManager itemManager;
    [SerializeField] TextMeshProUGUI itemPrice;
    [SerializeField] TextMeshProUGUI itemDescription;
    
    // Start is called before the first frame update
    void Start()
    {
        shop = GetComponentInParent<Shop>();
        itemSprite.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItem(ItemNameEnum item)
    {
        currItem = item;
        itemSprite.sprite = itemManager.GetSprite(item);
        itemSprite.color = new Color(255, 255, 255, 255);
        itemPrice.text = (itemManager.GetPrice(item) * shop.GetShopPriceMultiplier()).ToString();
        itemDescription.text = itemManager.GetDescription(item);
    }

    public void BuyShownItem()
    {
        if(currItem == ItemNameEnum.ERROR)
        {
            return;
        }

        int price = int.Parse(itemPrice.text);

        if(PlayerManager.ducks < price)
        {
            return;
        }

        PlayerManager.ducks -= price;

        Item item = gameObject.AddComponent<Item>();
        item.SetName(currItem);
        item.SetSprite(itemManager.GetSprite(item.GetName()));
        grid.AddItem(item, 1);
    }
}
