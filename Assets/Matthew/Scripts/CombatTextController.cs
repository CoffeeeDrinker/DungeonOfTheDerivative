using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatTextController : MonoBehaviour
{
    [SerializeField] GameObject background;
    [SerializeField] GameObject textField;
    [SerializeField] string textSpeed; //textSpeet must be set as a string key in textSpeedDict
    TextMeshProUGUI text;
    static Dictionary<string, float> textSpeedDict = new Dictionary<string, float>() {
        //Contains text speed settings and the corresponding coefficinets to be used in method TypeTextOverTime based on desired speed
        {"Slow", 1.1f },
        {"Medium", 2f },
        {"Fast", 3f },
        {"Instant", 0f }
    };
    
    // Start is called before the first frame update
    void Start()
    {
        text = textField.GetComponent<TextMeshProUGUI>();
        background.SetActive(false);
        //text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle()
    {
        background.SetActive(!background.activeSelf);
    }

    public void DisplayText(string displayText)
    {
        text.text = displayText;
    }

    //Types out displayText over time period duration
    public void DisplayText(string displayText, float duration)
    {
        if (textSpeedDict[textSpeed] == 0f)
        {
            text.text = displayText;
        }
        else
        {
            text.text = "";
            StartCoroutine(TypeTextOverTime(displayText, duration));
        }
    }

    private IEnumerator TypeTextOverTime(string endText, float duration)
    {
        char[] chars = endText.ToCharArray();
        int current = 0;
        while (current < chars.Length)
        {
            text.text = text.text + chars[current];
            current++;
            yield return new WaitForSeconds(duration / (chars.Length * textSpeedDict[textSpeed]));
        }
    }
}
