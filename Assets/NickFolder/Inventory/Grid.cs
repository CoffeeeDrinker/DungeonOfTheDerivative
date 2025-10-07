using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] List<GridSlot> gridSlots;
    [SerializeField] ItemManager itemManager;

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

    public List<GridSlot> GetGridSlots()
    {
        return gridSlots;
    }

    public void LoadGrid(string[] lines)
    {
        for(int i=0; i<lines.Length; i++)
        {
            string line = lines[i];
            string[] parts = line.Split(' ');

            if (parts[0] == ItemNameEnum.ERROR.ToString())
            {
                continue;
            } else
            {
                Item item = gameObject.AddComponent<Item>();
                item.SetName((ItemNameEnum)Enum.Parse(typeof(ItemNameEnum), parts[0]));
                item.SetSprite(itemManager.GetSprite(item.GetName()));
                gridSlots[i].RemoveItem();
                gridSlots[i].AddItem(item, int.Parse(parts[1]));
            }
        }
    }
}
