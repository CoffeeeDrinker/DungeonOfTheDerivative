using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class MoveContainer : MonoBehaviour
    {
        [SerializeField] string moveName;
        Move move;
        // Start is called before the first frame update
        void Start()
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
            return move;
        }
    }