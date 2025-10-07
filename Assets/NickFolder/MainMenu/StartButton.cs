using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField] ScreenManager screenManager;

    public void StartGame()
    {
        screenManager.SwitchToSaves();
    }
}
