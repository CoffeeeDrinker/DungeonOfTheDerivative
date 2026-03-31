using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour, AnswerInput
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Sprite buttonNotSelected;
    [SerializeField] Sprite buttonSelected;
    private GameObject[] buttons;
    private string chosen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Create(string[] options)
    {
        buttons = new GameObject[options.Length];
        for(int i=0; i<options.Length; i++)
        {
            GameObject curr = Instantiate(buttonPrefab, transform.GetChild(0));
            curr.GetComponentInChildren<TextMeshProUGUI>().text = options[i];
            buttons[i] = curr;
        }
    }

    public string GetInput()
    {
        if(chosen == "")
        {
            return "WRONG";
        }

        return chosen;
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    public void ResetField()
    {
        for(int i=0; i<buttons.Length; i++)
        {
            Destroy(buttons[i]);
        }

        chosen = "";
    }

    public void OptionChosen(string chosen)
    {
        for(int i=0; i<buttons.Count(); i++)
        {
            if (buttons[i].GetComponentInChildren<TextMeshProUGUI>().text == chosen)
            {
                buttons[i].GetComponent<Image>().sprite = buttonSelected;
            } else
            {
                buttons[i].GetComponent<Image>().sprite = buttonNotSelected;
            }
        }
        this.chosen = chosen;
    }
}
