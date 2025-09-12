using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectsBackground : MonoBehaviour
{
    [SerializeField] ItemManager itemManager;
    [SerializeField] ItemEffectsManager itemEffectsManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadItemBackground(ItemNameEnum itemName)
    {
        List<ItemEffectEnums> effects = itemManager.GetItemWorldEffects(itemName);

        foreach(ItemEffectEnums effect in effects)
        {
            Instantiate(itemEffectsManager.GetWorldButton(effect), transform);
        }
    }

    public void ClearBackground()
    {
        int childCount = transform.childCount;
        for(int i=0; i<childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
