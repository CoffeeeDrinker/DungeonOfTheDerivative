using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
[DefaultExecutionOrder(-1)]
public class UIController : MonoBehaviour
{
    [SerializeField] GameObject attackButtonField;
    [SerializeField] GameObject inventoryButtonField;
    [SerializeField] GameObject restButtonField;
    [SerializeField] GameObject runButtonField;
    [SerializeField] GameObject attackOptionMasterField;
    [SerializeField] GameObject combatTextMasterField;
    protected static AttackOptionMaster attackController;
    protected static AttackHandler attackHandler;
    protected static InventoryHandler inventoryHandler;
    protected static RestHandler restHandler;
    protected static RunHandler runHandler;
    protected static CombatTextController combatTextController;
    // Start is called before the first frame update
    void Start()
    {
        attackHandler = attackButtonField.GetComponent<AttackHandler>();
        inventoryHandler = inventoryButtonField.GetComponent<InventoryHandler>();
        restHandler = restButtonField.GetComponent<RestHandler>();
        runHandler = runButtonField.GetComponent<RunHandler>();
        attackController = attackOptionMasterField.GetComponent<AttackOptionMaster>();
        combatTextController = combatTextMasterField.GetComponent<CombatTextController>();
    }

    // Update is called once per frame
    void Update()
    {
        
  }

    //Postcondition: returns the base damage of the attack if an attack was made, -1 if inventory was opened, -2 if no input was recieved, -3 if rest, -4 if run
    public Move GotInput()
    {
        if (attackHandler.IsClicked())
        {
            HideButtons();
            attackController.ToggleAttackOptions(true);
            Move result = attackController.GetMove();
            while (result == null)
            {
                return new Move("Pass Frame");
            }
            attackController.ToggleAttackOptions(false);
            ShowButtons();
            return result;
        }
        else if (inventoryHandler.IsClicked())
        {
            Debug.Log("Inventory");
            return new Move("Inventory");
        } else if (restHandler.IsClicked())
        {
            Debug.Log("Rest");
            return new Move("Rest", 20);
        } else if (runHandler.IsClicked())
        {
            Debug.Log("Run");
            return new Move("Run");
        }
        else
        {
            return new Move("Pass Frame");
        }
    }

    public void Unclick()
    {
        attackHandler.Unclick();
        inventoryHandler.Unclick();
        restHandler.Unclick();
        runHandler.Unclick();
    }

    public void HideButtons()
    {
        if (attackButtonField.activeSelf) attackButtonField.SetActive(false);
        if (inventoryButtonField.activeSelf) inventoryButtonField.SetActive(false);
        if (restButtonField.activeSelf) restButtonField.SetActive(false);
        if (runButtonField.activeSelf) runButtonField.SetActive(false);
    }

    public void ShowButtons()
    {
        if (!attackButtonField.activeSelf) attackButtonField.SetActive(true);
        if (!inventoryButtonField.activeSelf) inventoryButtonField.SetActive(true);
        if (!restButtonField.activeSelf) restButtonField.SetActive(true);
        if (!runButtonField.activeSelf) runButtonField.SetActive(true);
    }

    public void DisplayText(string text)
    {
        combatTextMasterField.SetActive(true);
        combatTextController.Toggle();
        combatTextController.DisplayText(text);
    }

    public void DisplayText(string text, float duration)
    {
        combatTextMasterField.SetActive(true);
        combatTextController.Toggle();
        combatTextController.DisplayText(text, duration);
    }

    public void HideText()
    {
        combatTextController.DisplayText("");
        combatTextController.StopAllCoroutines();
        combatTextController.Toggle();
    }
}
