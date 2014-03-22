using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    private static int winningPlayerId = -1;

    public GUIText winText;

    private static float restartTimer;

	// Use this for initialization
    void Start() {
        winText.text = "";
        restartTimer = -1;
	}
	
	// Update is called once per frame
	void Update () {
        if (winningPlayerId > 0) {
            winText.text = "Player " + winningPlayerId + " survived!\nPlayer " + (winningPlayerId % 2 + 1) + ", you're lemonade :(";
        }

        if (restartTimer > -1) {
            restartTimer += Time.deltaTime;

            if (restartTimer > 2.0f) {
                RestartLevel();
            }
        }
	}

    public static void EndGame(int winningPlayer)
    {
        winningPlayerId = winningPlayer;
        restartTimer = 0;
        print("player " + winningPlayerId + " won");
    }

    private void RestartLevel() {
        print("restart");
        Application.LoadLevel(Application.loadedLevel);
    }
}
