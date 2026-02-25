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