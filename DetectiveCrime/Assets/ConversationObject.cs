using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class ConversationObject : MonoBehaviour {

    public static List<string> possibleClothing = new List<string>()
    {
        "Tophat", "Sweater", "Shorts", "Trousers",
        "Boxers", "Bowlerhat", "Necklace", "Eyepatch",
        "Scar", "Cap", "T-shirt", "Tanktop"
    };
    public static List<string> possibleColors = new List<string>()
    {
        "Red", "Green", "Blue", "Magenta",
        "Yellow", "Cyan", "Purple", "Pink",
        "Brown", "Black", "White", "Turquoise"
    };

    public List<string> descriptors;
    public GameObject conversationText;
    public GameObject arrestText;
    private GameObject _convText;
    private GameObject _arrestText;
    private bool isSelected;

    void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            var newDesc = GenerateDesc();
            descriptors.Add(newDesc);
            Debug.Log(newDesc);
        }
        _convText = Instantiate(conversationText);
        _arrestText = Instantiate(arrestText);
        _arrestText.GetComponent<ArrestScript>().SetNPC(gameObject);
        CloseConversation();
    }

    void OnMouseDown()
    {
        var allRenderers = gameObject.GetComponentsInChildren<Renderer>();
        List<Material> visitedMaterials = new List<Material>();
        
        if (!isSelected)
        {
            foreach (Renderer r in allRenderers)
            {
                if (!visitedMaterials.Contains(r.material))
                {
                    r.material.color = Color.red;
                    visitedMaterials.Add(r.material);
                }
            }
            OpenConversation();
            isSelected = true;
        }
        else
        {
            foreach (Renderer r in allRenderers)
            {
                if (!visitedMaterials.Contains(r.material))
                {
                    r.material.color = Color.white;
                    visitedMaterials.Add(r.material);
                }
            }
            CloseConversation();
            isSelected = false;
        }
    }

    void OpenConversation()
    {
        _arrestText.gameObject.SetActive(true);
        _convText.gameObject.SetActive(true);
        var menuPos = transform.position + new Vector3(0, transform.localScale.y * 4, 0);
        _arrestText.transform.position = menuPos;
        _convText.transform.position = menuPos + new Vector3(0, 1.1f, 0);
    }

    void CloseConversation()
    {
        _convText.gameObject.SetActive(false);
        _arrestText.gameObject.SetActive(false);
    }

    public void SetClue(string newClue)
    {
        _convText.GetComponent<TextMesh>().text = newClue;
    }

    private string GenerateDesc()
    {
        var colorIdx = UnityEngine.Random.Range(0, possibleColors.Count() - 1);
        var clothingIdx = UnityEngine.Random.Range(0, possibleClothing.Count - 1);
        return string.Format("{0} {1}", possibleColors[colorIdx], possibleClothing[clothingIdx]);
    }
}
