using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFred : MonoBehaviour
{
    SoundManager deathScream;

    // Start is called before the first frame update
    void Start()
    {
        deathScream = FindFirstObjectByType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void scream()
    {
        deathScream.PlaySound(SoundEnums.fredScream);
    }
}
