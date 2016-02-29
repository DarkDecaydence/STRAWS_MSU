using System;
using UnityEngine;

public class BaseConversation : MonoBehaviour, IConversation
{
    public TextMesh TextGameObject;
    public string ClueText {
        get { return TextGameObject.text; }
        set { TextGameObject.text = value; }
    }
    public TextMesh NameGameObject;
    public string NPCName {
        get { return NameGameObject.text; }
        set { NameGameObject.text = value; }
    }

    public Transform Parent {
        get { return transform.parent; }
        set { transform.parent = value; }
    }

    private NPC NPCParent { get; set; }

    void Start() {
        NPCParent = Parent.GetComponent<NPC>();
        Debug.Log(NPCName);
        GetComponentInChildren<ArrestScript>().Arrest_Pressed += ArrestScript_Arrest_Pressed;
        GetComponentInChildren<CallLiarScript>().Liar_Pressed += LiarScript_Liar_Pressed;
    }

    private void LiarScript_Liar_Pressed(object sender, EventArgs e) {
        Debug.Log(string.Format("You called {0} a liar!", Parent.gameObject.name));
        if (NPCParent.IsLying) {
            Debug.Log("They were lying! Listen to them squeel.");
            ClueText = NPCParent.RealClue;
        } else {
            Debug.Log("They aren't lying you shitlord! Now you offended them.");
            ClueText = "I'm not talking to you!";
        }
    }

    private void ArrestScript_Arrest_Pressed(object sender, EventArgs e) {
        Debug.Log("You tried to arrest: " + transform.parent.gameObject.name);
        if (Parent.GetComponent<Murderer>() != null) {
            Debug.Log("They are the Murderer! Congratz!");
        } else {
            Debug.Log("They are not the Murderer! Shit!");
        }
    }

    #region IConversation
    public void SetClueText(string s) {
        ClueText = s;
    }
    #endregion
}
