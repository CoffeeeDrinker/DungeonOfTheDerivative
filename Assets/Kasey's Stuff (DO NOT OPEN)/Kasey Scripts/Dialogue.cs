using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Creates a Dialogue object that contains a list of lines that the NPC will say when the user talks to them
public class Dialogue
{
    public List<string> dialogueLine = new List<string>();

    public void AddLine(string newLine)
    {
        dialogueLine.Add(newLine);
    }

    public string GetLine(int index)
    {
        return dialogueLine[index];
    }

    public string GetAllLines()
    {
        string allLines = "";
        foreach (string line in dialogueLine)
        {
            allLines += line+"\n";
        }
        return allLines;
    }
}
