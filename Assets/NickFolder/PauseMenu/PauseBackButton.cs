using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBackButton : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject optionsScreen;

    public void SwitchToPause()
    {
        pauseScreen.SetActive(true);
        optionsScreen.SetActive(false);
    }
}
