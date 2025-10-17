using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//NOTE: This class does NOTHING right now, might delete later
public interface IButton
{
    public void OnButtonPressed();
    public void Unclick();
    public bool IsClicked();

}
