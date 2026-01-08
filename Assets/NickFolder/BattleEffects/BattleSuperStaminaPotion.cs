using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSuperStaminaPotion : MonoBehaviour
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
        
    }

    public void OnClick()
    {
        user.Rest(user.GetMaxStamina()); //restores stamina to full
        TurnSystem.inInventory = false;
    }
}
 