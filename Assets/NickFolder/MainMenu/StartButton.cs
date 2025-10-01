using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField] string sceneName;

    public void StartGame()
    {
        SceneManager.LoadScene(sceneName);
    }
}
