using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveButton : MonoBehaviour
{
    [SerializeField] int save;
    [SerializeField] ScreenManager screenManager;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseSave()
    {
        switch(save)
        {
            case 1:
                CurrentSave.SetSaveOne();
                break;
            case 2:
                CurrentSave.SetSaveTwo();
                break;
            case 3:
                CurrentSave.SetSaveThree();
                break;
        }

        if (!hasStarted())
        {
            screenManager.SwitchToNewSave();
        } else
        {
            SceneManager.LoadScene("CombinedScene");
        }
    }

    public bool hasStarted()
    {
        string[] lines = File.ReadAllLines(CurrentSave.currSave);
        return lines[18].Split(" ")[1] == "true";
    }
}
