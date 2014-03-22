using UnityEngine;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float moveSpeed;
    public float speedIncreasePerCoin = 0.01f;

    public GUIText pointsText;
    public enum Players { player1 = 1, player2 = 2 };
    public Players playerNum;
    public GameObject opponent;
    public int coins;
	
	public float stompRadius = .5f;
    public float stompPower = 1;

    void Start() {
        updateHUD();
    }

	void Update() {
        if (Input.GetButtonDown("Stomp" + (int)playerNum)) {
			Stomp();
        }
	}
    
    void FixedUpdate() {
		float moveHorizontal = 0;
        float moveVertical = 0;

        moveHorizontal = Input.acceleration.x * 2 + Input.GetAxis("Horizontal" + (int)playerNum);
        moveVertical = Input.acceleration.y * 2 + Input.GetAxis("Vertical" + (int)playerNum);

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidbody.AddForce(movement * moveSpeed * Time.deltaTime);
	}

    void LateUpdate() {
        if (rigidbody.velocity.magnitude > 1) {
            transform.rotation = Quaternion.LookRotation(rigidbody.velocity, Vector3.up);
        }
    }
	
	void Stomp() {
        Debug.Log("stomp");
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, stompRadius);

        foreach (Collider col in objectsInRange) {
            try {
                PlayerController enemy = col.transform.parent.GetComponent<PlayerController>();

                if (enemy && enemy.playerNum != playerNum) {
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
            } catch (NullReferenceException e) {
                /* empty */
            }
        }
    }

    void OnDestroy() {
        renderer.enabled = false;
        print("player " + playerNum + " destroyed");
        GameManager.EndGame((int)playerNum % 2 + 1);
    }

	public void updateHUD() {

    }

    public void getCoins(int amount) {
		coins += amount;
        moveSpeed *= 1.0f + (speedIncreasePerCoin * amount);
    }

    public void loseCoins(int amount) {
        coins -= amount;
        moveSpeed /= 1.0f + (speedIncreasePerCoin * amount);
    }
}
