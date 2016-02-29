using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class NPC : MonoBehaviour {

    #region Clothing
    [Header("Clothing")]
    public Renderer Hat;
    private NamedColor hatColor;

    public Renderer Shirt;
    private NamedColor shirtColor;

    public Renderer Leggings;
    private NamedColor legColor;
    #endregion

    [Space(10)]
    #region Conversation fields
    [Header("Conversation Settings")]
    public string NPCName;
    public BaseConversation ConversationMenu;
    private BaseConversation _convMenu;

    public List<Clue> Clues = new List<Clue>();
    private bool UseClueCollection;

    public string Clue;
    public bool IsLying;
    public string RealClue;
    private uint silenceTime;
    public bool IsAlive;
    #endregion

    private bool isSelected;

    void Awake() {
        IsAlive = true;
        
        var newDesc = GenerateDescription();
        Hat.GetComponent<Renderer>().material.color = (hatColor = newDesc[0]).InnerColor;
        Shirt.GetComponent<Renderer>().material.color = (shirtColor = newDesc[1]).InnerColor;
        Leggings.GetComponent<Renderer>().material.color = (legColor = newDesc[2]).InnerColor;
        
        _convMenu = Instantiate(ConversationMenu);
        _convMenu.Parent = transform;
        _convMenu.NPCName = NPCName;
        CloseConversation();
    }

    #region Selectable
    void OnMouseDown() {    
        if (!isSelected) {
            var allRenderers = gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in allRenderers) {
                r.material.color += new Color(50, 0, 0);
            }

            OpenConversation();
            isSelected = true;
            GameController.Instance.CountInteraction();
        } else {
            var allRenderers = gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in allRenderers) {
                switch(r.gameObject.name) {
                    case "Hat": r.material.color = hatColor.InnerColor; break;
                    case "Head": r.material.color = Color.white; break;
                    case "Body": r.material.color = shirtColor.InnerColor; break;
                    case "Legs": r.material.color = legColor.InnerColor; break;
                    default: r.material.color = r.material.color; break; 
                }
            }
            CloseConversation();
            isSelected = false;
        }
    }

    void OpenConversation() {
        _convMenu.gameObject.SetActive(true);
        var menuPos = transform.position + new Vector3(0, transform.lossyScale.y + 4.5f, 0);
        _convMenu.transform.position = menuPos;
    }

    void CloseConversation() {
        _convMenu.gameObject.SetActive(false);
    }
    #endregion

    public void SetClue(string newClue) {
        _convMenu.TextGameObject.text = newClue;
    }

    public void SetClue(List<Clue> allClues) {
        Clues = allClues;
        UseClueCollection = true;

        _convMenu.TextGameObject.text = Clue = allClues[0].Statement;
        IsLying = allClues[0].IsLying;
        RealClue = allClues[0].SecondStatement;
        //Debug.Log(gameObject.name + ": UseClueCollection? " + UseClueCollection);
    }

    public void PassToCurrentDay() {
        if (UseClueCollection && IsAlive) { 
            if (Clues.Count < GameController.Instance.DayCount) {
                Murder();
                return;
            }

            var dailyClue = Clues[(int)GameController.Instance.DayCount-1];
            Clue = dailyClue.Statement;
            IsLying = dailyClue.IsLying;
            RealClue = dailyClue.SecondStatement;
            SetClue(Clue);
        }

        var newClothes = GenerateDescription();
        Hat.material.color = (hatColor = newClothes[0]).InnerColor;
        Shirt.material.color = (shirtColor = newClothes[1]).InnerColor;
        Leggings.material.color = (legColor = newClothes[2]).InnerColor;
    }

    public void Murder() {
        IsAlive = false;

        transform.Rotate(new Vector3(90.0f, 0, 0));

        var namedGray = new NamedColor("gray", Color.gray);
        hatColor = namedGray;
        shirtColor = namedGray;
        legColor = namedGray;

        Clue = "...";
        IsLying = false;
        RealClue = "...";
    }
    
    public void Reset() {
        IsAlive = true;
        transform.Rotate(new Vector3(-90, 0, 0));
        PassToCurrentDay();
    }

    private NamedColor[] GenerateDescription() {
        NamedColor[] possibleColors = GameController.GetAllNamedColors();

        NamedColor[] colorDesc = new NamedColor[3];
        for (int i = 0; i < 3; i++) {
            var colorIdx = Random.Range(0, possibleColors.Length - 1);
            colorDesc[i] = possibleColors[colorIdx];
        }
        return colorDesc;
    }

    public string[] GetFullDescription() {
        var hat = string.Format("{0} hat", hatColor.ColorName);
        var shirt = string.Format("{0} shirt", shirtColor.ColorName);
        var leggings = string.Format("{0} legs", legColor.ColorName);
        return new string[] { hat, shirt, leggings };
    }

    public void SetDescription(NamedColor hatColor, NamedColor shirtColor, NamedColor legColor) {
        this.hatColor = hatColor.InnerColor.Equals(new Color()) ? this.hatColor : hatColor;
        this.shirtColor = shirtColor.InnerColor.Equals(new Color()) ? this.shirtColor : shirtColor;
        this.legColor = legColor.InnerColor.Equals(new Color()) ? this.legColor : legColor;
        Debug.Log("Setting description of " + NPCName + " to " +
            string.Format("<{0}, {1}, {2}>", hatColor.ColorName, shirtColor.ColorName, legColor.ColorName));
        Hat.material.color = this.hatColor.InnerColor;
        Shirt.material.color = this.shirtColor.InnerColor;
        Leggings.material.color = this.legColor.InnerColor;
    }
}
