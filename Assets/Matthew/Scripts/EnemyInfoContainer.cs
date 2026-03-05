using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfoContainer : MonoBehaviour
{
    EnemyPreset preset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPreset(EnemyPreset enemyPreset)
    {
        this.preset = enemyPreset;
    }

    public EnemyPreset GetPreset()
    {
        return this.preset;
    }
}
