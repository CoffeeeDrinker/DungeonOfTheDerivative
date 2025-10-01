using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    [SerializeField] ScreenManager screenManager;

    public void SwitchToOptions()
    {
        screenManager.SwitchToOptions();
    }
}
