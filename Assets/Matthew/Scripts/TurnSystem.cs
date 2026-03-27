using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ShaderGraph.Serialization;
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
    [SerializeField] ItemManager itemManager;
    [SerializeField] Inventory inventory;
    private UIController UI; //attack button stored as AttackHandler component
    public static int turnIndex; //Whoever is having their turn currently; 0 = player, 1 = enemy
    private bool isClicked;
    public static bool inInventory = false;
    public static string itemUseText;
    private static bool hasStarted = false;
    //Kasey was here
    public GameObject gameManager;
    
    void Start()
    {
        winner = null;
        isClicked = false;
        player = playerField.GetComponent<PlayerController>();
        enemy = enemyField.GetComponent<EnemyController>();
        UI = UIField.GetComponent<UIController>();
        OnEnable();
    }

    private void OnEnable()
    {
        if (hasStarted)
        {
            turnIndex = 0;
            itemManager.EnterCombat();
            StartCoroutine(ManageTurns());
        }
        else { hasStarted = true; }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
                StatusEffect currentStatus = player.GetStatus();
                if (!player.TurnStart()) //only takes input if turn was not skipped
                {
                    if (player.GetStatus() != currentStatus){ //if player just recovered from a status effect
                        UI.DisplayText("You " + StatusEffects.recoveryMap[currentStatus], 2f);
                        float startTime = Time.time;
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
                    Move input = UI.GotInput();
                    if (input == null) //if no input has been recieved
                    {
                        yield return null; //if no input recieved, wait a frame
                    } else if(input.type == Move.RUN)
                    {
                        isClicked = false;
                        if (UnityEngine.Random.Range(0f, 1f) > 0.6) //60% chance of not escaping
                        {
                            UI.DisplayText("You got away!", 2f);
                            float startTime = Time.time;
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
                            winner = enemy;
                            gameManager.GetComponent<CombatManager>().CloseCombatSystem();
                            itemManager.ExitCombat();
                            break;
                        }
                        else
                        {
                            UI.DisplayText("You weren't able to escape!", 2f);
                            float startTime = Time.time;
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
                            UI.Unclick();
                            turnIndex++;
                        }
                    }
                    else if (input.type == Move.INVENTORY || input.type == Move.REST) //if it is something other than an attack
                    {
                        if (input.name == "Inventory")
                        {
                            inInventory = true;
                            itemUseText = null;
                            while (inInventory)
                            {
                                yield return null; //If nothing in inventory has been clicked, wait a frame
                            }
                            inventory.ToggleInventory();
                            if (itemUseText != null)
                            {
                                UI.DisplayText(itemUseText, 2f);
                                double startTime = Time.time;
                                isClicked = false;
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
                            turnIndex++;
                            UI.Unclick();
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
                        if (input.staminaCost <= player.GetStamina())
                        {
                            UI.HideButtons();
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
                            UI.ShowButtons();
                            turnIndex = 1;
                            UI.Unclick();
                        }
                        else
                        {
                            UI.DisplayText("You have insufficient stamina!", 2f);
                            float startTime = Time.time;
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
                            UI.HideAttackOptions();
                        }
                    }
                }
                else
                {
                    if (player.GetStatus() == StatusEffects.ASLEEP)
                    {
                        UI.DisplayText("You are asleep!", 2f);
                    }
                    else if (player.GetStatus() == StatusEffects.PARALYZED)
                    {
                        UI.DisplayText("You are frozen in paralysis!", 2f);
                    }
                    float startTime = Time.time;
                    isClicked = false;
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
                    turnIndex = 1; //skips turn if turn was skipped
                }
            }
            else // if it's enemy's turn
            {
                isClicked = false;
                
                if (enemy.IsAlive())
                {
                    StatusEffect oldStatus = enemy.GetStatus();
                    if (!enemy.TurnStart()) //only makes move if enemy doesn't have turn skipped
                    {
                        float startTime;
                        if(enemy.GetStatus() != oldStatus)
                        {
                            UI.DisplayText("Enemy" + StatusEffects.recoveryMap[oldStatus], 2f);
                            startTime = Time.time;
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
                        enemy.MakeNewMove(player);
                        string displayText = "Enemy is using: " + enemy.GetLastMove().name;
                        UI.DisplayText(displayText, 2f);
                        startTime = Time.time; //gets starting time for waiting
                        while (!isClicked) //checks if player is trying to skip by pressing left mouse button
                        {
                            if (Time.time - startTime >= 2f)
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
                    UI.DisplayText("Enemy collapsed from exhaustion!", 2f);
                    float startTime = Time.time;
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
                    UI.DisplayText("You gained " + enemy.getXP() + " XP!", 2f);
                    startTime = Time.time;
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
                    int lvl = player.GetLevel();
                    player.Win(enemy.getXP());
                    if(lvl != player.GetLevel())
                    {
                        UI.DisplayText("You leveled up from level " + lvl + " to level " + player.GetLevel() + "!", 2f);
                        startTime = Time.time;
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
                    winner = player;
                    enemy.Reset();
                    player.Reset();
  
                    //Continue dialogue
                    gameManager.GetComponent<CombatManager>().CloseCombatSystem();
                    itemManager.ExitCombat();

                    break;

                    //add xp, leave encounter, break
                }
            }
        } while (player.IsAlive()); //Run until one side is completely dead
        if (!player.IsAlive())
        {
            UI.DisplayText("You passed out from exhaustion!", 2f);
            float startTime = Time.time;
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
            enemy.Win(0);
            winner = enemy;

            gameManager.GetComponent<CombatManager>().CloseCombatSystem();
            itemManager.ExitCombat();
        }
    }

    //Dear Kasey:
    //KILL THIS METHOD GRRRR
    public int IfAttackHit()
    {
        return gameManager.GetComponent<MathProblemManager>().CheckAnswer();
        //return 0; //debug to make always hit
    }


}
