using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyHolder : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public static PlayerEnemyHolder instance;
    // Start is called before the first frame update
    void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
