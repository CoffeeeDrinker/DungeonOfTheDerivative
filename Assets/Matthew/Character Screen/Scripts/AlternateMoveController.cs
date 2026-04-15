using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlternateMoveController : MonoBehaviour
{
    [SerializeField] GameObject moveHolder;
    [SerializeField] GameObject textObject;
    [SerializeField] bool Unlocked;
    Move activeMove;
    // Start is called before the first frame update
    void Start()
    {
        activeMove = moveHolder.GetComponent<MoveContainer>().GetMove();
        if (!Unlocked)
        {
            GetComponent<GameObject>().SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (!Unlocked)
        {
            GetComponent<GameObject>().SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (textObject.GetComponent<TextMeshProUGUI>().text != activeMove.name)
            textObject.GetComponent<TextMeshProUGUI>().text = activeMove.name;
    }

    public void OnClick()
    {
        ActiveMoveMaster.moveMaster.Reselect(activeMove, this);
        textObject.GetComponent<TextMeshProUGUI>().color = Color.gray;//new Color(179, 179, 179);
    }

    public void SetMove(Move move)
    {
        activeMove = move;
    }

    public Move GetMove() { return activeMove; }

    public void ResetText()
    {
        textObject.GetComponent<TextMeshProUGUI>().color = new Color(255, 255, 255);
    }
}
