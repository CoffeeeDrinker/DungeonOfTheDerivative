using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] List<GridSlot> gridSlots;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Item item, int amount)
    {
        foreach(GridSlot gridSlot in gridSlots)
        {
            if(gridSlot.GetItemName() == item.GetName())
            {
                Debug.Log(item.GetName());
                gridSlot.AddItem(item, amount);
                return;
            }
        }

        foreach(GridSlot gridSlot in gridSlots)
        {
            if(!(gridSlot.HasItem()))
            {
                Debug.Log("Hi!");
                gridSlot.AddItem(item, amount);
                return;
            }
        }

    }
}
