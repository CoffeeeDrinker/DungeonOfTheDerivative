using System;
using UnityEngine;

public sealed class Move
{
    public readonly string name;
    public readonly MoveType type;
    public readonly Action<ICombatant, ICombatant> move;
    public static MoveType STATUS = new MoveType();
    public static MoveType REST = new MoveType();
    public static MoveType DAMAGE = new MoveType();
    public static MoveType INVENTORY = new MoveType();
    public static MoveType RUN = new MoveType();
    public static MoveType BUFF = new MoveType();
    public static MoveType DEBUFF = new MoveType();
    public static MoveType HEAL = new MoveType();
    public Move(string name, MoveType type, Action<ICombatant, ICombatant> move) { this.name = name; this.type = type; this.move = move; }
}

public class MoveType { }