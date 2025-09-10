using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempItemAdder : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory.AddItem("TestItem1", 1);
        inventory.AddItem("TestItem1", 4);
        inventory.AddItem("TestItem2", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
