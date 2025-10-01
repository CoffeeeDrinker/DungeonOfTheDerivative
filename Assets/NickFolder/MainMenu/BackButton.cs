using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    [SerializeField] ScreenManager screenManager;

    public void SwitchToMain()
    {
        screenManager.SwitchToMain();
    }
}
