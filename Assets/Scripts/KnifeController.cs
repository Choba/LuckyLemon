using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KnifeController : MonoBehaviour {
	public float maxTimerLimit = 10;
	private float timerLimit;
	private float deltaTime;
	private List<GameObject> collidingFruits = new List<GameObject>();
	private bool bIsSlicing;

	// Use this for initialization
	void Start () {
		renderer.enabled = false;
		Reset();
		timerLimit = maxTimerLimit;
	}
	
	// Update is called once per frame
	void Update () {
		deltaTime += Time.deltaTime;

		if(deltaTime >= timerLimit + 3.0f) {
			Reset();
			renderer.enabled = false;
		}
		if(deltaTime >= timerLimit && !bIsSlicing) {
			renderer.enabled = true;
			sliceFruits();
			collider.isTrigger = false;
		}
	}

	private void sliceFruits() {
		bIsSlicing = true;
		foreach(GameObject fruit in collidingFruits) {
			Destroy(fruit);
		}
		PlayerController pc = (PlayerController) GameObject.Find("Player").GetComponent(typeof(PlayerController));
		pc.updateHUD();
	}

	private void SetRandomRotation() {
		transform.Rotate(0.0f, Random.Range(0.0f, 360.0f), 0.0f);
	}

	private void Reset() {
		timerLimit = Random.Range(0,maxTimerLimit);
		deltaTime = 0;
		collider.isTrigger = true;
		bIsSlicing = false;
		SetRandomRotation();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Fruit" || other.gameObject.tag == "Player") {
			collidingFruits.Add(other.gameObject);
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Fruit" || other.gameObject.tag == "Player") {
			collidingFruits.Remove(other.gameObject);
		}
	}
}
