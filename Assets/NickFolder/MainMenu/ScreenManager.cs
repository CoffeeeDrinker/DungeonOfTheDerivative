using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] GameObject mainScreen;
    [SerializeField] GameObject optionsScreen;
    [SerializeField] GameObject creditScreen;

    public void SwitchToOptions()
    {
        mainScreen.SetActive(false);
        creditScreen.SetActive(false);
        optionsScreen.SetActive(true);
    }

    public void SwitchToCredits()
    {
        mainScreen.SetActive(false);
        creditScreen.SetActive(true);
        optionsScreen.SetActive(false);
    }

    public void SwitchToMain()
    {
        mainScreen.SetActive(true);
        creditScreen.SetActive(false);
        optionsScreen.SetActive(false);
    }
}
