using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController
{
    #region Singleton fields
    public static GameController Instance { get; private set; }
    public static void Construct() { Instance = new GameController(); }
    #endregion

    #region Instance fields
    public uint DailyInteractions;
    public uint DayCount;
    private uint remainingInteractions;
    #endregion

    public void CountInteraction() {
        remainingInteractions--;
        if (remainingInteractions == 0) {
            PassDay();
        }
    }

    private GameController() {
        remainingInteractions = DailyInteractions = 10;
        DayCount = 0;
    }

    #region Instance methods
    public void PassDay() {
        remainingInteractions = DailyInteractions;
        DayCount++;
        Debug.Log("PASSING ON TO DAY " + DayCount);

        foreach (NPC npc in NPCController.Instance.AllNPCs) {
            npc.PassToCurrentDay();
        }

        var temp = MurderDayPass;
        if (temp != null) {
            Debug.Log("Murderer passes day!");
            MurderDayPass(this, EventArgs.Empty);
        }
    }

    public static Murderer.MurderDayPassHandler MurderDayPass;

    public void CommitMurder() {

    }
    #endregion

    #region Static methods
    public static NamedColor[] GetAllNamedColors()
    {
        Type colorType = typeof(Color);
        PropertyInfo[] allColorProperties = colorType.GetProperties();
        PropertyInfo[] onlyStaticColorProperties = allColorProperties.Where(cp => cp.GetGetMethod().IsStatic).ToArray();

        Color c = new Color();
        string c_Name = string.Empty;
        List<NamedColor> allColors = new List<NamedColor>();
        for (int i = 0; i < onlyStaticColorProperties.Length; i++) {
            MethodInfo mi = onlyStaticColorProperties[i].GetGetMethod();
            c_Name = onlyStaticColorProperties[i].Name;
            c = (Color)mi.Invoke(onlyStaticColorProperties[i], new object[] { });
            if (c.Equals(Color.clear)) continue;
            else allColors.Add(new NamedColor(c_Name, c));
        }

        return allColors.ToArray();
    }
    #endregion
}
