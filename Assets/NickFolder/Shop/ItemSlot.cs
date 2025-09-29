using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] ItemManager itemManager;
    [SerializeField] ShopPopUp shopPopUp;
    Image itemSprite;
    ItemNameEnum itemName;

    // Start is called before the first frame update
    void Start()
    {
        itemSprite = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItem(ItemNameEnum item)
    {
        if(itemSprite == null)
        {
            itemSprite = GetComponent<Image>();
        }

        itemSprite.sprite = itemManager.GetSprite(item);
        itemName = item;
    }

    public void OnClick()
    {
        shopPopUp.SetItem(itemName);
    }
}
