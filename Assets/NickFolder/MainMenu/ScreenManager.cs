using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] GameObject mainScreen;
    [SerializeField] GameObject optionsScreen;
    [SerializeField] GameObject creditScreen;
    [SerializeField] GameObject saveScreen;
    [SerializeField] GameObject newSaveScreen;
    public void SwitchToOptions()
    {
        mainScreen.SetActive(false);
        creditScreen.SetActive(false);
        optionsScreen.SetActive(true);
        saveScreen.SetActive(false);
        newSaveScreen.SetActive(false);
    }

    public void SwitchToCredits()
    {
        mainScreen.SetActive(false);
        creditScreen.SetActive(true);
        optionsScreen.SetActive(false);
        saveScreen.SetActive(false);
        newSaveScreen.SetActive(false);
    }

    public void SwitchToMain()
    {
        mainScreen.SetActive(true);
        creditScreen.SetActive(false);
        optionsScreen.SetActive(false);
        saveScreen.SetActive(false);
        newSaveScreen.SetActive(false);
    }

    public void SwitchToSaves()
    {
        mainScreen.SetActive(false);
        creditScreen.SetActive(false);
        optionsScreen.SetActive(false);
        saveScreen.SetActive(true);
        newSaveScreen.SetActive(false);
    }

    public void SwitchToNewSave()
    {
        mainScreen.SetActive(false);
        creditScreen.SetActive(false);
        optionsScreen.SetActive(false);
        saveScreen.SetActive(false);
        newSaveScreen.SetActive(true);
    }
}
