using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    List<ICombatant> combatants; //First entry is player, rest are enemies

    // Start is called before the first frame update
    void Start()
    {
        do
        {
            //Run the first turn
        } while (((PlayerController)combatants[0]).isAlive()); //Run until one side is completely dead
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
