using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePropOrder : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.layer == 6)
            {
                if(player.position.y - transform.GetChild(i).position.y > 0.05)
                {
                    transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder + 1;
                }
                else
                {
                    transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder - 1;
                }
            }
        }
    }
}
