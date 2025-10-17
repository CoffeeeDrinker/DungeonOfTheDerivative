using UnityEngine;

public sealed class Move
{
    public readonly string name;
    public readonly float baseDmg; //numeric base damage for comparison, mean of lowDmgBound and highDmgBound
    public readonly float lowDmgBound; //Maximum base damage that can be dealt
    public readonly float highDmgBound; //Minimum base damage that can be dealt
    public readonly int staminaCost;
    public Move(int baseDmg)
    {
        //Move with constant damage
        name = "Attack";
        this.baseDmg = baseDmg;
        lowDmgBound = baseDmg;
        highDmgBound = baseDmg;
        staminaCost = 10;
    }

    public Move(int lowerBound, int upperBound, int staminaCost)
    {
        name = "Attack";
        lowDmgBound = lowerBound;
        highDmgBound = upperBound;
        baseDmg = (lowerBound + upperBound) / 2;
        this.staminaCost = staminaCost;
    }

    public Move(string name, int staminaCost)
    {
        this.name = name;
        this.staminaCost = -1*System.Math.Abs(staminaCost); //negative stamina cost restores stamina
    }

    public Move(string name)
    {
        this.name = name;
        lowDmgBound = highDmgBound = baseDmg = staminaCost = 0;
    }

    public float GetNewDamage()
    {
        float dmg = Random.Range(lowDmgBound, highDmgBound); //returns number in range of damages
        if(dmg > 0)
        {
            return dmg;
        }
        return 0;
    }
}
