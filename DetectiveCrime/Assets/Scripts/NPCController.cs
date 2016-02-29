using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class NPCController {

    public static NPCController Instance { get; private set; }
    public static void Construct() { Instance = new NPCController(); }

    private List<NPC> children;
    public List<NPC> AllNPCs { get { return children; } }

    private NPCController() {
        children = Object.FindObjectsOfType<NPC>().ToList();
        SetupGame01();
    }

    public void SetupGame01() {
        var gameTemplate = new {
            ClueList = ClueFactory.GenerateGame01(),
            MurdererClothing =
            new List<NamedColor[]>() {
                new NamedColor[] {
                    new NamedColor("red", Color.red),
                    new NamedColor(),
                    new NamedColor()
                },
                new NamedColor[] {
                    new NamedColor(),
                    new NamedColor("black", Color.black),
                    new NamedColor()
                }
            }
        };

        for (int i = 0; i < gameTemplate.ClueList.Count; i++) {
            children[i].SetClue(gameTemplate.ClueList[i]);
            if (i == 2) { // Apparently, i = 2 <=> NPC(7) / No. 8
                children[i].gameObject.AddComponent<Murderer>()
                    .ClothingPlan = gameTemplate.MurdererClothing; }
        }

    }
    
    public void SetupProtoGame() {
        var murderer = Random.Range(0, children.Count - 1);

        children[murderer].gameObject.AddComponent<Murderer>();
        var murdererDesc = children[murderer].GetFullDescription();

        foreach (NPC go in children) {
            var hintIdx = Random.Range(0, 3);
            go.SetClue(go.Clue);
        }
    }
}
