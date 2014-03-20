using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float boostSpeed;
	public float moveSpeed;
	private bool bIsBoosting = false;
	public GUIText pointsText;
	public GUIText winText;

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
		float moveHorizontal = Input.acceleration.x * 2 + Input.GetAxis ("Horizontal");
		float moveVertical = Input.acceleration.y * 2 + Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		//rigidbody.AddForce(movement * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
		rigidbody.velocity = movement * (bIsBoosting ? boostSpeed : moveSpeed) * Time.deltaTime;

		if ((Input.GetKeyDown (KeyCode.Space) || Input.touchCount >= 1) && !bIsBoosting){
			//rigidbody.velocity = boostSpeed * rigidbody.velocity.normalized;
			bIsBoosting = true;
			boostTimer = 0; //starts the boost timer
		}
	}


	void OnCollisionEnter(Collision col) {

	}

	void OnDestroy() {
		renderer.enabled = false;
		winText.text = "You're lemonade! :(";
		restartTimer = 0;
	}

	public void updateHUD() {
		int fruits = GameObject.FindGameObjectsWithTag("Fruit").Length;
		pointsText.text = "Fruits: " + fruits.ToString();

		if(fruits == 0) {
			winText.text = "YOU WIN!!!";
			restartTimer = 0;
		}
	}

	private void StopBoost() {
		bIsBoosting = false;
	}

	private void RestartLevel() {
		Application.LoadLevel(Application.loadedLevel);
	}
}
