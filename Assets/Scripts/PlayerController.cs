using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float boostSpeed;
	public float moveSpeed;
    public float speedIncreasePerCoin = 0.01f;
	private bool bIsBoosting = false;
	public GUIText pointsText;
	public GUIText winText;
    public enum Players { player1, player2 };
    public Players playerNum;
    public GameObject opponent;
    public int coins;

	private float restartTimer;
	private float boostTimer;

	void Start() {
		restartTimer = -1;
		boostTimer = -1;
		updateHUD();
		winText.text = "";
	}

	void Update() {
		if(restartTimer > -1) {
			restartTimer += Time.deltaTime;

			if(restartTimer > 2.0f) {
				RestartLevel();
			}
		}

		if(boostTimer > -1) {
			boostTimer += Time.deltaTime;
			
			if(boostTimer > 3.0f) {
				StopBoost();
				boostTimer = -1;
			}
		}
        
	}

	void FixedUpdate() {
		/*if ((Input.GetKeyDown (KeyCode.Space) || Input.touchCount >= 1) && bCanBoost){
			//rigidbody.velocity = boostSpeed * rigidbody.velocity.normalized;
			rigidbody.AddForce(rigidbody.velocity.normalized * boostSpeed, ForceMode.Impulse);
			bCanBoost = false;
			boostTimer = 0; //starts the boost timer
		}*/
		float moveHorizontal = 0;
        float moveVertical = 0;
        
        if (playerNum == Players.player1)
        {
		    moveHorizontal = Input.acceleration.x * 2 + Input.GetAxis ("Horizontal1");
		    moveVertical = Input.acceleration.y * 2 + Input.GetAxis ("Vertical1");
        }
        else
        {
		    moveHorizontal = Input.acceleration.x * 2 + Input.GetAxis ("Horizontal2");
		    moveVertical = Input.acceleration.y * 2 + Input.GetAxis ("Vertical2");
        }
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement * (bIsBoosting ? boostSpeed : moveSpeed) * Time.deltaTime;
        //Quaternion lookRot = Quaternion.LookRotation(rigidbody.velocity, Vector3.up);
        //Vector3 lookRot = transform.eulerAngles;
        //lookRot.y  = Vector3.Angle(rigidbody.velocity, Vector3.up);
        //Debug.Log("vel angle:" + Vector3.Angle(rigidbody.velocity, Vector3.up));
        //transform.eulerAngles = lookRot;
	}

	void OnCollisionEnter(Collision col) {

	}

	void OnDestroy() {
		renderer.enabled = false;
		winText.text = "You're lemonade! :(";
		restartTimer = 0;
	}

	public void updateHUD() {
		/*int fruits = GameObject.FindGameObjectsWithTag("Fruit").Length;
		pointsText.text = "Fruits: " + fruits.ToString();

		if(fruits == 0) {
			winText.text = "YOU WIN!!!";
			restartTimer = 0;
		}*/
	}

	private void StopBoost() {
		bIsBoosting = false;
	}

	private void RestartLevel() {
		Application.LoadLevel(Application.loadedLevel);
	}

    public void getCoins(int amount)
    {
        coins += amount;
        moveSpeed *= 1.0f + (speedIncreasePerCoin * amount);
    }
    public void loseCoins(int amount)
    {
        coins -= amount;
        moveSpeed /= 1.0f + (speedIncreasePerCoin * amount);
    }
}
