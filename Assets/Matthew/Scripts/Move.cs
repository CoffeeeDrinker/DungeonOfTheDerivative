using System;
using UnityEngine;

public sealed class Move
{
    public readonly string name;
    public readonly bool isAttack;
    public readonly Action<ICombatant, ICombatant> move;
    public Move(string name, bool isAttack, Action<ICombatant, ICombatant> move) { this.name = name; this.isAttack = isAttack; this.move = move; }
}
