using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] List<ItemSlot> itemSlots;
    [SerializeField] List<ItemNameEnum> possibleItems;
    [SerializeField] ItemManager itemManager;
    int shopPriceMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        shopPriceMultiplier = 1;
        LoadShop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadShop()
    {
        List<ItemNameEnum> tempList = new List<ItemNameEnum>(possibleItems);

        for(int i=0; i<6; i++)
        {
            int index = Random.Range(0, tempList.Count);
            itemSlots[i].SetItem(tempList[index]);
            tempList.RemoveAt(index);
        }

    }

    //Killing Fred makes everything cost twice as much
    public int GetShopPriceMultiplier()
    {
        return shopPriceMultiplier;
    }

    public void SetShopPriceMultiplier(int shopPriceMultiplier)
    {
        this.shopPriceMultiplier = shopPriceMultiplier;
    }
}
