using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    bool waiting; //true if it's players turn waiting for input, false otherwise
    // Start is called before the first frame update
    void Start()
    {
        waiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonPressed()
    {
        if (waiting)
        {
            waiting = false;
        }
    }

    public void WaitForClick()
    {
        waiting = true;
        while (waiting)
        {
            IEnumerator wait()
            {
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
