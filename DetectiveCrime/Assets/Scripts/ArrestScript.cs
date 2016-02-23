using UnityEngine;
using System.Collections;
using System;

public class ArrestScript : MonoBehaviour {
    
    public delegate void ButtonPressedHandler(object sender, EventArgs e);
    public event ButtonPressedHandler Arrest_Pressed;

    void OnMouseDown()
    {
        var temp = Arrest_Pressed;
        if (temp != null) {
            temp(this, EventArgs.Empty);
        }
    }
}
