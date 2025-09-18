using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    ItemNameEnum itemName;
    Sprite itemSprite;

    public void SetName(ItemNameEnum name)
    {
        itemName = name;
    }

    public void SetSprite(Sprite sprite)
    {
        itemSprite = sprite;
    }

    public ItemNameEnum GetName()
    {
        return itemName;
    }

    public Sprite GetSprite()
    {
        return itemSprite;
    }
}
