using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Creates a Dialogue object that contains a list of lines that the NPC will say when the user talks to them
public class Dialogue
{
    public List<string> dialogueLines = new List<string>();

    public void AddLine(string newLine)
    {
        if(newLine != "" && newLine != null)
            dialogueLines.Add(newLine.TrimStart('\n').TrimEnd('\n'));
    }

    public string GetLine(int index)
    {
        return dialogueLines[index];
    }

    public int NumLines()
    {
        return dialogueLines.Count;
    }

    public string GetAllLines()
    {
        string allLines = "";
        foreach (string line in dialogueLines)
        {
            allLines += "Line: "+line+"\n";
        }
        return allLines;
    }
}
