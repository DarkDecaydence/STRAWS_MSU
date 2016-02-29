using UnityEngine;
using System;

public class CallLiarScript : MonoBehaviour {

    public delegate void ButtonPressedHandler(object sender, EventArgs e);
    public event ButtonPressedHandler Liar_Pressed;

    void OnMouseDown()
    {
        var temp = Liar_Pressed;
        if (temp != null) {
            temp(this, EventArgs.Empty);
        }
        GameController.Instance.CountInteraction();
    }
}
