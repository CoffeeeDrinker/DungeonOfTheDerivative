using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct AttackAI
{
    public static Algorithm RANDOM = new Algorithm("Random", (moves, attacker, opponent) =>
    {
        Dictionary<Move, float> priorityDict = new Dictionary<Move, float>();
        for (int i = 0; i < moves.Count; i++)
        {
            priorityDict[moves[i]] = 1.0f;
        }
        return priorityDict;
    });
    public static Algorithm AGGRESSIVE = new Algorithm("Aggressive", (moves, attacker, opponent) =>
    {
        Dictionary<Move, float> priorityDict = new Dictionary<Move, float>();
        for (int i = 0; i < moves.Count; i++)
        {
            priorityDict[moves[i]] = 1.0f;
            if (moves[i].type == Move.DAMAGE)
            {
                priorityDict[moves[i]] += 0.2f;
            }
            else if (moves[i].type == Move.STATUS && opponent.GetStatus() != null)
            {
                priorityDict[moves[i]] = 0;
            }
            else if (moves[i].type == Move.REST)
            {
                priorityDict[moves[i]] = (attacker.GetMaxStamina() - attacker.GetStamina()) * 0.01f;
            }
            else
            {
                priorityDict[moves[i]] -= 0.1f;
            }
        }
        return priorityDict;
    });
    public static Algorithm MODERATE = new Algorithm("Moderate", (moves, attacker, opponent) =>
    {
        Dictionary<Move, float> priorityDict = new Dictionary<Move, float>();
        for (int i = 0; i < moves.Count; i++)
        {
            priorityDict[moves[i]] = 1.0f;
            if (moves[i].type == Move.DAMAGE)
            {
                priorityDict[moves[i]] += 0.1f;
            }
            else if (moves[i].type == Move.STATUS && opponent.GetStatus() != null)
            {
                priorityDict[moves[i]] = 0;
            }
            else if (moves[i].type == Move.REST)
            {
                priorityDict[moves[i]] = (attacker.GetMaxStamina() - attacker.GetStamina()) * 0.01f;
            }
        }
        return priorityDict;
    });
    public static Algorithm CAUTIOUS = new Algorithm("Cautious", (moves, attacker, opponent) =>
    {
        Dictionary<Move, float> priorityDict = new Dictionary<Move, float>();
        for (int i = 0; i < moves.Count; i++)
        {
            priorityDict[moves[i]] = 1.0f;
            if (moves[i].type == Move.BUFF)
            {
                priorityDict[moves[i]] += 0.4f;
            }
            else if (moves[i].type == Move.STATUS && opponent.GetStatus() != null)
            {
                priorityDict[moves[i]] = 0;
            }
            else if (moves[i].type == Move.REST)
            {
                priorityDict[moves[i]] = (float)(attacker.GetMaxStamina() - attacker.GetStamina())/attacker.GetMaxStamina() * 0.5f;
            }
            else
            {
                priorityDict[moves[i]] -= 0.4f;
            }
        }
        return priorityDict;
    });
    public static Algorithm TRICKY = new Algorithm("Tricky", (moves, attacker, opponent) =>
    {
        Dictionary<Move, float> priorityDict = new Dictionary<Move, float>();
        for (int i = 0; i < moves.Count; i++)
        {
            priorityDict[moves[i]] = 1.0f;
            if (moves[i].type == Move.STATUS && opponent.GetStatus() != null)
            {
                priorityDict[moves[i]] = 0;
            }
            else if (moves[i].type == Move.STATUS)
            {
                priorityDict[moves[i]] += 0.2f;
            }
            else if (moves[i].type == Move.REST)
            {
                priorityDict[moves[i]] = (attacker.GetMaxStamina() - attacker.GetStamina()) * 0.01f;
            }
            else
            {
                priorityDict[moves[i]] -= 0.1f;
            }
        }
        return priorityDict;
    });
    Algorithm algorithm;
    List<Move> moveList;
    ICombatant attacker;
    ICombatant opponent;
    public Dictionary<Move, float> getPriorities()
    {
        return algorithm.AssignPriorities(moveList, attacker, opponent);
    }
    public AttackAI(List<Move> moveList, ICombatant attacker, ICombatant opp, Algorithm alg)
    {
        this.moveList = moveList;
        algorithm = alg;
        this.attacker = attacker;
        this.opponent = opp;
    }
}

public struct Algorithm
{
    public String name;
    public Func<List<Move>, ICombatant, ICombatant, Dictionary<Move, float>> AssignPriorities;
    public Algorithm(String name, Func<List<Move>, ICombatant, ICombatant, Dictionary<Move, float>> f)
    {
        this.name = name;
        this.AssignPriorities = f;
    }
}