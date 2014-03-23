using UnityEngine;
using System.Collections;

public class PlayerScore : MonoBehaviour {
	public int playerId;
	
	// Update is called once per frame
	void Update () {		
		string key = "Player" + playerId + "Score";
		GetComponent<TextMesh>().text = "" + PlayerPrefs.GetInt (key);
	}
}
