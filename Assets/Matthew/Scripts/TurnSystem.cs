using System.Collections;
using System.Collections.Generic;
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
                Move input = UI.GotInput();
                if (input.name.Equals("Pass Frame"))
                {
                    yield return null; //if no input recieved, wait a frame
                }
                else if(input.name.Equals("Inventory"))
                {
                    turnIndex++; //if button pressed doesn't attack, pass turn
                    UI.Unclick();
                } else if(input.name.Equals("Rest"))
                {
                    player.Rest(input.staminaCost);
                    turnIndex++;
                    UI.Unclick();
                } else if(input.name.Equals("Run"))
                {
                    turnIndex++; //Running fails automatically :(
                    UI.Unclick();
                }
                else { //if button pressed is an attack
                    gameManager.GetComponent<MathProblemManager>().StartDraw();
                    //Attacks opponent, then makes it enemy's turn, then resets button
                    if (player.DepleteStamina(input.staminaCost)) //subtracts 10 from player stamina, runs if still player stamina > 0
                    {
                        while(FIXTHISKASEY() == 2)
                        {
                            yield return null;
                        }
                        if (FIXTHISKASEY() == 0) //checks if it hit
                        {
                            enemy.TakeDamage(player.Turn(input));
                            gameManager.GetComponent<MathProblemManager>().UnAnswer();
                        }
                        else if(FIXTHISKASEY() == 1)
                        {
                            Debug.Log("incorrect, no damage :(");
                            gameManager.GetComponent<MathProblemManager>().UnAnswer();
                        }
                    }   
                    turnIndex = 1;
                    UI.Unclick();
                }
            }
            else // if it's enemy's turn
            {
                if (enemy.IsAlive())
                {
                    //Deals damage to player with base dmg 10
                    player.TakeDamage(enemy.Turn(null));
                    UI.DisplayText("Enemy is using: " + enemy.GetLastMove().name, 2f);
                    float startTime = Time.time; //gets starting time for waiting
                    while (!isClicked) //checks if player is trying to skip by pressing left mouse button
                    {
                        if(Time.time - startTime >= 2)
                        {
                            break;
                        }
                        yield return null;
                    }
                    isClicked = false;
                    UI.HideText();
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
    public int FIXTHISKASEY()
    {
        return gameManager.GetComponent<MathProblemManager>().CheckAnswer();
    }
}
