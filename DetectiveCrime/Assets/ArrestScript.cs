using UnityEngine;
using System.Collections;

public class ArrestScript : MonoBehaviour {

    public GameObject connectedNPC;

    void OnMouseDown()
    {
        Debug.Log("You tried to arrest: " + connectedNPC);
        if (connectedNPC.GetComponent<Murderer>() != null)
        {
            Debug.Log("He is the Murderer! Congratz!");
        } else
        {
            Debug.Log("He is not the Murderer! Shit!");
        }
    }

    public void SetNPC(GameObject npc)
    {
        connectedNPC = npc;
    }
}
