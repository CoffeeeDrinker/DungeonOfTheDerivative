using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
public class EnemyController : MonoBehaviour, ICombatant
{
    EnemyPreset preset;
    public GameObject combatController;
    AttackAI personality;
    public GameObject m_enemy;
    public GameObject healthBar;
    public GameObject playerObj;
    int level;
    int maxHealth;
    int maxStamina;
    int health;
    int stamina;
    private float defenseModifier;
    private float attackModifier;
    public int XPWorth;
    private List<Move> moveList;
    private Move lastMove;
    public Dictionary<Move, float> priorities;
    private StatusEffect status;
    private ICombatant player;
    [SerializeField] GameObject statusMarker;
    [SerializeField] GameObject healthBarEmptySpace;
    private double totalHealthBarDisplacement = 0;
    private bool hasStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        OnEnable();
    }


    //Dear future matthew:
    //I changed it so what was in Start is in OnEnable which theoretically should make it so combat restarts whenever you fight a new enemy but ITS NOT WORKING AND ITS ALL BROKEN AAAAAAAA
    void OnEnable() {
        if (!hasStarted)
        {
            hasStarted = true;
            return;
        }
        preset = combatController.GetComponent<EnemyInfoContainer>().GetPreset();
        preset.Initialize();
        player = playerObj.GetComponent<ICombatant>();
        healthBarEmptySpace.SetActive(true);
        statusMarker.SetActive(false);
        level = preset.level;
        maxStamina = preset.maxStamina;
        maxHealth = preset.maxHealth;
        stamina = maxStamina;
        health = maxHealth;
        defenseModifier = preset.defenseModifier;
        attackModifier = preset.attackModifier;
        XPWorth = preset.XPWorth;
        moveList = new List<Move>() //initializes list of moves
        {
            new Move(
                "Rest", //name of move
                Move.REST, //is not an attack
                -1, //undefined stamina cost
                (origin, direction) => //implementation of move
                {
                    origin.Rest(30);
                }),
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
            new Move(
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
                }),
            new Move(
                "Evan Smash",
                Move.DAMAGE, //is an attack
                15, //stamina cost
                (origin, direction) =>
                {
                    int dmg = UnityEngine.Random.Range(25, 30); //randomly generates base damage within pre-defined bounds
                    if (origin.GetStamina() > 15)
                    {
                        direction.TakeDamage(origin.Attack(dmg)); //adds modifiers to base damage from attacking combatant, then deals that much damage to target combatant
                        origin.DepleteStamina(15);
                    }
                }),
        };
            personality = new AttackAI(moveList, this, player, AttackAI.TRICKY);
            priorities = personality.getPriorities();
            healthBarEmptySpace.transform.localScale = new Vector3(((float)(maxHealth - health) / (float)maxHealth), healthBarEmptySpace.transform.localScale.y, healthBarEmptySpace.transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int ICombatant.Attack(int baseDmg)
    {
        int damage = (int)((baseDmg + baseDmg * 0.25 * (level-1)) * attackModifier);
        return damage;
    }

    public void MakeNewMove(ICombatant target)
    {
        //Update rest priority
        priorities = personality.getPriorities();
        /*
        for (int i = 0; i < priorities.Count; i++)
        {
            Debug.Log("Move " + i + " prioritiy: " + priorities[moveList[i]]);
        } */
        //select which move to use
        float selectionRange = 0;
        for (int i = 0; i < moveList.Count; i++)
        {
            if (moveList[i].staminaCost <= stamina) //only consider moves that can be used with current stamina
                selectionRange += priorities[moveList[i]];
        }
        float random = UnityEngine.Random.Range(0, selectionRange);
        for (int i = 0; i < moveList.Count; i++)
        {
                if (random <= priorities[moveList[i]])
                {
                    Debug.Log("Enemy is using: move " + i);
                    Debug.Log("Enemy health: " + health + "\nEnemy stamina: " + stamina);
                    lastMove = moveList[i];
                    lastMove.move(this, target);
                    break;
                }
                else
                {
                    random -= priorities[moveList[i]];
                }
        }
        
    }

    bool ICombatant.IsAlive()
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

    //Decreases health by damage, to a minimum of 0
    //Precondition: damage is an integer greater than 0
    //Postcondition: returns true if health is greater than 0, false otherwise
    bool ICombatant.TakeDamage(int damage)
    {
        damage = (int)(damage/defenseModifier);
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
        float xInit = healthBarEmptySpace.GetComponent<Renderer>().bounds.size.x;
        healthBarEmptySpace.transform.localScale = new Vector3(7.625111f * (((float)(maxHealth - health) / (float)maxHealth)), healthBarEmptySpace.transform.localScale.y, healthBarEmptySpace.transform.localScale.z);
        float xDiff = xInit - healthBarEmptySpace.GetComponent<Renderer>().bounds.size.x;
        healthBarEmptySpace.transform.Translate(0.5f * xDiff, 0, 0);
        totalHealthBarDisplacement += 0.5f * xDiff;
        //healthBar.transform.Translate(damage * (5.71F/maxHealth), 0, 0);
        if (health > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    int ICombatant.GetHealth()
    {
        return health;
    }

    int ICombatant.GetStamina()
    {
        return stamina;
    }

    //Reduces stamina by exhaustion, returns true if there is enough stamina to reduce by exhaustion, false otherwise
    //If exhaustion is greater than stamina, stamina set to 0
    bool ICombatant.DepleteStamina(int exhaustion)
    {
        stamina -= exhaustion;
        if (stamina > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void ICombatant.Rest(int baseRecharge)
    {
        int recharge = baseRecharge;
        stamina += recharge;
    }

    void ICombatant.Win(int XP) {
        Debug.Log("LOOOOOSSSSERRRRR");
    }

    int ICombatant.getXP()
    {
        return XPWorth;
    }

    Move ICombatant.GetLastMove()
    {
        return lastMove;
    }

    bool ICombatant.TurnStart()
    {
        bool skip = false;
        if (status != null)
        {
            skip = status.statusEffect(this);
        }
        return skip;
    }

    void ICombatant.Reset()
    {
        healthBarEmptySpace.transform.Translate(-1f * (float)totalHealthBarDisplacement, 0, 0);
        totalHealthBarDisplacement = 0f;
        status = null;
    }

    void ICombatant.AddStatusEffect(StatusEffect s)
    {
        if (status == null)
        {
            this.status = s;
            statusMarker.SetActive(true);
            statusMarker.GetComponent<SpriteRenderer>().sprite = s.sprite;
        }
    }

    StatusEffect ICombatant.GetStatus()
    {
        return status;
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

    int ICombatant.GetMaxStamina()
    {
        return maxStamina;
    }

    int ICombatant.GetMaxHealth()
    {
        return maxHealth;
    }

    int ICombatant.GetLevel()
    {
        return level;
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
            return true;
        }
        return true;
    }

    float ICombatant.GetAttackModifier()
    {
        return defenseModifier;
    }

    bool ICombatant.SetAttackModifier(float d)
    {
        attackModifier = d;
        if (attackModifier > 1.5)
        {
            attackModifier = 1.5f;
            return false;
        }
        else if (attackModifier < 0.5)
        {
            attackModifier = 0.5f;
            return true;
        }
        return true;
    }

    //Debug method to test selection algorithm and determine distribution of move picks
    /*  private void testMoveProbability()
      {
          int numMove1 = 0;
          int numMove2 = 0;
          int numMove3 = 0;
          int numMove4 = 0;
          int numMove5 = 0;
          for (int j = 0; j < 100000; j++)
          {
              float selectionRange = 0;
              for (int i = 0; i < moves.Count; i++)
              {
                  selectionRange += priorities[moves[i]];
              }
              float random = UnityEngine.Random.Range(0f, selectionRange);
              for (int i = 0; i < moves.Count; i++)
              {
                  if (random <= priorities[moves[i]])
                  {
                      if (i == 0)
                      {
                          numMove1++;
                      }
                      else if (i == 1)
                      {
                          numMove2++;
                      }
                      else if (i == 2)
                      {
                          numMove3++;
                      }
                      else if (i == 3)
                      {
                          numMove4++;
                      }
                      else if (i == 4)
                      {
                          numMove5++;
                      }
                      break;
                  }
                  else
                  {
                      random -= priorities[moves[i]];
                  }
              }
          }
          Debug.Log("Move 1 test: " + numMove1 + "\nMove 2 test: " + numMove2 + "\nMove 3 test: " + numMove3 + "\nMove 4 test:" + numMove4 + "\nMove 5 test: " + numMove5);
      }*/
}

[System.Serializable] public struct EnemyPreset
{
    [SerializeField] public int maxHealth;
    [SerializeField] public int maxStamina;
    [SerializeField] public int level;
    [SerializeField] public float defenseModifier;
    [SerializeField] public float attackModifier;
    [SerializeField] public int XPWorth;
    [SerializeField] GameObject personalityContainer;
    public Algorithm personality;
    public void Initialize()
    {
        personality = personalityContainer.GetComponent<PersonalityHolder>().GetPersonality();
    }
    /*
    public EnemyPreset(int maxHealth, int maxStamina, int level, Algorithm AI)
    {
        this.maxHealth = maxHealth;
        this.maxStamina = maxStamina;
        this.level = level;
        defenseModifier = 1;
        attackModifier = 1;
        XPWorth = level * 100;
        personality = AI;
    }
    public EnemyPreset(int maxHealth, int maxStamina, int level, int XPWorth)
    {
        this.maxHealth = maxHealth;
        this.maxStamina = maxStamina;
        this.level = level;
        defenseModifier = 1;
        attackModifier = 1;
        this.XPWorth = XPWorth;
        personality = AttackAI.MODERATE;
    }

    public EnemyPreset(int maxHealth, int maxStamina, int level, int XPWorth, Algorithm AI)
    {
        this.maxHealth = maxHealth;
        this.maxStamina = maxStamina;
        this.level = level;
        defenseModifier = 1;
        attackModifier = 1;
        this.XPWorth = XPWorth;
        personality = AI;
    }

    public EnemyPreset(int maxHealth, int maxStamina, int level, int XPWorth, float defenseModifier, float attackModifier, Algorithm AI)
    {
        this.maxHealth = maxHealth;
        this.maxStamina = maxStamina;
        this.level = level;
        this.defenseModifier = defenseModifier;
        this.attackModifier = attackModifier;
        this.XPWorth = XPWorth;
        personality = AI;
    } */
}