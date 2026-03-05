using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRegStaminaPoiton : MonoBehaviour
{
    ICombatant user;
    // Start is called before the first frame update
    void Start()
    {
        user = GameObject.FindWithTag("Player").GetComponent<ICombatant>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        user.Rest(60 * user.GetLevel());
        TurnSystem.inInventory = false;
    }
}
