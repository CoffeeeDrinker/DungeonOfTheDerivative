using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] List<string> itemNames;
    [SerializeField] List<Sprite> itemSprites;
    Dictionary<string, Sprite> itemSpriteMap;

    void Start()
    {
        itemSpriteMap = new Dictionary<string, Sprite>();
        for (int i = 0; i < itemNames.Count; i++)
        {
            itemSpriteMap[itemNames[i]] = itemSprites[i];
        }
    }

    public Sprite GetSprite(string name)
    {
        return itemSpriteMap[name];
    }
}
