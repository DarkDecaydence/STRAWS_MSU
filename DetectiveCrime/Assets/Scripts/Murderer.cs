using UnityEngine;
using System.Collections.Generic;
using System;

public class Murderer : MonoBehaviour {

    public delegate void MurderDayPassHandler(object sender, EventArgs e);
    private List<NamedColor[]> clothingPlan = new List<NamedColor[]>();

    public List<NamedColor[]> ClothingPlan {
        get { return clothingPlan; }
        set { clothingPlan = value; }
    }
    public NPC ThisNpc;

    public Murderer() {
        GameController.MurderDayPass += (s, e) => PassToCurrentDay();
        ThisNpc = gameObject.GetComponent<NPC>();
    }

    public void PassToCurrentDay() {
        var dailyClothes = new NamedColor[3];
        if (clothingPlan.Count >= GameController.Instance.DayCount)
            dailyClothes = clothingPlan[(int)GameController.Instance.DayCount-1];

        ThisNpc.SetDescription(dailyClothes[0], dailyClothes[1], dailyClothes[2]);
    }

}
