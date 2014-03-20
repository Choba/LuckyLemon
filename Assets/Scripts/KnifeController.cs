﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KnifeController : MonoBehaviour {
	public float maxTimerLimit = 10;
	private float timerLimit;
	private float deltaTime;
	private List<GameObject> collidingFruits = new List<GameObject>();
	private GameObject knife;
	private GameObject knifeShadow;
    private float chopSpeed = 20;
    private float liftSpeed = 5;
    private Vector3 acceleration;
    private enum State { Idle, Imminent, Chopping, OnBoard, Lifting };
    private State state;

	// Use this for initialization
	void Start () {
		knife = transform.Find ("Knife").gameObject;
		knifeShadow = transform.Find ("KnifeShadow").gameObject;
		knife.renderer.enabled = false;
        Reset();
        Lift();
		timerLimit = maxTimerLimit;
	}
	
	// Update is called once per frame
	void Update () {
		deltaTime += Time.deltaTime;

        switch (state)
        {
            case State.Idle:
                if (deltaTime >= timerLimit) {
                    state = State.Imminent;
                    knifeShadow.SetActive(true);
                }
                break;
            case State.Imminent:
                if (deltaTime >= timerLimit + 2.0f) {
                    knife.renderer.enabled = true;
                    state = State.Chopping;
                }
                break;
            case State.Chopping:
                Chop();

                if (transform.position.y <= 0) {
                    state = State.OnBoard;
                }
                break;
            case State.OnBoard:
                acceleration = Vector3.zero;
                if (deltaTime >= timerLimit + 3.0f) {
                    state = State.Lifting;
                }
                break;
            case State.Lifting:
                Lift();

                if (transform.position.y >= 4) {
                    Reset();
                }
                break;
            default:
                break;
        }
	}
	
	private void Lift() {
        Debug.Log("lift");
<<<<<<< HEAD
        acceleration += Vector3.up * liftSpeed * Time.deltaTime;
		//Vector3 target = transform.position;
		//target.y = 4;
		//transform.position = Vector3.Lerp(transform.position, target, step);
        transform.Translate(acceleration);
=======
		float step = chopSpeed * Time.deltaTime;
		Vector3 target = transform.position;
		target.y = 6;
		transform.position = Vector3.MoveTowards(transform.position, target, step);
>>>>>>> bc15415d6ae9e6635185f6e7f224b3050deb2c89
	}

    private void Chop() {
        Debug.Log("chop");
<<<<<<< HEAD
		//float step = chopSpeed * Time.deltaTime;
		//Vector3 target = transform.position;
		//target.y = 0;
        //transform.position = Vector3.Lerp(transform.position, target, step);
        acceleration += Vector3.down * chopSpeed * Time.deltaTime;
        transform.Translate(acceleration);
=======
		float step = liftSpeed * Time.deltaTime;
		Vector3 target = transform.position;
		target.y = 1.7;
		transform.position = Vector3.MoveTowards(transform.position, target, step);
>>>>>>> bc15415d6ae9e6635185f6e7f224b3050deb2c89
	}

	private void SetRandomRotation() {
		transform.Rotate(0.0f, Random.Range(0.0f, 360.0f), 0.0f);
	}

	private void Reset() {
        Debug.Log("reset");
        state = State.Idle;
        deltaTime = 0;
		timerLimit = Random.Range(0,maxTimerLimit);
		knife.renderer.enabled = false;
		knifeShadow.SetActive(false);
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
