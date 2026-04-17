using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(10)] //runs after default scripts

public class ActiveMoveMaster : MonoBehaviour
{
    [SerializeField] GameObject ActiveMoveMasterField;
    public static ActiveMoveMaster moveMaster;
    [SerializeField] GameObject Move1Field;
    [SerializeField] GameObject Move2Field;
    [SerializeField] GameObject Move3Field;
    [SerializeField] GameObject Move4Field;
    Move move1;
    Move move2;
    Move move3;
    Move move4;
    private ActiveMoveController clickedButton = null;
    private bool clicked = false;
    
    // Start is called before the first frame update
    void Start()
    {
        moveMaster = ActiveMoveMasterField.GetComponent<ActiveMoveMaster>();
        move1 = Move1Field.GetComponent<ActiveMoveController>().GetMove();
        move2 = Move2Field.GetComponent<ActiveMoveController>().GetMove();
        move3 = Move3Field.GetComponent<ActiveMoveController>().GetMove();
        move4 = Move4Field.GetComponent<ActiveMoveController>().GetMove();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && clicked == false)
        {
            clicked = true;
        }
    }

    public void Reselect(Move newMove, AlternateMoveController replacement)
    {
        StartCoroutine(ReselectActiveMove(newMove, replacement));
    }

    private IEnumerator ReselectActiveMove(Move newMove, AlternateMoveController replacement)
    {
        clicked = false;
        clickedButton = null;
        while (!clicked)
        {
            yield return null;
        }
        for(int i = 0; i < 50; i++)
            yield return null;
        Debug.Log("button: " + clickedButton);
        if (clickedButton != null)
        {
            replacement.SetMove(clickedButton.GetComponent<ActiveMoveController>().GetMove());
            clickedButton.GetComponent<ActiveMoveController>().SetMove(newMove);
            move1 = Move1Field.GetComponent<ActiveMoveController>().GetMove();
            move2 = Move2Field.GetComponent<ActiveMoveController>().GetMove();
            move3 = Move3Field.GetComponent<ActiveMoveController>().GetMove();
            move4 = Move4Field.GetComponent<ActiveMoveController>().GetMove();
        }
        Unclick();
        replacement.ResetText();
    }

    public void Click(ActiveMoveController button) {
        clicked = true;
        clickedButton = button;
    }

    public void Unclick()
    {
        clicked = false;
        clickedButton = null;
    }
}
