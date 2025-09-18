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

        //Health Potion
        List<ItemEffectEnums> itemEffects = new List<ItemEffectEnums>();
        itemEffects.Add(ItemEffectEnums.heal);
        itemEffects.Add(ItemEffectEnums.discard);
        itemEffectWorldMap[ItemNameEnum.healthPotion] = itemEffects;

        //Bomb
        itemEffects = new List<ItemEffectEnums>();
        itemEffects.Add(ItemEffectEnums.blowUp);
        itemEffects.Add(ItemEffectEnums.discard);
        itemEffectWorldMap[ItemNameEnum.bomb] = itemEffects;

        //Blue Cards
        itemEffects = new List<ItemEffectEnums>();
        itemEffects.Add(ItemEffectEnums.discard);
        itemEffectWorldMap[ItemNameEnum.blueCards] = itemEffects;

        //Fred
        itemEffects = new List<ItemEffectEnums>();
        itemEffects.Add(ItemEffectEnums.pet);
        itemEffects.Add(ItemEffectEnums.discard);
        itemEffectWorldMap[ItemNameEnum.fred] = itemEffects;

        //Red Cards
        itemEffects = new List<ItemEffectEnums>();
        itemEffects.Add(ItemEffectEnums.discard);
        itemEffectWorldMap[ItemNameEnum.redCards] = itemEffects;

        //Stamina Potion
        itemEffects = new List<ItemEffectEnums>();
        itemEffects.Add(ItemEffectEnums.discard);
        itemEffectWorldMap[ItemNameEnum.staminaPotion] = itemEffects;

        //Super Stamina Potion
        itemEffects = new List<ItemEffectEnums>();
        itemEffects.Add(ItemEffectEnums.discard);
        itemEffectWorldMap[ItemNameEnum.superStaminaPotion] = itemEffects;
    }
}
