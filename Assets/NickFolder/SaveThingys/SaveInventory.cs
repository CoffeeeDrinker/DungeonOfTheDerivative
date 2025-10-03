using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveInventory : MonoBehaviour
{
    [SerializeField] string savePath;
    [SerializeField] Grid inventory;
    void Start()
    {
        savePath = Application.dataPath + savePath;
    }

    public void Save()
    {
        string saveFile = "";

        List<GridSlot> slots = new List<GridSlot>();
        foreach(GridSlot slot in slots)
        {
            if(!slot.HasItem())
            {
                saveFile += ItemNameEnum.ERROR + " " + 0 + "\n";
            } else
            {
                saveFile += slot.GetItemName() + " " + slot.GetItemAmount() + "\n";
            }
        }

        File.WriteAllText(savePath, saveFile);
    }

    public void LoadGrid()
    {
        string[] lines = File.ReadAllLines(savePath);
        inventory.LoadGrid(lines);
    }
}
