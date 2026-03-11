using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBlowUp : MonoBehaviour
{
    [SerializeField] ICombatant user;
    ICombatant target;
    // Start is called before the first frame update
    void Start()
    {
        user = GameObject.FindWithTag("Player").GetComponent<ICombatant>();
        target = GameObject.FindWithTag("Enemy").GetComponent<ICombatant>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        //Blow up D:
        user.TakeDamage(user.GetMaxHealth());
        target.TakeDamage(target.GetMaxHealth());
        TurnSystem.inInventory = false;
        TurnSystem.itemUseText = "You detonated a bomb!";
    }
}
