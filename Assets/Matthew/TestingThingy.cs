using System;
using System.Diagnostics;
using UnityEngine;

public sealed class TestingThingy
{
    public readonly string name;
    public readonly Action<ICombatant, ICombatant> move;
    public TestingThingy(string name, Action<ICombatant, ICombatant> move) { this.name = name; this.move = move; }
}

class ClientCode
{
    ClientCode()
    {
        TestingThingy attackMove = new TestingThingy(
            "KILL",
            (origin, direction) =>
            {
                float lowDmgBound = 10;
                float highDmgBound = 20;
                float dmg = UnityEngine.Random.Range(lowDmgBound, highDmgBound); //returns number in range of damages
                if (dmg > 0)
                {
                    direction.TakeDamage((int)dmg);
                    origin.DepleteStamina(10);
                }
            }
            
         );

        TestingThingy rest = new TestingThingy(
            "Rest",
            (origin, direction) =>
            {
                origin.Rest(20);
            }
        );

        TestingThingy die = new TestingThingy(
            "Die",
            (origin, direction) =>
            {
                origin.TakeDamage(1000000);
            }
        );
    }
}