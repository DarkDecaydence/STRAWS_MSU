using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class DummySelector : MonoBehaviour {

    private GameObject[] children;

	// Use this for initialization
	void Start () {
        children = (from convObj in GetComponentsInChildren<ConversationObject>().ToList()
                   select convObj.gameObject).ToArray();
        var murderer = Random.Range(0, children.Length - 1);

        children[murderer].AddComponent<Murderer>();
        var murdererDesc = children[murderer].GetComponent<ConversationObject>().descriptors;

        foreach (GameObject go in children)
        {
            var newClueIdx = Random.Range(0, murdererDesc.Count() - 1);
            go.GetComponent<ConversationObject>().SetClue(murdererDesc[newClueIdx]);
        }
	}
}
