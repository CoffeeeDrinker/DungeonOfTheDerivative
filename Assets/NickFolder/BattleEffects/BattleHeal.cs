using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHeal : MonoBehaviour
{
    GameObject player;
    ICombatant user;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerEnemyHolder.instance.player;
        user = player.GetComponent<ICombatant>();
    }

    // Update is called once per frame
    void Update()
    {
        user = GameObject.FindWithTag("Player").GetComponent<ICombatant>();
    }

    public void OnClick()
    {
        //Heal stuff AAAAAAAAAA
        user.Heal(40 * user.GetLevel());
        TurnSystem.inInventory = false;
    }
}
