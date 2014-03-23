using UnityEngine;
using System.Collections;

public class PlayerCoinGUI : MonoBehaviour {

    public PlayerController player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<TextMesh>().text = "" + player.coins;
	}
}
