using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    string itemName;
    Sprite itemSprite;

    public void SetName(string name)
    {
        itemName = name;
    }

    public void SetSprite(Sprite sprite)
    {
        itemSprite = sprite;
    }

    public string GetName()
    {
        return itemName;
    }

    public Sprite GetSprite()
    {
        return itemSprite;
    }
}
