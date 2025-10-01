using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditButton : MonoBehaviour
{
    [SerializeField] ScreenManager screenManager;
    public void SwitchToCredits()
    {
        screenManager.SwitchToCredits();
    }
}
