using System;
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
    private SpriteRenderer statusRenderer;
    [SerializeField] GameObject healthBarEmptySpace;
    [SerializeField] GameObject staminaBarEmptySpace;
    private float totalStaminaDisplacement = 0;
    private StatusEffect status = null;
    private int health;
    private int stamina;
    private float defenseModifier = 1;
    private float attackModifier = 1;
    [SerializeField] int playerLevel;
    private int maxStamina;
    private int maxHealth;
    private int XP = 0;
    private Move lastMove;
    public List<Move> moveList;
    void Start()
    {
        statusRenderer = statusMarker.GetComponent<SpriteRenderer>();
        statusRenderer.enabled = true;
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
                Move.DAMAGE, //is an attack
                15, //stamina cost
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
                Move.STATUS, //is an attack
                15, //stamina cost
                (origin, direction) =>
                {
                    if (origin.GetStamina() > 15)
                    {
                        direction.AddStatusEffect(StatusEffects.ASLEEP);
                        origin.DepleteStamina(15);
                    }
                }),
            /*new Move(
                "Iron Stare",
                Move.STATUS, //is an attack
                15, //stamina cost
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
                }), */
            Moves.POLAR,
            new Move(
                "Evan Smash",
                Move.DAMAGE, //is an attack
                15, //stamina cost
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
            int damage = (int)(baseDmg * playerLevel * attackModifier);
            return damage;
    }

    //Decreases health by damage, to a minimum of 0
    //Precondition: damage is an integer greater than 0
    //Postcondition: returns true if health is greater than 0, false otherwise
    public bool TakeDamage(int damage){
        damage = (int)(damage / defenseModifier);
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
        totalStaminaDisplacement += 0.5f * xDiff;
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
    /*
    private IEnumerator ChangeStaminaBar(int staminaDiff)
    {
        for(int i = 1; i <= staminaDiff; i++)
        {
            float xInit = staminaBarEmptySpace.GetComponent<Renderer>().bounds.size.x;
            staminaBarEmptySpace.transform.localScale = new Vector3(7.625111f * (((float)(stamina+i) / (float)maxStamina)), staminaBarEmptySpace.transform.localScale.y, staminaBarEmptySpace.transform.localScale.z);
            float xDiff = staminaBarEmptySpace.GetComponent<Renderer>().bounds.size.x - xInit;
            staminaBarEmptySpace.transform.Translate(-0.5f * xDiff, 0, 0);
            for (int i = 0; i < 5; i++) { yield return null; } //waits 5 frames between updating stamina bar
        }
    } */

    int ICombatant.getXP()
    {
        throw new System.NotImplementedException();
    }

    void ICombatant.Win(int XP)
    {
        int XPBase = 1000;
        this.XP += XP;
        if(XP >= XPBase*playerLevel)
        {
            XP -= XPBase * playerLevel;
            playerLevel++;
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
        if (status == null) //only sets status if not already set
        {
            status = s;
            statusMarker.SetActive(true);
            statusRenderer.enabled = true;
            statusRenderer.sprite = s.sprite;
        }
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

    void ICombatant.Heal(int heal)
    {
        if (health + heal >= maxHealth)
        {
            heal = maxHealth - health;
        }
        float xInit = healthBarEmptySpace.GetComponent<Renderer>().bounds.size.x;
        health += heal;
        healthBarEmptySpace.transform.localScale = new Vector3(7.625111f * (((float)(maxHealth - health) / (float)maxHealth)), healthBarEmptySpace.transform.localScale.y, healthBarEmptySpace.transform.localScale.z);
        float xDiff = healthBarEmptySpace.GetComponent<Renderer>().bounds.size.x - xInit;
        healthBarEmptySpace.transform.Translate(-0.5f * xDiff, 0, 0);
    }

    float ICombatant.GetDefense()
    {
        return defenseModifier;
    }

    bool ICombatant.SetDefense(float d)
    {
        defenseModifier = d;
        if (defenseModifier > 2)
        {
            defenseModifier = 2f;
            return false;
        } else if(defenseModifier < 0.5)
        {
            defenseModifier = 0.5f;
            return false;
        }
        return true;
    }

    float ICombatant.GetAttackModifier()
    {
        return attackModifier;
    }

    bool ICombatant.SetAttackModifier(float a)
    {
        attackModifier = a;
        if (attackModifier > 1.5)
        {
            attackModifier = 1.5f;
            return false;
        }
        else if (attackModifier < 0.5)
        {
            attackModifier = 0.5f;
            return false;
        }
        return true;
    }

    void ICombatant.Reset()
    {
        //reset stamina and stamina bar
        stamina = maxStamina;
        staminaBarEmptySpace.transform.Translate(-1 * totalStaminaDisplacement, 0, 0);
        staminaBarEmptySpace.transform.localScale = new Vector3(((float)(maxStamina - stamina) / (float)maxStamina), staminaBarEmptySpace.transform.localScale.y, staminaBarEmptySpace.transform.localScale.z);
        totalStaminaDisplacement = 0;
    }
}
