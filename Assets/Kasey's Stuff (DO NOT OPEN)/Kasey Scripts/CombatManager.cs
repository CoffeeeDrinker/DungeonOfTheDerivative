using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public GameObject combatSystem;
    public GameObject player;
    public Vector3 combatSystemTransform;

    public void StartCombat()
    {
        combatSystem.SetActive(true);
        combatSystem.transform.localPosition = player.transform.localPosition + combatSystemTransform;
    }
}
