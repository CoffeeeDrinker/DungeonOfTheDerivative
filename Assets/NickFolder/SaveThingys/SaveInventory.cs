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
        savePath = CurrentSave.currSave;
        LoadGrid();
    }

    public void Save()
    {
        string saveFile = "";

        List<GridSlot> slots = inventory.GetGridSlots();

        foreach(GridSlot slot in slots)
        {
            if(!slot.HasItem())
            {
                saveFile += ItemNameEnum.ERROR + " " + 0 + "\n";
            } else
            {
                Debug.Log(slot.GetItemName());
                saveFile += slot.GetItemName() + " " + slot.GetItemAmount() + "\n";
            }
        }

        File.WriteAllText(savePath, saveFile);
    }

    public void LoadGrid()
    {
        string[] lines = File.ReadAllLines(savePath);
        string[] grid = new string[15];

        for(int i=0; i<15; i++)
        {
            grid[i] = lines[i];
        }

        inventory.LoadGrid(grid);

        string[] ducks = lines[15].Split(' ');
        Debug.Log(ducks[1]);
        PlayerManager.SetDucks(int.Parse(ducks[1]));

        string[] volume = lines[16].Split(' ');
        SoundManager.Instance.SetVolumeSFX(int.Parse(volume[1]));
    }
}
