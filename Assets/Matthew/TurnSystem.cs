using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    ICombatant player;
    ICombatant enemy;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        enemy = GetComponent<EnemyController>();

        do
        {
            enemy.TakeDamage(player.Turn());
            if (enemy.IsAlive())
            {
                player.TakeDamage(enemy.Turn());
            }
            else
            {
                //add xp, leave encounter, break
            }
        } while (player.IsAlive()); //Run until one side is completely dead
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
