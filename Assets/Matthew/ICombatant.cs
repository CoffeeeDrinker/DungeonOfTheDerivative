using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatant
{
    int Turn();
    bool IsAlive();
    public bool TakeDamage(int damage);
}
