using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectsManager : MonoBehaviour
{
    [SerializeField] List<ItemEffectEnums> itemEffects;
    [SerializeField] List<ItemEffectEnums> itemBattleEffects;
    [SerializeField] List<GameObject> itemWorldButtons;
    [SerializeField] List<GameObject> itemBattleButtons;
    Dictionary<ItemEffectEnums, GameObject> itemWorldButtonsMap;
    Dictionary<ItemEffectEnums, GameObject> itemCombatButtonsMap;

    // Start is called before the first frame update
    void Start()
    {
        itemWorldButtonsMap = new Dictionary<ItemEffectEnums, GameObject>();
        itemCombatButtonsMap = new Dictionary<ItemEffectEnums, GameObject>();

        for (int i = 0; i < itemEffects.Count; i++)
        {
            itemWorldButtonsMap[itemEffects[i]] = itemWorldButtons[i];
        }

        for (int i = 0; i < itemBattleEffects.Count; i++)
        {
            itemCombatButtonsMap[itemBattleEffects[i]] = itemBattleButtons[i];
        }
    }

    public GameObject GetWorldButton(ItemEffectEnums itemEffect)
    {
        return itemWorldButtonsMap[itemEffect];
    }

    public GameObject GetCombatButton(ItemEffectEnums itemEffect)
    {
        return itemCombatButtonsMap[itemEffect];
    }
}
