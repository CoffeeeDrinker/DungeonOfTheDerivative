using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class EnemyController : MonoBehaviour, ICombatant
{
    public GameObject m_enemy;
    public GameObject healthBar;
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
    [SerializeField] GameObject statusMarker;
    // Start is called before the first frame update
    void Start()
    {
        statusMarker.SetActive(false);
        moveList = new List<Move>() //initializes list of moves
        {
            new Move(
                "Rest", //name of move
                false, //is not an attack
                (origin, direction) => //implementation of move
                {
                    origin.Rest(30);
                }),
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
        maxStamina = (int)(100 + 100 * ((level - 1.0) * 0.1));
        maxHealth = 100000+(int)(100 + 100 * ((level - 1.0) * 0.1));
        stamina = maxStamina;
        health = maxHealth;
        priorities = AssignPriority(moveList);

        
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
        priorities[moveList[0]] = 1;// 0.01f * (maxStamina - stamina);
        /*
        for (int i = 0; i < priorities.Count; i++)
        {
            Debug.Log("Move " + i + " prioritiy: " + priorities[moveList[i]]);
        } */
        //select which move to use
        float selectionRange = 0;
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

    //Takes moves available to and assigns each one a priority level
    //Postcondition: returns a dictionary mapping each move to its calculated priority level
    private Dictionary<Move, float> AssignPriority(List<Move> moves)
    {
        Dictionary<Move, float> priorityMap = new Dictionary<Move, float>();
        //for(int i = 1; i < 5; i++)
        //{
        // float priority = 2f;//moves[i].baseDmg / moves[i].staminaCost;
        //priorityMap.Add(moves[i], priority);
        //}

        //NOTE: Priority values are harcoded for now. Need to make it so enemy move priorities are assigned at the same time as their moves
        priorityMap.Add(moves[0], 0.01f * (maxStamina - stamina)); //assigns priority for rest option
        priorityMap.Add(moves[1], 2);
        priorityMap.Add(moves[2], 0.5f);
        priorityMap.Add(moves[3], 0.75f);
        priorityMap.Add(moves[4], 1f);
        return priorityMap;
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
        healthBar.transform.Translate(damage * (5.71F/maxHealth), 0, 0);
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