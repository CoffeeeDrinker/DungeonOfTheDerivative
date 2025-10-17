using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOptionsButton : MonoBehaviour
{
    [SerializeField] GameObject optionsScreen;
    [SerializeField] GameObject pauseScreen;
    public void SwitchToOptions()
    {
        pauseScreen.SetActive(false);
        optionsScreen.SetActive(true);
    }
}
