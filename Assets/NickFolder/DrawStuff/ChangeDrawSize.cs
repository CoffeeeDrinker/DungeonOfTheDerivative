using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDrawSize : MonoBehaviour
{
    [SerializeField] Draw drawer;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMP_InputField>().onValueChanged.AddListener(delegate { ValueChanged(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ValueChanged()
    {
        drawer.ChangeDrawSize(int.Parse(GetComponent<TMP_InputField>().text));
    }
    
}
