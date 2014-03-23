using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private static GameManager _instance;

    private int winningPlayerId = -1;
    private float restartTimer;

	private bool gameIsRunning;

	void Awake() {
		if (GameManager.Instance != null && GameManager.Instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			_instance = this;
			PlayerPrefs.DeleteAll ();
		}
		DontDestroyOnLoad(this.gameObject);
		DontDestroyOnLoad(this.gameObject.transform.parent);
	}

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
		gameIsRunning = true;
	}

	void OnLevelWasLoaded(int level) {
		StartGame ();
	}

    public void EndGame(int winningPlayer)
    {		
		if (gameIsRunning) {
			winningPlayerId = winningPlayer;
			GrantPoints ();

			restartTimer = 0;
			KnifeController knife = GameObject.FindObjectOfType (typeof(KnifeController)) as KnifeController;
			knife.SetEnabled (false);
			print ("Player " + winningPlayerId + " survived!\nPlayer " + (winningPlayerId % 2 + 1) + ", you're lemonade :(");
			gameIsRunning = false;
		}
	}

	private void GrantPoints() {
		string key = "Player" + winningPlayerId + "Score";
		PlayerPrefs.SetInt (key, PlayerPrefs.GetInt (key) + 1);
	}

	private void RestartLevel() {
		Application.LoadLevel(Application.loadedLevel);
    }

	public bool GameIsStarted() {
		return gameIsRunning;
	}

	public static GameManager Instance {
		get {
			return _instance;
		}
	}

}
