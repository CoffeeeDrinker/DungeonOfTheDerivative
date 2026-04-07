using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] string typeField;
    CombatantType type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public CombatantType GetType()
    {
        if(typeField == "Player")
        {
            type = CombatantType.Player;
        } else if (typeField == "Grunt")
        {
            type = CombatantType.Grunt;
        } else if (typeField == "Boss")
        {
            type = CombatantType.Boss;
        }
        return type;
    }
}
