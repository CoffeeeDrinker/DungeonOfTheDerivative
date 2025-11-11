using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveButton : MonoBehaviour
{
    [SerializeField] int save;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseSave()
    {
        switch(save)
        {
            case 1:
                CurrentSave.SetSaveOne();
                break;
            case 2:
                CurrentSave.SetSaveTwo();
                break;
            case 3:
                CurrentSave.SetSaveThree();
                break;
        }

        SceneManager.LoadScene("CombinedScene");
    }
}
