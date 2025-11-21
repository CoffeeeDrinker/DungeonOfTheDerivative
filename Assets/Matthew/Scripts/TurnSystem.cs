using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
[DefaultExecutionOrder(100)] //This class's Start() method will be called last
public class TurnSystem : MonoBehaviour
{
    public GameObject playerField;
    public GameObject enemyField;
    public ICombatant player;
    public ICombatant enemy;
    public ICombatant winner;
    public GameObject UIField; //serialized field containing attack button
    private UIController UI; //attack button stored as AttackHandler component
    public static int turnIndex; //Whoever is having their turn currently; 0 = player, 1 = enemy
    private bool isClicked;

    //Kasey was here
    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        winner = null;
        isClicked = false;
        player = playerField.GetComponent<PlayerController>();
        enemy = enemyField.GetComponent<EnemyController>();
        UI = UIField.GetComponent<UIController>();
        turnIndex = 0;
        StartCoroutine(ManageTurns());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && turnIndex != 0)
        {
            isClicked = !isClicked;
        }
    }

    private IEnumerator ManageTurns()
    {
        do
        {
            if (turnIndex == 0) //if it's player's turn
            {
                if (!player.TurnStart()) //only takes input if turn was not skipped
                {
                    Move input = UI.GotInput();
                    if (input == null) //if no input has been recieved
                    {
                        yield return null; //if no input recieved, wait a frame
                    }
                    else if (input.isAttack == false) //if it is something other than an attack
                    {
                        if (input.name == "Inventory" && !IfInventoryUnUnClicked())
                        {
                            yield return null; //If nothing in inventory has been clicked, wait a frame
                        }
                        else
                        {
                            input.move(player, enemy);
                            turnIndex++;
                            UI.Unclick();
                        }
                    }
                    else
                    { //if button pressed is an attack
                        gameManager.GetComponent<MathProblemManager>().StartDraw();
                        //Attacks opponent, then makes it enemy's turn, then resets button
                        while (IfAttackHit() == 2)
                        {
                            yield return null;
                        }
                        if (IfAttackHit() == 0) //checks if it hit
                        {
                            input.move(player, enemy);
                            gameManager.GetComponent<MathProblemManager>().UnAnswer();
                        }
                        else if (IfAttackHit() == 1)
                        {
                            Debug.Log("incorrect, no damage :(");
                            gameManager.GetComponent<MathProblemManager>().UnAnswer();
                        }
                        turnIndex = 1;
                        UI.Unclick();
                    }
                }
                else
                {
                    turnIndex = 1; //skips turn if turn was skipped
                }
            }
            else // if it's enemy's turn
            {
                if (enemy.IsAlive())
                {
                    //Deals damage to player with base dmg 10
                    if (!enemy.TurnStart()) //only makes move if enemy doesn't have turn skipped
                    {
                        enemy.MakeNewMove(player);
                        UI.DisplayText("Enemy is using: " + enemy.GetLastMove().name, 2f);
                        float startTime = Time.time; //gets starting time for waiting
                        while (!isClicked) //checks if player is trying to skip by pressing left mouse button
                        {
                            if (Time.time - startTime >= 2)
                            {
                                break;
                            }
                            yield return null;
                        }
                        isClicked = false;
                        UI.HideText();
                    } else
                    {
                        if(enemy.GetStatus() == StatusEffects.ASLEEP)
                        {
                            UI.DisplayText("Enemy is asleep!", 2f);
                        } else if (enemy.GetStatus() == StatusEffects.PARALYZED)
                        {
                            UI.DisplayText("Enemy is frozen in paralysis!", 2f);
                        }
                        float startTime = Time.time; //gets starting time for waiting
                        while (!isClicked) //checks if player is trying to skip by pressing left mouse button
                        {
                            if (Time.time - startTime >= 2)
                            {
                                break;
                            }
                            yield return null;
                        }
                        isClicked = false;
                        UI.HideText();
                    }
                        turnIndex = 0;
                    Debug.Log("Player health: " + player.GetHealth() + "\nPlayer stamina: " + player.GetStamina() + "\nEnemy health: " + enemy.GetHealth() + "\nEnemy stamina: " + enemy.GetStamina());
                }
                else
                {
                    //If enemy dies, end combat
                    Debug.Log("You won!");
                    player.Win(enemy.getXP());
                    winner = player;

                    //Continue dialogue
                    gameManager.GetComponent<CombatManager>().CloseCombatSystem();

                    break;

                    //add xp, leave encounter, break
                }
            }
        } while (player.IsAlive()); //Run until one side is completely dead
        if (!player.IsAlive())
        {
            enemy.Win(0);
            winner = enemy;
        }
    }

    //Dear Kasey:
    //KILL THIS METHOD GRRRR
    public int IfAttackHit()
    {
        return gameManager.GetComponent<MathProblemManager>().CheckAnswer();
        //return 0; //debug to make always hit
    }
     
    public bool IfInventoryUnUnClicked()
    {
        return true;
        //return true if smth was clicked
    }
}
