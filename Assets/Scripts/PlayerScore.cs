using UnityEngine;
using System.Collections;

public class PlayerScore : MonoBehaviour {
	public int playerId;
	
	// Update is called once per frame
	void Update () {
		GetComponent<TextMesh>().text = "" + GameManager.Instance.getPlayerScore (playerId);
	}
}
