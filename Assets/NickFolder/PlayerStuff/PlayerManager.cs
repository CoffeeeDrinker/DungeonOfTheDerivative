using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static int ducks = 400;
    public static string playerName = "ERROR: No name set";

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SetDucks(int amount)
    {
        ducks = amount;
    }

    public static void SetName(string word)
    {
        playerName = word;
    }
}
