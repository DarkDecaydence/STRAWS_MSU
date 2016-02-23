using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class DummySelector : MonoBehaviour {

    private List<NPC> children;

	void Start () {
        children = (from convObj in GetComponentsInChildren<NPC>()
                   select convObj).ToList();
        var murderer = Random.Range(0, children.Count - 1);

        children[murderer].gameObject.AddComponent<Murderer>();
        var murdererDesc = children[murderer].GetFullDescription();

        foreach (NPC go in children)
        {
            var hintIdx = Random.Range(0, 3);
            go.SetClue(string.Format("The murderer wears a {0}", murdererDesc[hintIdx]));
        }
	}
}
