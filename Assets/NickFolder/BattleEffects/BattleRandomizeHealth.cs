using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRandomizeHealth : MonoBehaviour
{
    [SerializeField] GameObject player;
    ICombatant user;
    // Start is called before the first frame update
    void Start()
    {
        user = player.GetComponent<ICombatant>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        //Randomize health
        user.TakeDamage(user.GetMaxHealth());
        user.Heal(UnityEngine.Random.Range(0, user.GetMaxHealth()));
        TurnSystem.inInventory = false;
    }
}
