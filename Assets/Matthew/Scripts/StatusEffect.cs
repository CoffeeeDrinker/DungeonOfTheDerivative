using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public sealed class StatusEffect
{
    public readonly string name;
    public readonly Sprite sprite;
    public readonly Func<ICombatant, bool> statusEffect; //takes ICombatant affected and does status efffect things to them, returns true if skips turn, false otherwise
    public StatusEffect(string name, Sprite sprite, Func<ICombatant, bool> statusEffect) { this.name = name; this.sprite = sprite; this.statusEffect = statusEffect; }
}

public static class StatusEffects
{
    public readonly static List<Sprite> statusSpriteList = new List<Sprite>()
    {
        //poisoned
        null,
        //buring
        null,
        //asleep
        null,
        //caffeine crash
        null,
        //confused
        null,
        //caffeinated
        null,
        //paralyzed,
        null,
        //frostbite,
        null
    };
    public readonly static StatusEffect POISONED = new StatusEffect(
        "Poisoned",
        statusSpriteList[0],
        (affected) =>
        {
            //Poisoned deals 1/5 of current health as damage each turn
            //20% chance of escaping every turn
            affected.TakeDamage((int)(affected.GetHealth() / 5f));
            if (UnityEngine.Random.Range(0, 10) <= 2)
            {
                affected.ClearStatusEffects();
            }
            return false; //never skips turn
        }
     );
    public readonly static StatusEffect BURNING = new StatusEffect(
        "Burning",
        statusSpriteList[1],
        (affected) =>
        {
            //Poisoned deals fixed 10 damage each turn and makes your attacks deal 0.75x damage
            //20% chance of escaping every turn
            affected.TakeDamage(10);
            //affected.SetDmgCoefficient(0.75);
            if (UnityEngine.Random.Range(0, 10) <= 2)
            {
                //affected.SetDmgCoefficient(1);
                affected.ClearStatusEffects();
            }
            return false; //never skips turn
        }
     );
    public readonly static StatusEffect ASLEEP = new StatusEffect(
        "Asleep",
        statusSpriteList[2],
        (affected) =>
        {
            //Asleep makes you skip every turn until you wake up
            //Chance of waking up each turn
            if (UnityEngine.Random.Range(0, 10) <= 2)
            {
                affected.ClearStatusEffects();
                return false;
            }
            else
            {
                return true; //always skips turn
            }
        }
     );
    public readonly static StatusEffect CAFFEINECRASH = new StatusEffect(
        "Caffeine Crash",
        statusSpriteList[3],
        (affected) =>
        {
            //Caffeine Crash depletes stamina by a fixed 10 every turn
            //10% chance of stopping each turn
            affected.DepleteStamina(10);
            if (UnityEngine.Random.Range(0, 10) <= 1)
            {
                affected.ClearStatusEffects();
            }
            return false; //never skips turn
        }
     );
    public readonly static StatusEffect CONFUSED = new StatusEffect(
        "Confused",
        statusSpriteList[4],
        (affected) =>
        {
            //Confused makes affected have a 50% chance of attacking themselves with fixed damage 20 and losing turn
            //30% chance of waking up each turn
            if (UnityEngine.Random.Range(0, 10) <= 5)
            {
                affected.TakeDamage(affected.Attack(20));
                return true; //skips turn if makes affected attack itself
            }
            if (UnityEngine.Random.Range(0, 10) <= 3)
            {
                affected.ClearStatusEffects();
                return false;
            }
            return false;
        }
     );

    public readonly static StatusEffect CAFFEINATED = new StatusEffect(
        "Caffeinated",
        statusSpriteList[5],
        (affected) =>
        {
            //Caffinated makes you gain fixed 20 stamina every turn
            //20% chance of wearing off each turn
            //Once it wears off, affected gets CaffieneCrash condition
            affected.Rest(20);
            if (UnityEngine.Random.Range(0, 10) <= 3)
            {
                affected.ClearStatusEffects();
            }
            //affected.SetStatusEffect(caffeineCrash);
            return false; //never skips turn
        }
     );

    public readonly static StatusEffect PARALYZED = new StatusEffect(
        "Paralyzed",
        statusSpriteList[6],
        (affected) =>
        {
            //Paralyzed gives you a 50% chance of losing your turn each turn
            //20% chance of wearing off each turn
            if (UnityEngine.Random.Range(0, 10) <= 3)
            {
                affected.ClearStatusEffects();
                return false;
            }
            if (UnityEngine.Random.Range(0, 10) <= 5)
            {
                return true; //skips turn
            }
            return false;
        }
     );

    public readonly static StatusEffect FROSTBITE = new StatusEffect(
        "Frostbite",
        statusSpriteList[7],
        (affected) =>
        {
            //Frostbite halves the strength of your attacks
            //25% chance of wearing off each turn
            //affected.SetDmgCoefficient(0.5);
            if (UnityEngine.Random.Range(0, 10) <= 3)
            {
                 affected.ClearStatusEffects();
                //affected.SetDmgCoefficient(1)
            }
            return false; //never skips turn
        }
     );

    public readonly static Dictionary<StatusEffect, string> recoveryMap = new Dictionary<StatusEffect, string>()
    {
        //keys should include a certain status effect defined in this class
        //values should include text to display when opponent recovers from status effect in key. Start strings with a space and write them as though the first word in the sentence is "enemy"
        {POISONED, " has recovered from poisoning!" },
        {BURNING, " has stopped burning!" },
        {ASLEEP, " woke up!" },
        {CAFFEINECRASH, " has ended its caffeine crash!" },
        {CONFUSED, " is no longer confused!" },
        {CAFFEINATED, " has crashed on caffeine!" },
        {PARALYZED, " has recovered from paralysis!"},
        {FROSTBITE, " has warmed up!" }
    };
}