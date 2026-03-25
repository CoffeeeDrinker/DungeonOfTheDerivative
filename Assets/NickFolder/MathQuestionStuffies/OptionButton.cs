using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionButton : MonoBehaviour
{
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
        transform.parent.parent.GetComponent<Options>().OptionChosen(transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
    }
}
