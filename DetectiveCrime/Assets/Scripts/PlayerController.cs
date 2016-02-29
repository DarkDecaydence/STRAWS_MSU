using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	void Start () {
        StartGame01();
	}

    private void StartGame01() {
        GameController.Construct();
        NPCController.Construct();
        NPCController.Instance.SetupGame01();
        GameController.Instance.PassDay();
    }
}
