using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOptionMaster : MonoBehaviour
{
    Animator anim;

    [SerializeField] GameObject attackOption1Field;
    [SerializeField] GameObject attackOption2Field;
    [SerializeField] GameObject attackOption3Field;
    [SerializeField] GameObject attackOption4Field;
    private List<AttackOptionHandler> attackOptionList;
    // Start is called before the first frame update
    void Start()
    {
        attackOptionList = new List<AttackOptionHandler>(4)
        {
            attackOption1Field.GetComponent<AttackOptionHandler>(),
            attackOption2Field.GetComponent<AttackOptionHandler>(),
            attackOption3Field.GetComponent<AttackOptionHandler>(),
            attackOption4Field.GetComponent<AttackOptionHandler>()
        };
        ToggleAttackOptions(false);

        anim = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Move GetMove() //returns whichever move is clicked this frame, null if nothing clicked this frame
    {
        AttackOptionHandler selected = null;
        for (int i = 0; i < attackOptionList.Count; i++)
        {
            if (attackOptionList[i].IsClicked())
            {
                anim.SetBool("swing1", true);
                selected = attackOptionList[i];
                attackOptionList[i].Unclick();
                
                break;
            }
        }
        if (selected != null)
        {
            Debug.Log(selected.GetMove().name);
            return selected.GetMove();
        }
        else
        {
            return null;
        }
    }

    public void ToggleAttackOptions(bool active)
    {
        attackOption1Field.SetActive(active);
        attackOption2Field.SetActive(active);
        attackOption3Field.SetActive(active);
        attackOption4Field.SetActive(active);
    }
}
