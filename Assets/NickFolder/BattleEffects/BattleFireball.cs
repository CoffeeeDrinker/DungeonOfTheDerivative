using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFireball : MonoBehaviour
{
    ICombatant user;
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
        target.TakeDamage(80 * user.GetLevel());
        target.AddStatusEffect(StatusEffects.BURNING);
        //FIREBALL AAAAAAAAAA FIRE FIRE FIRE FIRE FIRE
        TurnSystem.inInventory = false;
        TurnSystem.itemUseText = "You cast fireball at your opponent!";
    }
}
