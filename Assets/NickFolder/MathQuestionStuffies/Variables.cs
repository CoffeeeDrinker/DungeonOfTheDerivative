using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Variables : MonoBehaviour, AnswerInput
{
    [SerializeField] GameObject variablePrefab;
    List<TMP_InputField> list;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Create(List<string> variables)
    {
        list = new List<TMP_InputField>();
        for(int i=0; i<variables.Count; i++)
        {
            GameObject variable = Instantiate(variablePrefab, transform.GetChild(0));
            variable.GetComponentInChildren<TextMeshProUGUI>().text = variables[i];
            list.Add(variable.GetComponentInChildren<TMP_InputField>());
        }
    }

    public string GetInput()
    {
        string ans = "";
        foreach(TMP_InputField input in list)
        {
            if(!(ans == ""))
            {
                ans += " ";
            }
            ans += input.text;
        }
        return ans;
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    public void ResetField()
    {
        for(int i=0; i<list.Count; i++)
        {
            Destroy(transform.GetChild(0).GetChild(i));
        }
        list.Clear();
    }
}
