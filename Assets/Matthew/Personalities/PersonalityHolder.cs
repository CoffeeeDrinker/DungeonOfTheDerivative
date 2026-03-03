using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalityHolder : MonoBehaviour
{
    [SerializeField] string personalityName;
    Algorithm personality;
    // Start is called before the first frame update
    void Start()
    {
        List<Algorithm> algorithms = new List<Algorithm>() {
            AttackAI.AGGRESSIVE,
            AttackAI.MODERATE,
            AttackAI.CAUTIOUS,
            AttackAI.TRICKY
        };
        for (int i = 0; i < algorithms.Count; i++)
        {
            if(algorithms[i].name == personalityName)
            {
                personality = algorithms[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Algorithm GetPersonality()
    {
        return personality;
    }
}
