using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICombatant
{
    public GameObject player; //player character
    public GameObject attackButtonField; //serialized field containing attack button
    AttackHandler attackButton; //attack button stored as AttackHandler component
    public GameObject healthBar;
    public GameObject staminaBar;
    private int health;
    private int stamina;
    private int playerLevel = 1;
    private int maxStamina;
    private int maxHealth;
    private int XP = 0;
    private Move lastMove;
    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = (int)(100 + 100*((playerLevel - 1.0) * 0.1));
        maxStamina = (int)(100 + 100 * ((playerLevel - 1.0) * 0.1));
        health = maxHealth;
        stamina = maxStamina;
        attackButton = attackButtonField.GetComponent<AttackHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        //idle animation
    }

    //Calculates damage
    //Postcondition: returns damage dealt as a integer, 0 if attack missed
    int ICombatant.Turn(Move move)
    {
            lastMove = move;
            //return damage
            //SEND TO MATH PROBLEM
            //IF RIGHT ANSWER:
            int damage = ((int)move.GetNewDamage()) * playerLevel;
            //ELSE:
            //damage = 0;
            return damage;
    }

    //Decreases health by damage, to a minimum of 0
    //Precondition: damage is an integer greater than 0
    //Postcondition: returns true if health is greater than 0, false otherwise
    public bool TakeDamage(int damage){
        health -= damage;
        healthBar.transform.Translate(-damage*(5.71F/(float)maxHealth), 0, 0);
        if(health > 0){
            return true;
        } else{
            health = 0;
            return false;
        }
    }

    int ICombatant.GetHealth(){
        return health;
    }

    public bool IsAlive()
    {
        if (health > 0)
        {
            return true;
        }
        else
        {
            health = 0;
            return false;
        }
    }

    //Preconditon: exhaustion is a positive integer
    //Postcondition: stamina is decreased by exhaustion and the stamina bar is shifted accordingly. Returns true if stamina is still greater than 0, false otherwise
    bool ICombatant.DepleteStamina(int exhaustion)
    {
        stamina -= exhaustion;
        staminaBar.transform.Translate(-exhaustion * (5.71F/maxStamina), 0, 0);
        if (stamina > 0)
        {
            return true;
        }
        else {
            stamina = 0;
            return false;
        }
    }

    void ICombatant.Rest(int baseRecharge)
    {
        int recharge = -1*baseRecharge; //make negative recharge positive
        if (stamina+recharge >= maxStamina)
        {
            recharge = maxStamina-stamina;
        }
        stamina += recharge;
        staminaBar.transform.Translate(recharge * (5.71F / maxStamina), 0, 0);
    }

    int ICombatant.getXP()
    {
        throw new System.NotImplementedException();
    }

    void ICombatant.Win(int XP)
    {
        this.XP += XP;
        if(XP >= 100)
        {
            playerLevel++;
            XP -= 100;
        }
        Debug.Log("Level: " + playerLevel);
    }

    int ICombatant.GetStamina()
    {
        return stamina;
    }

    Move ICombatant.GetLastMove()
    {
        return lastMove;
    }
}
