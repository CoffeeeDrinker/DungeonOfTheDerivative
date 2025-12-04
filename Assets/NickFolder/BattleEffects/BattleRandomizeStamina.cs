using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRandomizeStamina : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        //Randomize Stamina
        TurnSystem.inInventory = false;
    }
}
