using System;
using UnityEngine;

public sealed class Move
{
    public readonly string name;
    public readonly MoveType type;
    public readonly int staminaCost;
    public readonly Action<ICombatant, ICombatant> move;
    public readonly static MoveType STATUS = new MoveType();
    public readonly static MoveType REST = new MoveType();
    public readonly static MoveType DAMAGE = new MoveType();
    public readonly static MoveType INVENTORY = new MoveType();
    public readonly static MoveType RUN = new MoveType();
    public readonly static MoveType BUFF = new MoveType();
    public readonly static MoveType DEBUFF = new MoveType();
    public readonly static MoveType HEAL = new MoveType();
    public Move(string name, MoveType type, int staminaCost, Action<ICombatant, ICombatant> move) { this.name = name; this.staminaCost = staminaCost; this.type = type; this.move = move; }
}

public class MoveType { }