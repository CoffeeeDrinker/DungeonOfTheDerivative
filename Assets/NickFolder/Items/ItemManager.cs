using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemManager : MonoBehaviour
{
    [SerializeField] List<ItemNameEnum> itemNames;
    [SerializeField] List<Sprite> itemSprites;

    Dictionary<ItemNameEnum, Sprite> itemSpriteMap;
    Dictionary<ItemNameEnum, List<ItemEffectEnums>> itemEffectWorldMap;

    void Start()
    {
        itemSpriteMap = new Dictionary<ItemNameEnum, Sprite>();

        for (int i = 0; i < itemNames.Count; i++)
        {
            itemSpriteMap[itemNames[i]] = itemSprites[i];
        }

        LoadItemEffectWorldMap();
    }

    public Sprite GetSprite(ItemNameEnum name)
    {
        return itemSpriteMap[name];
    }

    public List<ItemEffectEnums> GetItemWorldEffects(ItemNameEnum name)
    {
        return itemEffectWorldMap[name];
    }

    void LoadItemEffectWorldMap()
    {
        itemEffectWorldMap = new Dictionary<ItemNameEnum, List<ItemEffectEnums>>();
        List<ItemEffectEnums> itemEffects = new List<ItemEffectEnums>();
        //Health potion
        itemEffects.Add(ItemEffectEnums.heal);
        itemEffects.Add(ItemEffectEnums.discard);
        itemEffectWorldMap[ItemNameEnum.healthPotion] = itemEffects;
        //
    }
}
