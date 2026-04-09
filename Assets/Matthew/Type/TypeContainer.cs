using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeContainer : MonoBehaviour
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

    public CombatantType GetCombatantType()
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
