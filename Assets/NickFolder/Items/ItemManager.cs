using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemManager : MonoBehaviour
{
    [SerializeField] List<ItemNameEnum> itemNames;
    [SerializeField] List<Sprite> itemSprites;
    [SerializeField] List<int> itemPrices;
    [SerializeField] List<string> itemDescriptions;

    public bool inCombat;
    Dictionary<ItemNameEnum, int> itemPriceMap;
    Dictionary<ItemNameEnum, string> itemDescriptionsMap;
    Dictionary<ItemNameEnum, Sprite> itemSpriteMap;
    Dictionary<ItemNameEnum, List<ItemEffectEnums>> itemEffectWorldMap;
    Dictionary<ItemNameEnum, List<ItemEffectEnums>> itemEffectCombatMap;

    void Start()
    {
        itemSpriteMap = new Dictionary<ItemNameEnum, Sprite>();
        itemPriceMap = new Dictionary<ItemNameEnum, int>();
        itemDescriptionsMap = new Dictionary<ItemNameEnum, string>();

        for (int i = 0; i < itemNames.Count; i++)
        {
            itemSpriteMap[itemNames[i]] = itemSprites[i];
            itemPriceMap[itemNames[i]] = itemPrices[i];
            itemDescriptionsMap[itemNames[i]] = itemDescriptions[i];
        }

        LoadItemEffectWorldMap();
        LoadItemEffectBattleMap();
    }

    public Sprite GetSprite(ItemNameEnum name)
    {
        return itemSpriteMap[name];
    }

    public int GetPrice(ItemNameEnum name)
    {
        return itemPriceMap[name];
    }

    public string GetDescription(ItemNameEnum name)
    {
        return itemDescriptionsMap[name];
    }

    public List<ItemEffectEnums> GetItemWorldEffects(ItemNameEnum name)
    {
        return itemEffectWorldMap[name];
    }

    public List<ItemEffectEnums> GetItemCombatEffects(ItemNameEnum name)
    {
        return itemEffectCombatMap[name];
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
        itemEffects.Add(ItemEffectEnums.kill);
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

        //Fireball
        itemEffects = new List<ItemEffectEnums>();
        itemEffects.Add(ItemEffectEnums.discard);
        itemEffectWorldMap[ItemNameEnum.fireball] = itemEffects;

        //Coffee
        itemEffects = new List<ItemEffectEnums>();
        itemEffects.Add(ItemEffectEnums.drinkCoffee);
        itemEffects.Add(ItemEffectEnums.discard);
        itemEffectWorldMap[ItemNameEnum.coffee] = itemEffects;
    }

    void LoadItemEffectBattleMap()
    {
        itemEffectCombatMap = new Dictionary<ItemNameEnum, List<ItemEffectEnums>>();

        //Health Potion
        List<ItemEffectEnums> itemEffects = new List<ItemEffectEnums>();
        itemEffects.Add(ItemEffectEnums.heal);
        itemEffectCombatMap[ItemNameEnum.healthPotion] = itemEffects;

        //Bomb
        itemEffects = new List<ItemEffectEnums>();
        itemEffects.Add(ItemEffectEnums.blowUp);
        itemEffectCombatMap[ItemNameEnum.bomb] = itemEffects;

        //Blue Cards
        itemEffects = new List<ItemEffectEnums>();
        itemEffects.Add(ItemEffectEnums.randomizeStamina);
        itemEffectCombatMap[ItemNameEnum.blueCards] = itemEffects;

        //Fred
        itemEffects = new List<ItemEffectEnums>();
        itemEffectCombatMap[ItemNameEnum.fred] = itemEffects;

        //Red Cards
        itemEffects = new List<ItemEffectEnums>();
        itemEffects.Add(ItemEffectEnums.randomizeStamina);
        itemEffectCombatMap[ItemNameEnum.redCards] = itemEffects;

        //Stamina Potion
        itemEffects = new List<ItemEffectEnums>();
        itemEffects.Add(ItemEffectEnums.stamina);
        itemEffectCombatMap[ItemNameEnum.staminaPotion] = itemEffects;

        //Super Stamina Potion
        itemEffects = new List<ItemEffectEnums>();
        itemEffects.Add(ItemEffectEnums.superStamina);
        itemEffectCombatMap[ItemNameEnum.superStaminaPotion] = itemEffects;

        //Fireball
        itemEffects = new List<ItemEffectEnums>();
        itemEffects.Add(ItemEffectEnums.fireball);
        itemEffectCombatMap[ItemNameEnum.fireball] = itemEffects;

        //Coffee
        itemEffects = new List<ItemEffectEnums>();
        itemEffects.Add(ItemEffectEnums.drinkCoffee);
        itemEffectCombatMap[ItemNameEnum.coffee] = itemEffects;
        
    }

    public void EnterCombat()
    {
        inCombat = true;
    }

    public void ExitCombat()
    {
        inCombat = false;
    }

    public bool InCombat()
    {
        return inCombat;
    }
}
