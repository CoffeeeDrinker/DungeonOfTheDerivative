using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopUp : MonoBehaviour
{
    ItemNameEnum currItem;
    [SerializeField] Image itemSprite;
    [SerializeField] ItemManager itemManager;
    [SerializeField] TextMeshProUGUI itemPrice;
    [SerializeField] TextMeshProUGUI itemDescription;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItem(ItemNameEnum item)
    {
        currItem = item;
        itemSprite.sprite = itemManager.GetSprite(item);
        itemPrice.text = itemManager.GetPrice(item).ToString();
        itemDescription.text = itemManager.GetDescription(item);
    }
}
