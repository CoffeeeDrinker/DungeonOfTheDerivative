using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-10)]
public class MoveContainer : MonoBehaviour
    {
        [SerializeField] string moveName;
        Move move;
        void Awake()
        {
            List<Move> moves = Moves.AllMoves;
            for (int i = 0; i < moves.Count; i++)
            {
                if (moves[i].name == moveName)
                {
                    move = moves[i];
                    break;
                }
            }
        }

    private void Update()
    {
        
    }
    public Move GetMove()
        {
        if( move == null)
        {
            List<Move> moves = Moves.AllMoves;
            for (int i = 0; i < moves.Count; i++)
            {
                if (moves[i].name == moveName)
                {
                    move = moves[i];
                    break;
                }
            }
        }
        return move;
        }
    }