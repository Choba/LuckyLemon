using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KnifeController : MonoBehaviour {
	public float maxTimerLimit = 10;
	private float timerLimit;
	private float deltaTime;
	private List<GameObject> collidingFruits = new List<GameObject>();
	private bool bIsSlicing;
	private GameObject knife;
	private GameObject topLight;

	// Use this for initialization
	void Start () {
		knife = transform.Find ("Knife").gameObject;
		topLight = transform.Find ("TopLight").gameObject;
		knife.renderer.enabled = false;
		Reset();
		timerLimit = maxTimerLimit;
	}
	
	// Update is called once per frame
	void Update () {
		deltaTime += Time.deltaTime;

		if(deltaTime >= timerLimit + 5.0f) {
			Reset();
			knife.renderer.enabled = false;
			knife.collider.enabled = false;
		}
		if(deltaTime >= timerLimit + 2.0f && !bIsSlicing) {
			sliceFruits();
			knife.collider.isTrigger = false;
		}
		if(deltaTime >= timerLimit && !renderer.enabled) {
			knife.collider.enabled = true;
			knife.renderer.enabled = true;
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
		knife.collider.isTrigger = true;
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
