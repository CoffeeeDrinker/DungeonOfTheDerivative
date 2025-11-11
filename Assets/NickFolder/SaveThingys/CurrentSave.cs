using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public static class CurrentSave
{
    public static string save1 = Application.dataPath + "/NickFolder/PlayerSaves/InventorySaveOne.txt";
    public static string save2 = Application.dataPath + "/NickFolder/PlayerSaves/InventorySaveTwo.txt";
    public static string save3 = Application.dataPath + "/NickFolder/PlayerSaves/InventorySaveThree.txt";
    public static string currSave = save1;

    public static void SetSaveOne()
    {
        currSave = save1;
    }

    public static void SetSaveTwo()
    {
        currSave = save2;
    }

    public static void SetSaveThree()
    {
        currSave = save3;
    }

}
