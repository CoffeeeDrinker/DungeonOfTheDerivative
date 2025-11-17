using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAssigner : MonoBehaviour
{
    public List<GameObject> NPCs;
    public TextAsset allDialogueText;
    private string file;

    void Start()
    {
        //Convert text file to string
        file = allDialogueText.text;

        //Loop until there is no more dialgoue
        int i = 0;
        while (file.Length > 0)
        {
            //Check if NPC name is correct
            if(i>0)
                file = file.Substring(file.IndexOf("\n")+1);

            int NPCIndex = FindNPC(file.Substring(0, file.IndexOf("\n")).Trim());
            if (NPCIndex > -1){
                file = file.Substring(file.IndexOf(NPCs[NPCIndex].name.Trim()));
            }
            else
            {
                break;
            }

            if (NPCs[NPCIndex].name.Trim() == file.Substring(0, file.IndexOf("\n")).Trim())
            {
                //Delete the name from the file
                file = file.Substring(file.IndexOf("\n") + 1);

                //Get Dialogue objects to add to the NPCDialogue
                while (file.IndexOf("~") != 0)
                {
                    //Create new Dialogue object and add lines to it
                    Dialogue newDialogue = new Dialogue();
                    while (file.IndexOf("~") > 1)
                    {
                        newDialogue.AddLine(file.Substring(0, file.IndexOf("\n")).Trim());
                        file = file.Substring(file.IndexOf("\n") + 1);
                    }
                    NPCs[NPCIndex].GetComponent<NPCDialogueScript>().NPCDialogue.Add(newDialogue);
                    file = file.Substring(file.IndexOf("~") + 1);
                }
                file = file.Substring(file.IndexOf("~") + 1);
            }
            else
            {
                file = file.Substring(file.IndexOf("~~") + 3).Trim();
            }
            i++;
        }
              
    }

    public int FindNPC(string name)
    {
        for (int i = 0; i < NPCs.Count; i++)
        {
            if (name == NPCs[i].name.Trim())
            {
                return i;
            }
        }
        return -1;
    }
}
