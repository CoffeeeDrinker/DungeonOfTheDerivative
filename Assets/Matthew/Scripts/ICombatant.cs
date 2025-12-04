using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Interface for player and enemy controllers
public interface ICombatant
{
    int Attack(int baseDmg);
    bool IsAlive();
    public bool TakeDamage(int damage);

    public int GetHealth();

    public bool DepleteStamina(int exhaustion);

    public void Rest(int baseRecharge);

    public void Win(int XP);

    public int getXP();

    public int GetStamina();

    Move GetLastMove();

    void MakeNewMove(ICombatant x);

    //void Heal(int x);
}
