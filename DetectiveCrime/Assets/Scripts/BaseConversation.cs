using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BaseConversation : MonoBehaviour, IConversation
{
    public TextMesh TextGameObject;
    public string ClueText
    {
        get { return TextGameObject.text; }
        set { TextGameObject.text = value; }
    }

    public NPC Parent { get; set; }

    void Start() {
        GetComponentInChildren<ArrestScript>().Arrest_Pressed += ArrestScript_Arrest_Pressed;
    }

    private void ArrestScript_Arrest_Pressed(object sender, EventArgs e)
    {
        Debug.Log("You tried to arrest: " + Parent.gameObject.name);
        if (Parent.GetComponent<Murderer>() != null) {
            Debug.Log("He is the Murderer! Congratz!");
        } else {
            Debug.Log("He is not the Murderer! Shit!");
        }
    }

    #region IConversation
    public void SetClueText(string s) {
        ClueText = s;
    }
    #endregion
}
