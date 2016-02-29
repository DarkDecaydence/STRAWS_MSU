using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ClueFactory {
    public static List<List<Clue>> GenerateGame01() {
        var npc1 = new List<Clue>();
        var npc2 = new List<Clue>();
        var npc3 = new List<Clue>();
        var npc4 = new List<Clue>();
        var npc5 = new List<Clue>();
        var npc6 = new List<Clue>();
        var npc7 = new List<Clue>();
        var npc8 = new List<Clue>();
        var npc9 = new List<Clue>();
        var npc10 = new List<Clue>();

        /*Murderer wears red hat*/
        npc1.Add(new Clue("The murderer wears a red hat"));
        npc2.Add(new Clue("The murderer wears a green shirt.", true, "No. 1 is right."));
        npc3.Add(new Clue("No. 7 is lying."));
        npc4.Add(new Clue("No. 1 is right."));
        npc5.Add(new Clue("No. 7 never lies.", true, "No. 6 is right."));
        npc6.Add(new Clue("No. 8 was there."));
        npc7.Add(new Clue("No. 1 is lying.", true, "No. 1 was there."));
        npc8.Add(new Clue("No. 9 is lying.", true));
        npc9.Add(new Clue("No. 3 is right."));
        npc10.Add(new Clue("No. 4 is lying.", true, "No. 8 was there."));

        /* KILL NPC1! */

        /*Murderer wears black shirt*/
        npc2.Add(new Clue("No. 8 was there."));
        npc3.Add(new Clue("No. 6 was there.", true, "The murderer wears a black shirt."));
        npc4.Add(new Clue("No. 9 is right."));
        npc5.Add(new Clue("The murderer wears a yellow hat.", true, "No. 3 was there"));
        npc6.Add(new Clue("No. 9 was there.", true, "No 2 is right."));
        npc7.Add(new Clue("No. 10 is right."));
        npc8.Add(new Clue("No. 6 was there.", true));
        npc9.Add(new Clue("No. 7 is right."));
        npc10.Add(new Clue("No. 3 is lying."));

        var returnList = new List<List<Clue>>();
        returnList.Add(npc1);
        returnList.Add(npc2);
        returnList.Add(npc3);
        returnList.Add(npc4);
        returnList.Add(npc5);
        returnList.Add(npc6);
        returnList.Add(npc7);
        returnList.Add(npc8);
        returnList.Add(npc9);
        returnList.Add(npc10);

        return returnList;
    }
}
