using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCoffee : MonoBehaviour
{
    ICombatant user;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        //mmmmmm coffee
        user.Rest(25*user.GetLevel());
        user.AddStatusEffect(StatusEffects.CAFFEINATED);
        TurnSystem.inInventory = false;
    }
}
