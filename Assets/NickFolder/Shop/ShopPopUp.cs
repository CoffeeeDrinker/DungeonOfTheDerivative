using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopUp : MonoBehaviour
{
    ItemNameEnum currItem;
    Shop shop;
    [SerializeField] Image itemSprite;
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
}
