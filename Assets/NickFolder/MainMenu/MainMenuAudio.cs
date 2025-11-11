using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAudio : MonoBehaviour
{
    int audio;
    [SerializeField] Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeAudio()
    {
        audio = (int)slider.value;
        string[] arr1 = File.ReadAllLines(CurrentSave.save1);
        arr1[16] = "Volume " + GetComponent<AudioSource>();
        File.WriteAllLines(CurrentSave.save1, arr1);
        arr1 = File.ReadAllLines(CurrentSave.save2);
        arr1[16] = "Volume " + GetComponent<AudioSource>();
        File.WriteAllLines(CurrentSave.save2, arr1);
        arr1 = File.ReadAllLines(CurrentSave.save3);
        arr1[16] = "Volume " + GetComponent<AudioSource>();
        File.WriteAllLines(CurrentSave.save3, arr1);
    }
}
