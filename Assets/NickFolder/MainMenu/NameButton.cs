using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NameButton : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        string[] lines = File.ReadAllLines(CurrentSave.currSave);
        lines[17] = inputField.text;
        lines[18] = "Started true";
        File.WriteAllLines(CurrentSave.currSave, lines);
        SceneManager.LoadScene("CombinedScene");
    }
}
