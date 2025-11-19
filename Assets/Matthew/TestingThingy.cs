using System;
using System.Diagnostics;
using UnityEngine;

public sealed class TestingThingy
{
    public readonly string name;
    public readonly Action<ICombatant> statusEffect;
    public TestingThingy(string name, Action<ICombatant> statusEffect) { this.name = name; this.statusEffect = statusEffect; }
}

public static class StatusEffectsDIE
{
        public readonly static TestingThingy POISONED = new TestingThingy(
            "Poisoned",
            (affected) =>
            {
                //Poisoned deals 1/5 of current health as damage each turn
                //20% chance of escaping every turn
                affected.TakeDamage((int)(affected.GetHealth()/5f));
                if(UnityEngine.Random.Range(0, 10) <= 2)
                {
                    //affected.ClearStatusEffects()
                }
            }
         );
        public readonly static TestingThingy BURNING = new TestingThingy(
            "Burning",
            (affected) =>
            {
                //Poisoned deals fixed 10 damage each turn and makes your attacks deal 0.75x damage
                //20% chance of escaping every turn
                affected.TakeDamage(10);
                //affected.SetDmgCoefficient(0.75);
                if (UnityEngine.Random.Range(0, 10) <= 2)
                {
                    //affected.SetDmgCoefficient(1);
                    //affected.ClearStatusEffects()
                }
            }
         );
        public readonly static TestingThingy ASLEEP = new TestingThingy(
            "Asleep",
            (affected) =>
            {
                //Asleep makes you skip every turn until you wake up
                //40% chance of waking up each turn
                if (UnityEngine.Random.Range(0, 10) <= 4)
                {
                    //affected.ClearStatusEffects()
                }
                else
                {
                    //affected.PassTurn()
                }
            }
         );
        public readonly static TestingThingy CAFFEINECRASH = new TestingThingy(
            "Caffeine Crash",
            (affected) =>
            {
                //Caffeine Crash depletes stamina by a fixed 10 every turn
                //10% chance of stopping each turn
                affected.DepleteStamina(10);
                if (UnityEngine.Random.Range(0, 10) <= 1)
                {
                    //affected.ClearStatusEffects()
                }
            }
         );
        public readonly static TestingThingy CONFUSED = new TestingThingy(
            "Confused",
            (affected) =>
            {
                //Confused makes affected have a 50% chance of attacking themselves with fixed damage 20 and losing turn
                //30% chance of waking up each turn
                if(UnityEngine.Random.Range(0, 10) <= 5)
                {
                    affected.TakeDamage(affected.Attack(20));
                    //affected.PassTurn()
                }
                if (UnityEngine.Random.Range(0, 10) <= 3)
                {
                    //affected.ClearStatusEffects()
                }
            }
         );

        public readonly static TestingThingy CAFFEINATED = new TestingThingy(
            "Caffeinated",
            (affected) =>
            {
                //Caffinated makes you gain fixed 20 stamina every turn
                //20% chance of wearing off each turn
                //Once it wears off, affected gets CaffieneCrash condition
                affected.Rest(20);
                if (UnityEngine.Random.Range(0, 10) <= 3)
                {
                    //affected.ClearStatusEffects()
                }
                //affected.SetStatusEffect(caffeineCrash);
            }
         );

        public readonly static TestingThingy PARALYZED = new TestingThingy(
            "Paralyzed",
            (affected) =>
            {
                //Paralyzed gives you a 50% chance of losing your turn each turn
                //20% chance of wearing off each turn
                if (UnityEngine.Random.Range(0, 10) <= 3)
                {
                    //affected.ClearStatusEffects()
                }
                if (UnityEngine.Random.Range(0, 10) <= 5)
                {
                    //affected.PassTurn();
                }
            }
         );

        public readonly static TestingThingy FROSTBITE = new TestingThingy(
            "Frostbite",
            (affected) =>
            {
                //Frostbite halves the strength of your attacks
                //25% chance of wearing off each turn
                //affected.SetDmgCoefficient(0.5);
                if (UnityEngine.Random.Range(0, 10) <= 3)
                {
                    //affected.ClearStatusEffects();
                    //affected.SetDmgCoefficient(1)
                }

            }
         );
}

public static class Moves
{
    public static readonly Move REST = new Move(
        "Rest",
        false, //is not an attack
        (origin, direction) =>
        {
            origin.Rest(20);
        });
    public static readonly Move OPENINVENTORY = new Move(
        "Open Inventory",
        false,
        (origin, direction) =>
        {
            //Open inventory
        });
    public static readonly Move TAYLOREXPANSION = new Move(
        "Taylor Expansion",
        false,
        (origin, direction) =>
        {
             //origin.SetDefense(origin.GetDefense()*1.1);
        });
    public static readonly Move EMPIRICALRECOVERY = new Move(
        "Empirical Recovery",
        false,
        (origin, direction) =>
        {
            //heals 10*level 68% of the time, 20*level 27% of the time, 40*level 4.7% of the time, and 80*level 0.3% of the time, just like the empirical rule!
            float random = UnityEngine.Random.Range(0, 100);
            if(random <= 68)
            {
                //origin.Heal(10*origin.getLevel());
            } else if (random <= 95)
            {
                //origin.Heal(20*origin.getLevel());
            }
            else if (random <= 99.7)
            {
                //origin.Heal(40*origin.getLevel());
            } else
            {
                //origin.Heal(80*origin.getLevel());
            }
        });
    public static readonly Move LINEARLYDEPEND = new Move(
        "Linearly Depend",
        false, //is not an attack
        (origin, direction) =>
        {
            //If origin has less attack or defense than direction, buff origin
            //If direction has less attack or defense than origin, buff direction
            //Do nothing if equal in stats
            /*
            if(direction.GetAttack() > origin.GetAttack()){
                origin.SetAttack(origin.GetAttack()*1.1);
            } else if(origin.GetAttack() > direction.GetAttack()){
                direction.SetAttack(direction.GetAttack()*1.1);
            }
            if(direction.GetDefense() > origin.GetDefense()){
                origin.SetDefense(origin.GetDefense()*1.1);
            } else if(origin.GetDefense() > direction.GetDefense()){
                direction.SetDefense(direction.GetDefense()*1.1);
            }
            */
        });
    public static readonly Move AUGMENT = new Move(
        "Augment",
        false, //is not an attack
        (origin, direction) =>
        {
            //Increases origin's rank (get it? you add another vector and it increases your rank? I'm so clever)
            //Makes origin operate 1 level higher than normal, doesn't stack
            /*
            origin.SetTempLevel(origin.GetLevel()+1); //note: ICombatant.GetLevel() returns actual level, not temp level, so calling this twice does nothing
            */
        });
    public static readonly Move DOPPONENTDLEVEL = new Move(
        "dOpponent/dLevel",
        false, //is not an attack
        (origin, direction) =>
        {
            //Decreases direction's order (get it? you differentiate and it reduces order? I'm so clever)
            //Makes direction operate 1 level lower than normal, doesn't stack
            /*
            direction.SetTempLevel(direction.GetLevel()-1); //note: ICombatant.GetLevel() returns actual level, not temp level, so calling this twice does nothing
            */
        });
    public static readonly Move SERIESSTUN = new Move(
        "Series Stun",
        true, //is an attack (note: this one doesn't deal damage but you still need to solve a math problem to hit)
        (origin, direction) =>
        {
            //Stuns the opponent, making them lose their turn
            //direction.PassTurn();
        });
    public static readonly Move SUBTRACTIONSLASH = new Move(
        "Subtraction Slash",
        true, //is an attack
        (origin, direction) =>
        {
            float dmg = UnityEngine.Random.Range(10, 20);
            direction.TakeDamage(origin.Attack((int)dmg));
        });
    public static readonly Move CONSTANTCRUSH = new Move(
        "Constant Crush",
        true, //is an attack
        (origin, direction) =>
        {
            float dmg = 15;
            direction.TakeDamage(origin.Attack((int)dmg));
        });
    public static readonly Move EXPONENTEXPLOSION = new Move(
        "Exponent Explosion",
        true, //is an attack
        (origin, direction) =>
        {
            int dmg = origin.Attack(UnityEngine.Random.Range(20, 35));
            direction.TakeDamage(dmg);
            origin.TakeDamage(dmg/5);
        });
    public static readonly Move COLUMNSPACECASCADE = new Move(
        "Columnspace Cascade",
        true, //is an attack
        (origin, direction) =>
        {
            direction.TakeDamage(origin.Attack(UnityEngine.Random.Range(0, 5)));
            direction.TakeDamage(origin.Attack(UnityEngine.Random.Range(3, 7)));
            direction.TakeDamage(origin.Attack(UnityEngine.Random.Range(5, 10)));
        });
}