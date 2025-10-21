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
    private List<Move> moves;
    private Move lastMove;
    public Dictionary<Move, float> priorities;
    // Start is called before the first frame update
    void Start()
    {
        moves = new List<Move> {
            new Move(1, 50, 25),
            new Move(1, 10, 5),
            new Move(30, 40, 20),
            new Move(20, 20, 20),
            new Move("Rest", 20) //moves[4] is always rest for enemies
        };
        maxStamina = (int)(100 + 100 * ((level - 1.0) * 0.1));
        maxHealth = (int)(100 + 100 * ((level - 1.0) * 0.1));
        stamina = maxStamina;
        health = maxHealth;
        priorities = AssignPriority(moves);
        for(int i = 0; i < priorities.Count; i++)
        {
            Debug.Log("Move " + i + " prioritiy: " + priorities[moves[i]]);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
    }

    int ICombatant.Turn(Move move)
    {
        //Update rest priority
        priorities[moves[4]] = 0.01f * (maxStamina - stamina);
        for (int i = 0; i < priorities.Count; i++)
        {
            Debug.Log("Move " + i + " prioritiy: " + priorities[moves[i]]);
        }
        //select which move to use
        float selectionRange = 0;
        for(int i = 0; i < moves.Count; i++)
        {
            if (moves[i].staminaCost <= stamina) //only consider moves that can be used with current stamina
                selectionRange += priorities[moves[i]];
        }
        float random = UnityEngine.Random.Range(0, selectionRange);
        for (int i = 0; i < moves.Count; i++)
          {
           if (stamina >= moves[i].staminaCost)
                {
                    if (random <= priorities[moves[i]])
                    {
                        Debug.Log("Enemy is using: move " + i);
                        move = moves[i];
                        lastMove = move;
                        break;
                    }
                    else
                    {
                        random -= priorities[moves[i]];
                    }
                }
            }
        if (move.name == "Rest") //replenish stamina if enemy decides to rest
        {
            stamina -= move.staminaCost; //staminaCost is negative because it's resting, subtract to raise stamina
        }
        else
        {
            stamina -= move.staminaCost;
        }
        double modifier = UnityEngine.Random.Range(0.1F, 2.0F);
        int damage = (int)(move.GetNewDamage() * modifier);
        //Calculate total damage as totalDmg*modifier
        return damage;
    }

    //Takes moves available to and assigns each one a priority level
    //Postcondition: returns a dictionary mapping each move to its calculated priority level
    private Dictionary<Move, float> AssignPriority(List<Move> moves)
    {
        Dictionary<Move, float> priorityMap = new Dictionary<Move, float>();
        for(int i = 0; i < 4; i++)
        {
            float priority = moves[i].baseDmg / moves[i].staminaCost;
            priorityMap.Add(moves[i], priority);
        }
        priorityMap.Add(moves[4], 0.01f * (maxStamina - stamina)); //assigns priority for rest option
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
        int recharge = -baseRecharge;
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

    //Debug method to test selection algorithm and determine distribution of move picks
    private void testMoveProbability()
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
    }
}