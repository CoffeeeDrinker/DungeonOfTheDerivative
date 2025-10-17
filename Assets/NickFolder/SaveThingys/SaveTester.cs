using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTester : MonoBehaviour
{
    [SerializeField] SaveInventory inventory;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveTest()
    {
        inventory.Save();
    }

    public void LoadTest()
    {
        inventory.LoadGrid();
    }


}
