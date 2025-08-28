using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICombatant
{
    public GameObject player;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //idle animation
    }

    public int Turn()
    {
        //Tell player it's their turn
        //Wait for input
        //Recieve input
        //Send to math screen
        //If fail then miss, otherwise calculate damage
        //return damage
        return 0;
    }

    //Decreases health by damage, to a minimum of 0
    //Precondition: damage is an integer greater than 0
    //Postcondition: returns true if health is greater than 0, false otherwise
    public bool TakeDamage(int damage){
        health -= damage;
        if(health > 0){
            return true;
        } else{
            health = 0;
            return false;
        }
    }

    public int GetHealth(){
        return health;
    }

    public bool isAlive()
    {
        if (health > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
