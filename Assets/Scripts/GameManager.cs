using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private static GameManager _instance;

    private int winningPlayerId = -1;
    private float restartTimer;

	void Awake() {
		if (GameManager.Instance != null && GameManager.Instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			_instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
	
	void OnDestroy() {
		print ("destroy old GameManager");
	}

	// Use this for initialization
    void Start() {
		StartGame ();
	}

	private void Instantiate() {
		if (_instance != null && _instance != this)	{
			Destroy(this.gameObject);
			return;
		} else {
			_instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        if (restartTimer > -1) {
			restartTimer += Time.deltaTime;

            if (restartTimer > 2.0f) {
                RestartLevel();
            }
        }
	}

	public void StartGame() {
		print ("start game");
		restartTimer = -1;
	}

    public void EndGame(int winningPlayer)
    {
        winningPlayerId = winningPlayer;
        restartTimer = 0;
		KnifeController knife = GameObject.FindObjectOfType (typeof(KnifeController)) as KnifeController;
		knife.SetEnabled (false);
		print("Player " + winningPlayerId + " survived!\nPlayer " + (winningPlayerId % 2 + 1) + ", you're lemonade :(");

		GrantPoints ();
	}

	private void GrantPoints() {
		string key = "Player" + winningPlayerId + "Score";
		PlayerPrefs.SetInt (key, PlayerPrefs.GetInt (key) + 1);
	}

    private void RestartLevel() {
        print("restart");
        Application.LoadLevel(Application.loadedLevel);
		StartGame ();
    }

	public static GameManager Instance {
		get {
			return _instance;
		}
	}

}
