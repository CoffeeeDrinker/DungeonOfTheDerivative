using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRandomizeStamina : MonoBehaviour
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
        //Randomize Stamina
        user.DepleteStamina(user.GetMaxStamina());
        user.Rest(UnityEngine.Random.Range(0, user.GetMaxStamina()));
        TurnSystem.inInventory = false;
    }
}
