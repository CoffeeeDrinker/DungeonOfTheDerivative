using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class PlayerController : MonoBehaviour, ICombatant
{
    public GameObject player; //player character
    public GameObject attackButtonField; //serialized field containing attack button
    AttackHandler attackButton; //attack button stored as AttackHandler component
    public GameObject healthBar;
    public GameObject staminaBar;
    [SerializeField] GameObject statusMarker;
    [SerializeField] GameObject healthBarEmptySpace;
    [SerializeField] GameObject staminaBarEmptySpace;
    private StatusEffect status = null;
    private int health;
    private int stamina;
    [SerializeField] int playerLevel;
    private int maxStamina;
    private int maxHealth;
    private int XP = 0;

    private Move lastMove;
    public List<Move> moveList;
    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {

        statusMarker.SetActive(false);
        healthBarEmptySpace.SetActive(true);
        staminaBarEmptySpace.SetActive(true);
        maxHealth = (int)(100 + 100*((playerLevel - 1.0) * 0.1));
        maxStamina = (int)(100 + 100 * ((playerLevel - 1.0) * 0.1));
        health = maxHealth;
        stamina = maxStamina;
        healthBarEmptySpace.transform.localScale = new Vector3(((float)(maxHealth - health) / (float)maxHealth), healthBarEmptySpace.transform.localScale.y, healthBarEmptySpace.transform.localScale.z);
        staminaBarEmptySpace.transform.localScale = new Vector3(((float)(maxStamina - stamina) / (float)maxStamina), staminaBarEmptySpace.transform.localScale.y, staminaBarEmptySpace.transform.localScale.z);
        attackButton = attackButtonField.GetComponent<AttackHandler>();
        moveList = new List<Move>() //initializes list of moves
        {
            new Move(
                "Punch",
                true, //is an attack
                (origin, direction) =>
                {
                    int dmg = UnityEngine.Random.Range(9, 11); //randomly generates base damage within pre-defined bounds
                    if (origin.GetStamina() > 15)
                    {
                        direction.TakeDamage(origin.Attack(dmg)); //adds modifiers to base damage from attacking combatant, then deals that much damage to target combatant
                        origin.DepleteStamina(15);
                    }
                }),
            new Move(
                "Lulaby",
                true, //is an attack
                (origin, direction) =>
                {
                    if (origin.GetStamina() > 15)
                    {
                        direction.AddStatusEffect(StatusEffects.ASLEEP);
                        origin.DepleteStamina(15);
                    }
                }),
            new Move(
                "Iron Stare",
                true, //is an attack
                (origin, direction) =>
                {
                    int dmg = UnityEngine.Random.Range(5, 10); //randomly generates base damage within pre-defined bounds
                    if (origin.GetStamina() > 15)
                    {
                        if(UnityEngine.Random.Range(0f, 10f) > 5)
                        {
                            direction.AddStatusEffect(StatusEffects.PARALYZED);
                        }
                        origin.DepleteStamina(15);
                    }
                }),
            new Move(
                "Evan Smash",
                true, //is an attack
                (origin, direction) =>
                {
                    int dmg = UnityEngine.Random.Range(25, 75); //randomly generates base damage within pre-defined bounds
                    if (origin.GetStamina() > 15)
                    {
                        direction.TakeDamage(origin.Attack(dmg)); //adds modifiers to base damage from attacking combatant, then deals that much damage to target combatant
                        origin.DepleteStamina(15);
                    }
                }),
        };
    }

    // Update is called once per frame
    void Update()
    {
        //idle animation
    }

    //Calculates damage
    //Postcondition: returns damage dealt as a integer, 0 if attack missed
    int ICombatant.Attack(int baseDmg)
    {
            //lastMove = move;

            //return damage after modifiers
            int damage = baseDmg * playerLevel;
            return damage;
    }

    //Decreases health by damage, to a minimum of 0
    //Precondition: damage is an integer greater than 0
    //Postcondition: returns true if health is greater than 0, false otherwise
    public bool TakeDamage(int damage){
        health -= damage;
        if(health < 0)
        {
            health = 0;
        }
        float xInit = healthBarEmptySpace.GetComponent<Renderer>().bounds.size.x;
        healthBarEmptySpace.transform.localScale = new Vector3(7.625111f * (((float)(maxHealth - health) / (float)maxHealth)), healthBarEmptySpace.transform.localScale.y, healthBarEmptySpace.transform.localScale.z);
        float xDiff = xInit - healthBarEmptySpace.GetComponent<Renderer>().bounds.size.x;
        healthBarEmptySpace.transform.Translate(0.5f*xDiff, 0, 0);
        //healthBar.transform.Translate(-damage*(5.71F/(float)maxHealth), 0, 0);
        if (health > 0){
            return true;
        } else{
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
        if(stamina < 0)
        {
            stamina = 0;
        }
        float xInit = staminaBarEmptySpace.GetComponent<Renderer>().bounds.size.x;
        staminaBarEmptySpace.transform.localScale = new Vector3(7.625111f * (((float)(maxStamina - stamina) / (float)maxStamina)), staminaBarEmptySpace.transform.localScale.y, staminaBarEmptySpace.transform.localScale.z);
        float xDiff = xInit - staminaBarEmptySpace.GetComponent<Renderer>().bounds.size.x;
        staminaBarEmptySpace.transform.Translate(0.5f * xDiff, 0, 0);
        //staminaBar.transform.Translate(-exhaustion * (5.71F/maxStamina), 0, 0);
        if (stamina > 0)
        {
            return true;
        }
        else {
            return false;
        }
    }

    void ICombatant.Rest(int baseRecharge)
    {
        int recharge = baseRecharge; //make negative recharge positive
        if (stamina+recharge >= maxStamina)
        {
            recharge = maxStamina-stamina;
        }
        float xInit = staminaBarEmptySpace.GetComponent<Renderer>().bounds.size.x;
        stamina += recharge;
        staminaBarEmptySpace.transform.localScale = new Vector3(7.625111f * (((float)(maxStamina - stamina) / (float)maxStamina)), staminaBarEmptySpace.transform.localScale.y, staminaBarEmptySpace.transform.localScale.z);
        float xDiff = staminaBarEmptySpace.GetComponent<Renderer>().bounds.size.x - xInit;
        staminaBarEmptySpace.transform.Translate(-0.5f * xDiff, 0, 0);
        //staminaBar.transform.Translate(recharge * (5.71F / maxStamina), 0, 0);
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

    bool ICombatant.TurnStart()
    {
        bool skip = false;
        if(status != null)
        {
            skip = status.statusEffect(this); //status effect does its thing on the player, if it skips turn sets skip to true, false otherwise
        }
        return skip;
    }

    void ICombatant.MakeNewMove(ICombatant x)
    {
        throw new System.NotImplementedException();
    }

    void ICombatant.AddStatusEffect(StatusEffect s)
    {
        if(status == null) //only sets status if not already set
            status = s;
        statusMarker.SetActive(true);
    }

    int ICombatant.GetLevel()
    {
        return playerLevel;
    }
    StatusEffect ICombatant.GetStatus()
    {
        return status;
    }

    int ICombatant.GetMaxStamina()
    {
        return maxStamina;
    }

    int ICombatant.GetMaxHealth()
    {
        return maxHealth;
    }

    public List<Move> GetMoveList()
    {
        return moveList;
    }

    void ICombatant.ClearStatusEffects()
    {
        status = null;
        statusMarker.SetActive(false);
    }

    void ICombatant.Heal(int health)
    {
        this.health += health;
    }

}
