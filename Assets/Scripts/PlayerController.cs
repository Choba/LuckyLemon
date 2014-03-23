using UnityEngine;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float moveSpeed;
    public float speedIncreasePerCoin = 0.01f;
    public Animator meshAnimator;
    public Animator coinGuiAnimator;
    public int stompCost = 3;
    public GameObject stompAnimation;
    public bool noControls;
    float noControlsTimer;
    public Transform meshTransform;
    public Animator endscreenAnimator;

    public GUIText pointsText;
    public enum Players { player1 = 1, player2 = 2 };
    public Players playerNum;
    public GameObject opponent;
    public int coins;
	
	public float stompRadius = .5f;
    public float stompPower = 1;

	public bool lockRotationToVelocity;

    void Start() {
        updateHUD();
    }

	void Update() {
        if (Input.GetButtonDown("Stomp" + (int)playerNum)) {
			Stomp();
        }
        if (noControls)
        {
            noControlsTimer -= Time.deltaTime;
            if (noControlsTimer < 0)
                noControls = false;
        }
	}
    
    void FixedUpdate() {
        if (noControls) return;
		float moveHorizontal = 0;
        float moveVertical = 0;

        moveHorizontal = Input.acceleration.x * 2 + Input.GetAxis("Horizontal" + (int)playerNum);
        moveVertical = Input.acceleration.y * 2 + Input.GetAxis("Vertical" + (int)playerNum);

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        //rigidbody.AddForce(movement * moveSpeed * Time.deltaTime);
        GetComponentInChildren<Rigidbody>().velocity = movement * moveSpeed *Time.deltaTime;

        //meshAnimator.SetFloat("Velocity", Vector3.Magnitude(rigidbody.velocity));
	}

    void LateUpdate() {
		if (lockRotationToVelocity && GetComponentInChildren<Rigidbody>().velocity.magnitude > 1) {
			transform.rotation = Quaternion.LookRotation(GetComponentInChildren<Rigidbody>().velocity, Vector3.up);
        }
    }
	
	void Stomp() {
        if (coins < stompCost)
        {
            return;
        }

        loseCoins(stompCost);
        Vector3 pos = new Vector3(meshTransform.position.x, meshTransform.position.y + 0.5f, meshTransform.position.z);
        GameObject stomp = (GameObject) Instantiate(stompAnimation);
        stomp.transform.position = pos;

        Collider[] objectsInRange = Physics.OverlapSphere(meshTransform.position, stompRadius);

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

                    //Debug.Log("stomp hit " + enemy + " at dist " + (col.transform.position - transform.position).magnitude);

                    enemy.GetComponentInChildren<Rigidbody>().AddExplosionForce(stompPower, meshTransform.position, stompRadius, .2f);
                    enemy.GetComponent<PlayerController>().noControls = true;
                    enemy.GetComponent<PlayerController>().noControlsTimer = 0.5f;
                }
            } catch (NullReferenceException e) {
                
            }
        }
    }

    void OnDestroy() {
        renderer.enabled = false;
        print("player " + playerNum + " destroyed");
        if (playerNum == Players.player1)
        {
            endscreenAnimator.SetTrigger("OrangeWins");
        }
        else
        {
            endscreenAnimator.SetTrigger("LemonWins");
        }
        GameManager.Instance.EndGame((int)playerNum % 2 + 1);
    }

	public void updateHUD() {

    }

    public void getCoins(int amount) {
        if (coins + amount > 99)
        {
            amount -= (coins + amount - 99);
            coins = 99;
        }
        else
        {
            coins += amount;
        }
        coinGuiAnimator.SetTrigger("Animate");
        moveSpeed *= 1.0f + (speedIncreasePerCoin * amount);
    }

    public void loseCoins(int amount) {
        coins -= amount;
        moveSpeed /= 1.0f + (speedIncreasePerCoin * amount);
    }
}
