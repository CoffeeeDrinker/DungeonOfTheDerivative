using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TestThingyScript : MonoBehaviour
{
    string path = Application.dataPath + "/NickFolder/PlayerSaves/InventorySaveOne.txt";

    // Start is called before the first frame update
    void Start()
    {
        File.AppendAllText(path, "Work?");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
