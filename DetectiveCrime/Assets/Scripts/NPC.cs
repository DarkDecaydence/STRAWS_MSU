using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class NPC : MonoBehaviour {

    public static List<string> possibleClothing = new List<string>()
    {
        "Hat", "Shirt", "Leggings"
    };

    [Header("Clothing")]
    public GameObject Hat;
    private NamedColor hatColor;

    public GameObject Shirt;
    private NamedColor shirtColor;

    public GameObject Leggings;
    private NamedColor legColor;
    [Space(10)]


    public BaseConversation ConversationMenu;
    private BaseConversation _convMenu;

    private bool isSelected;

    void Awake()
    {
        var newDesc = GenerateDescription();
        Hat.GetComponent<Renderer>().material.color = (hatColor = newDesc[0]).InnerColor;
        Shirt.GetComponent<Renderer>().material.color = (shirtColor = newDesc[1]).InnerColor;
        Leggings.GetComponent<Renderer>().material.color = (legColor = newDesc[2]).InnerColor;


        _convMenu = Instantiate(ConversationMenu);
        _convMenu.Parent = this;
        CloseConversation();
    }

    void OnMouseDown()
    {        
        if (!isSelected)
        {
            var allRenderers = gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in allRenderers) {
                r.material.color = new Color(255, r.material.color.g, r.material.color.b);
            }

            OpenConversation();
            isSelected = true;
        }
        else
        {
            var allRenderers = gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in allRenderers)
            {
                switch(r.gameObject.name) {
                    case "Hat": r.material.color = hatColor.InnerColor; break;
                    case "Head": r.material.color = Color.white; break;
                    case "Body": r.material.color = shirtColor.InnerColor; break;
                    case "Legs": r.material.color = legColor.InnerColor; break;
                    default: r.material.color = Color.white; break; 
                }
            }
            CloseConversation();
            isSelected = false;
        }
    }

    void OpenConversation()
    {
        _convMenu.gameObject.SetActive(true);
        var menuPos = transform.position + new Vector3(0, transform.lossyScale.y + 4.5f, 0);
        _convMenu.transform.position = menuPos;
    }

    void CloseConversation()
    {
        _convMenu.gameObject.SetActive(false);
    }

    public void SetClue(string newClue)
    {
        _convMenu.TextGameObject.text = newClue;
    }

    private NamedColor[] GenerateDescription()
    {
        NamedColor[] possibleColors = GameController.GetAllNamedColors();

        NamedColor[] colorDesc = new NamedColor[3];
        int i = 0;
        foreach (string s in possibleClothing)
        {
            var colorIdx = Random.Range(0, possibleColors.Length - 1);
            colorDesc[i++] = possibleColors[colorIdx];
        }
        return colorDesc;
    }

    public string[] GetFullDescription()
    {
        var hat = string.Format("{0} hat", hatColor.ColorName);
        var shirt = string.Format("{0} shirt", shirtColor.ColorName);
        var leggings = string.Format("{0} legs", legColor.ColorName);
        return new string[] { hat, shirt, leggings };
    }
}
