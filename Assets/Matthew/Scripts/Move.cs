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

public static class Moves
{
    public static readonly Move POLAR = new Move(
        "Polar",
        Move.STATUS,
        25,
        (origin, direction) =>
        {
            if(origin.GetStamina() > 25)
            {
                origin.DepleteStamina(25);
                direction.TakeDamage(origin.Attack(10));
                if(UnityEngine.Random.Range(0f, 10f) > 5f)
                {
                    direction.AddStatusEffect(StatusEffects.POISONED);
                }
            }
        });
    public static readonly Move TAYLOREXPANSION = new Move(
        "Taylor Expansion",
        Move.BUFF,
        15, //stamina cost
        (origin, direction) =>
        {
            origin.SetDefense(origin.GetDefense()*1.1f);
        });
    public static readonly Move EMPIRICALRECOVERY = new Move(
        "Empirical Recovery",
        Move.HEAL,
        15, //stamina cost
        (origin, direction) =>
        {
            //heals 10*level 68% of the time, 20*level 27% of the time, 40*level 4.7% of the time, and 80*level 0.3% of the time, just like the empirical rule!
            float random = UnityEngine.Random.Range(0f, 100f);
            if (random <= 68)
            {
                //origin.Heal(10*origin.getLevel());
            }
            else if (random <= 95)
            {
                //origin.Heal(20*origin.getLevel());
            }
            else if (random <= 99.7)
            {
                //origin.Heal(40*origin.getLevel());
            }
            else
            {
                //origin.Heal(80*origin.getLevel());
            }
        });
    public static readonly Move LINEARLYDEPEND = new Move(
        "Linearly Depend",
        Move.BUFF, //is not an attack
        15, //stamina cost
        (origin, direction) =>
        {
            //If origin has less attack or defense than direction, buff origin
            //If direction has less attack or defense than origin, nerf origin
            //Do nothing if equal in stats
            
            if(direction.GetAttackModifier() > origin.GetAttackModifier()){
                origin.SetAttackModifier(origin.GetAttackModifier()*1.1f);
            } else if(origin.GetAttackModifier() > direction.GetAttackModifier()){
                origin.SetAttackModifier(origin.GetAttackModifier()*0.9f);
            }
            if(direction.GetDefense() > origin.GetDefense()){
                origin.SetDefense(origin.GetDefense()*1.1f);
            } else if(origin.GetDefense() > direction.GetDefense()){
                origin.SetDefense(origin.GetDefense()*0.9f);
            }
            
        });
    public static readonly Move AUGMENT = new Move(
        "Augment",
        Move.BUFF, //is not an attack
        15, //stamina cost
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
        Move.DEBUFF, //is not an attack
        15, //stamina cost
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
        Move.DEBUFF, //is an attack (note: this one doesn't deal damage but you still need to solve a math problem to hit)
        15, //stamina cost
        (origin, direction) =>
        {
            //Stuns the opponent, making them lose their turn
            //direction.PassTurn();
        });
    public static readonly Move SUBTRACTIONSLASH = new Move(
        "Subtraction Slash",
        Move.DAMAGE, //is an attack
        15, //stamina cost
        (origin, direction) =>
        {
            float dmg = UnityEngine.Random.Range(10, 20);
            direction.TakeDamage(origin.Attack((int)dmg));
        });
    public static readonly Move CONSTANTCRUSH = new Move(
        "Constant Crush",
        Move.DAMAGE, //is an attack
        15, //stamina cost
        (origin, direction) =>
        {
            float dmg = 15;
            direction.TakeDamage(origin.Attack((int)dmg));
        });
    public static readonly Move EXPONENTEXPLOSION = new Move(
        "Exponent Explosion",
        Move.DAMAGE, //is an attack
        15, //stamina cost
        (origin, direction) =>
        {
            int dmg = origin.Attack(UnityEngine.Random.Range(20, 35));
            direction.TakeDamage(dmg);
            origin.TakeDamage(dmg / 5);
        });
    public static readonly Move COLUMNSPACECASCADE = new Move(
        "Columnspace Cascade",
        Move.DAMAGE, //is an attack
        15, //stamina cost
        (origin, direction) =>
        {
            direction.TakeDamage(origin.Attack(UnityEngine.Random.Range(0, 5)));
            direction.TakeDamage(origin.Attack(UnityEngine.Random.Range(3, 7)));
            direction.TakeDamage(origin.Attack(UnityEngine.Random.Range(5, 10)));
        });
}