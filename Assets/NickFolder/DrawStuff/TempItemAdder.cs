using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempItemAdder : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory.AddItem(ItemNameEnum.healthPotion, 2);
        inventory.AddItem(ItemNameEnum.fred, 1);
        inventory.AddItem(ItemNameEnum.bomb, 5);
        inventory.AddItem(ItemNameEnum.staminaPotion, 1);
        inventory.AddItem(ItemNameEnum.redCards, 9);
        inventory.AddItem(ItemNameEnum.blueCards, 5);
        inventory.AddItem(ItemNameEnum.superStaminaPotion, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
