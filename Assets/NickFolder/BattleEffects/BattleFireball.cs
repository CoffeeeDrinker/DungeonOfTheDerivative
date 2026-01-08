using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFireball : MonoBehaviour
{
    GameObject player;
    GameObject enemy;
    ICombatant user;
    ICombatant target;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerEnemyHolder.instance.player;
        enemy = PlayerEnemyHolder.instance.enemy;
        user = player.GetComponent<ICombatant>();
        target = enemy.GetComponent<ICombatant>();
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
    }
}
