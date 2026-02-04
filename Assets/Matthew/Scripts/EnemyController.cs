using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class EnemyController : MonoBehaviour, ICombatant
{
    AttackAI personality;
    public GameObject m_enemy;
    public GameObject healthBar;
    public GameObject playerObj;
    [SerializeField] int level;
    int maxHealth;
    int maxStamina;
    int health;
    int stamina;
    public readonly int XPWorth;
    private List<Move> moveList;
    private Move lastMove;
    public Dictionary<Move, float> priorities;
    private StatusEffect status;
    private ICombatant player;
    [SerializeField] GameObject statusMarker;
    [SerializeField] GameObject healthBarEmptySpace;
    // Start is called before the first frame update
    void Start()
    {
        player = playerObj.GetComponent<ICombatant>();
        healthBarEmptySpace.SetActive(true);
        statusMarker.SetActive(false);
        maxStamina = (int)(100 + 100 * ((level - 1.0) * 0.1));
        maxHealth = (int)(100 + 100 * ((level - 1.0) * 0.1));
        stamina = maxStamina;
        health = maxHealth;
        moveList = new List<Move>() //initializes list of moves
        {
            new Move(
                "Rest", //name of move
                Move.REST, //is not an attack
                (origin, direction) => //implementation of move
                {
                    origin.Rest(30);
                }),
            new Move(
                "Punch",
                Move.DAMAGE, //is an attack
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
        double modifier = UnityEngine.Random.Range(0.1F, 2.0F);
        int damage = (int)(baseDmg * modifier);
        //Calculate total damage as totalDmg*modifier
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
        Debug.Log("MoveList: " + moveList.Count);
        for (int i = 0; i < moveList.Count; i++)
        {
            //if (moveList[i].staminaCost <= stamina) //only consider moves that can be used with current stamina
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
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
        float xInit = healthBarEmptySpace.GetComponent<Renderer>().bounds.size.x;
        healthBarEmptySpace.transform.localScale = new Vector3(7.625111f * (((float)(maxHealth - health) / (float)maxHealth)), healthBarEmptySpace.transform.localScale.y, healthBarEmptySpace.transform.localScale.z);
        float xDiff = xInit - healthBarEmptySpace.GetComponent<Renderer>().bounds.size.x;
        healthBarEmptySpace.transform.Translate(0.5f * xDiff, 0, 0);
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

    void ICombatant.AddStatusEffect(StatusEffect s)
    {
        if(status == null)
            this.status = s;
        statusMarker.SetActive(true);
        //statusMarker.GetComponent<SpriteRenderer>().sprite = s.sprite;
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

struct AttackAI
{
    public static Algorithm AGGRESSIVE = new Algorithm("Aggressive", (moves, attacker, opponent) =>
    {
        Dictionary<Move, float> priorityDict = new Dictionary<Move, float>();
        for(int i = 0; i < moves.Count; i++)
        {
            priorityDict[moves[i]] = 1.0f;
            if (moves[i].type == Move.DAMAGE)
            {
                priorityDict[moves[i]] += 0.2f;
            }
            else if (moves[i].type == Move.STATUS && opponent.GetStatus() != null)
            {
                priorityDict[moves[i]] = 0;
            } else if (moves[i].type == Move.REST)
            {
                priorityDict[moves[i]] = (attacker.GetMaxStamina() - attacker.GetStamina()) * 0.01f;
            }
            else
            {
                priorityDict[moves[i]] -= 0.1f;
            }
        }
        return priorityDict;
    });
    public static Algorithm MODERATE = new Algorithm("Moderate", (moves, attacker, opponent) =>
    {
        Dictionary<Move, float> priorityDict = new Dictionary<Move, float>();
        for (int i = 0; i < moves.Count; i++)
        {
            priorityDict[moves[i]] = 1.0f;
            if (moves[i].type == Move.DAMAGE)
            {
                priorityDict[moves[i]] += 0.1f;
            }
            else if (moves[i].type == Move.STATUS && opponent.GetStatus() != null)
            {
                priorityDict[moves[i]] = 0;
            }
            else if (moves[i].type == Move.REST)
            {
                priorityDict[moves[i]] = (attacker.GetMaxStamina() - attacker.GetStamina()) * 0.01f;
            }
        }
        return priorityDict;
    });
    public static Algorithm CAUTIOUS = new Algorithm("Cautious", (moves, attacker, opponent) =>
    {
        Dictionary<Move, float> priorityDict = new Dictionary<Move, float>();
        for (int i = 0; i < moves.Count; i++)
        {
            priorityDict[moves[i]] = 1.0f;
            if (moves[i].type == Move.BUFF)
            {
                priorityDict[moves[i]] += 0.2f;
            }
            else if (moves[i].type == Move.STATUS && opponent.GetStatus() != null)
            {
                priorityDict[moves[i]] = 0;
            }
            else if (moves[i].type == Move.REST)
            {
                priorityDict[moves[i]] = (attacker.GetMaxStamina() - attacker.GetStamina()) * 0.01f;
            }
            else
            {
                priorityDict[moves[i]] -= 0.1f;
            }
        }
        return priorityDict;
    });
    public static Algorithm TRICKY = new Algorithm("Tricky", (moves, attacker, opponent) =>
    {
        Dictionary<Move, float> priorityDict = new Dictionary<Move, float>();
        for (int i = 0; i < moves.Count; i++)
        {
            priorityDict[moves[i]] = 1.0f;
            if (moves[i].type == Move.STATUS && opponent.GetStatus() != null)
            {
                priorityDict[moves[i]] = 0;
            }
            else if (moves[i].type == Move.STATUS)
            {
                priorityDict[moves[i]] += 0.2f;
            }
            else if (moves[i].type == Move.REST)
            {
                priorityDict[moves[i]] = (attacker.GetMaxStamina() - attacker.GetStamina()) * 0.01f;
            }
            else
            {
                priorityDict[moves[i]] -= 0.1f;
            }
        }
        return priorityDict;
    });
    Algorithm algorithm;
    List<Move> moveList;
    ICombatant attacker;
    ICombatant opponent;
    public Dictionary<Move, float> getPriorities()
    {
        return algorithm.AssignPriorities(moveList, attacker, opponent);
    }
    public AttackAI(List<Move> moveList, ICombatant attacker, ICombatant opp, Algorithm alg){
        this.moveList = moveList;
        algorithm = alg;
        this.attacker = attacker;
        this.opponent = opp;
    }
}

struct Algorithm
{
    String name;
    public Func<List<Move>, ICombatant, ICombatant, Dictionary<Move, float>> AssignPriorities;
    public Algorithm(String name, Func<List<Move>, ICombatant, ICombatant, Dictionary<Move, float>> f)
    {
        this.name = name;
        this.AssignPriorities = f;
    }
}