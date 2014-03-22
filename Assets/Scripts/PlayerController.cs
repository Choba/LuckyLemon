using UnityEngine;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;
    public float speedIncreasePerCoin = 0.01f;

	public GUIText pointsText;
	public GUIText winText;
    public enum Players { player1, player2 };
    public Players playerNum;
    public GameObject opponent;
    public int coins;

    public float stompRadius = .5f;
    public float stompPower = 1;

	private float restartTimer;

	void Start() {
		restartTimer = -1;
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

        if (Input.GetButtonDown("Stomp" + (playerNum == Players.player1 ? 1 : 2))) {
            Stomp();
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
		rigidbody.AddForce(movement * moveSpeed * Time.deltaTime);
        
	}

    void LateUpdate() {
        if (rigidbody.velocity.magnitude > 1)
        {
            transform.rotation = Quaternion.LookRotation(rigidbody.velocity, Vector3.up);
        }
    }

    void Stomp() {
        Debug.Log("stomp");
        Vector3 stompPos = transform.position;
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, stompRadius);

        foreach (Collider col in objectsInRange) {
            try {
                PlayerController enemy = col.transform.parent.GetComponent<PlayerController>();

                if (enemy && enemy.playerNum != playerNum)
                {
                    /*RaycastHit hit;
                    bool exposed = false;

                    if (Physics.Raycast (location, (col.transform.position - location), hit)) {
                        exposed = (hit.collider = enemyCollider);
                    }
 
                    if (exposed) {
                        // Damage Enemy! with a linear falloff of damage amount
                        var proximity : float = (location - enemy.transform.position).magnitude;
                        var effect : float = 1 - (proximity / radius);
                        enemy.ApplyDamage(damage * effect);
                    }*/

                    Debug.Log("stomp hit " + enemy + " at dist " + (col.transform.position - transform.position).magnitude);
                    enemy.rigidbody.AddExplosionForce(stompPower, transform.position, stompRadius, .2f);
                }
            }
            catch (NullReferenceException e) {
                /* empty */
            }
        }
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
