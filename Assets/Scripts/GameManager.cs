using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private static GameManager _instance;

    private int winningPlayerId = -1;
    private float restartTimer;

	private bool gameIsRunning;

	void Awake () {
		DontDestroyOnLoad (_instance);
	}

	// Use this for initialization
    void Start() {
		StartGame ();
	}
	
	// Update is called once per frame
	void Update () {
        if (restartTimer > -1) {
			restartTimer += Time.deltaTime;
			print("wait for restart...");

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

    public void EndGame(int winningPlayer)
    {
        winningPlayerId = winningPlayer;
        restartTimer = 0;
		KnifeController knife = GameObject.FindObjectOfType (typeof(KnifeController)) as KnifeController;
		knife.SetEnabled (false);
		gameIsRunning = false;
		print("Player " + winningPlayerId + " survived!\nPlayer " + (winningPlayerId % 2 + 1) + ", you're lemonade :(");
	}

    private void RestartLevel() {
        print("restart");
        Application.LoadLevel(Application.loadedLevel);
		StartGame ();
    }

	public static GameManager Instance
	{
		get
		{
			if (!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
				if (!_instance)
				{
					_instance = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
					if (!_instance) {
						Debug.LogError("There needs to be one active GameManager script on a GameObject in your scene.");
					}
				}
			}
			
			return _instance;
		}
	}

}
