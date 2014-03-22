using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float moveSpeed;
	public GUIText pointsText;
	public GUIText winText;
    public enum Players { player1, player2 };
    public Players playerNum;
    public GameObject opponent;
    public bool invertControls;
    float invertControlsTimer;
    float doubleSpeedTimer;
    float increasedSizeTimer;

    public float stompRadius = 50;
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

        if (invertControlsTimer > 0)
        {
            invertControlsTimer -= Time.deltaTime;
        }
        else if (invertControlsTimer < 0)
        {
            DeInvertControls();
            invertControlsTimer = 0;
        }
        if (doubleSpeedTimer > 0)
        {
            doubleSpeedTimer -= Time.deltaTime;
        }
        else if (doubleSpeedTimer < 0)
        {
            halveSpeed();
            doubleSpeedTimer = 0;
        }
        if (increasedSizeTimer > 0)
        {
            increasedSizeTimer -= Time.deltaTime;
        }
        else if (increasedSizeTimer < 0)
        {
            decreaseSize();
            increasedSizeTimer = 0;
        }

        if (Input.GetButtonDown("Jump")) {
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
        if (invertControls)
        {
            moveHorizontal = -moveHorizontal;
            moveVertical = -moveVertical;
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
            PlayerController enemy = col.GetComponent<PlayerController>();
            Debug.Log("stomp hit " + enemy);
            if (enemy && col.rigidbody) {
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

                col.rigidbody.AddExplosionForce(stompPower, transform.position, stompRadius, 3.0F);
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

    public void doubleSpeed(float dur)
    {
        moveSpeed *= 2;
        doubleSpeedTimer = dur;
    }
    public void halveSpeed()
    {
        moveSpeed /= 2;
    }
    public void InvertControls(float dur)
    {
        invertControls = true;
        invertControlsTimer = dur;
        print(invertControls);
    }
    public void DeInvertControls()
    {
        invertControls = false;
    }

    public void increaseSize(float dur)
    {
        transform.localScale *= 1.5f;
        increasedSizeTimer = dur;
    }

    public void decreaseSize()
    {
        transform.localScale /= 1.5f;
    }
}
