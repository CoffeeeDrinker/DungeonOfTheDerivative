using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectsManager : MonoBehaviour
{
    [SerializeField] List<ItemEffectEnums> itemEffects;
    [SerializeField] List<GameObject> itemWorldButtons;
    Dictionary<ItemEffectEnums, GameObject> itemWorldButtonsMap;

    // Start is called before the first frame update
    void Start()
    {
        itemWorldButtonsMap = new Dictionary<ItemEffectEnums, GameObject>();
        for(int i=0; i<itemEffects.Count; i++)
        {
            itemWorldButtonsMap[itemEffects[i]] = itemWorldButtons[i];
        }
    }

    public GameObject GetWorldButton(ItemEffectEnums itemEffect)
    {
        return itemWorldButtonsMap[itemEffect];
    }
}
