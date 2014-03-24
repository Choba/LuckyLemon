﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KnifeController : MonoBehaviour {
	public float maxTimerLimit = 1;
    float timeScaling = 1;
    float cutPatternTimeScaling = 10;
    public AudioSource kidVoice;
    public AudioClip[] voices;
	private float timerLimit;
	private float deltaTime;
	private List<GameObject> collidingFruits = new List<GameObject>();
	private GameObject knife;
    private GameObject knifeShadow;
    private GameObject knifeCutCollider;

    public List<AudioClip> swingSounds = new List<AudioClip>();
    public List<AudioClip> missSounds = new List<AudioClip>();

    private float chopSpeed = 40;
    private float liftSpeed = 10;
    private Vector3 acceleration;
    public enum State { Idle, Imminent, Chopping, OnBoard, Lifting };
    private State state;

    public List<Vector3> knifePositions = new List<Vector3>();
    public List<Vector3> knifeRotations = new List<Vector3>();
    public List<ListWrapper> series = new List<ListWrapper>();
    private int positionIndex = -1;
    private int seriesIndex;

    public float speedIncrease = 0.01f;

	private float endTimer = -1f;

    private float topY;

	// Use this for initialization
	void Start () {
        topY = transform.position.y;
        seriesIndex = Random.Range(0, series.Count);
		knife = transform.Find ("Knife").gameObject;
        knifeShadow = transform.Find("KnifeShadow").gameObject;
        knifeCutCollider = transform.Find("KnifeCut").gameObject;
		knife.renderer.enabled = false;
        Reset();
        Lift();
		timerLimit = maxTimerLimit;
        DontDestroyOnLoad(kidVoice);
	}
	
	// Update is called once per frame
	void Update () {
		deltaTime += Time.deltaTime;
        maxTimerLimit *= (1 - speedIncrease * Time.deltaTime);
        timeScaling *= (1 - speedIncrease * Time.deltaTime);
        chopSpeed *= (1 + speedIncrease * Time.deltaTime);
        liftSpeed *= (1 + speedIncrease * Time.deltaTime);

		if (endTimer > -1) {
			endTimer += Time.deltaTime;

			if(endTimer > 0.5f) {
				active = false;
			}
		}

        switch (state)
        {
            case State.Idle:
                if (deltaTime >= timerLimit) {
                    state = State.Imminent;
                    
                    knifeShadow.SetActive(true);
                }
                break;
            case State.Imminent:
                if (deltaTime >= timerLimit + 2.0f * timeScaling / cutPatternTimeScaling)
                {
                    if (positionIndex == 0)
                    {
                        cutPatternTimeScaling = 10;
                    }
                    knife.renderer.enabled = true;
                    state = State.Chopping;
                    audio.clip = swingSounds[Random.Range(0, swingSounds.Count)];
                    audio.Play();
                }
                break;
            case State.Chopping:
                //knifeCutCollider.collider.enabled = true;
                Chop();

                if (transform.position.y <= .07f) {
                    audio.clip = missSounds[Random.Range(0, missSounds.Count)];
                    audio.Play();
                    state = State.OnBoard;
                }
                break;
            case State.OnBoard:
                //knifeCutCollider.collider.enabled = false;
                acceleration = Vector3.zero;
                Vector3 pos = transform.position;
                pos.y = .07f;

                transform.position = pos;
                if (deltaTime >= timerLimit + 3.0f * timeScaling / cutPatternTimeScaling)
                {
                    state = State.Lifting;
                }
                break;
            case State.Lifting:
                Lift();

                if (transform.position.y >= topY)
                {
                    Reset();
                }
                break;
            default:
                break;
        }
	}

	public void End() {
		state = State.Lifting;
		endTimer = 0;
	}

	public void GotoState(State newState) {
		state = newState;
	}
	
	private void Lift() {
        acceleration += Vector3.up * (liftSpeed) * Time.deltaTime;
        transform.Translate(acceleration);
	}

    private void Chop() {
        acceleration += Vector3.down * (chopSpeed) * Time.deltaTime;
        transform.Translate(acceleration);
	}

	private void GotoNextPosition() {
		//transform.Rotate(0.0f, Random.Range(0.0f, 360.0f), 0.0f);
        if (positionIndex < series[seriesIndex].myList.Count - 1)
        {
            positionIndex++;
        }
        else
        {
            float random = Random.value;
            if (random < 0.66f)
            {
                int idx = Random.Range(0, voices.Length);
                kidVoice.clip = voices[idx];
                kidVoice.Play();
            }
            seriesIndex = Random.Range(0, series.Count);
            positionIndex = 0;
        }
        ListWrapper lw = series[seriesIndex];
        int i = lw.myList[positionIndex];

        transform.position = knifePositions[i];
        transform.eulerAngles = knifeRotations[i];

        
	}

	private void Reset() {
        state = State.Idle;
        deltaTime = 0;
        
		knife.renderer.enabled = false;
		knifeShadow.SetActive(false);
		GotoNextPosition();
        if (positionIndex == 0)
        {
            cutPatternTimeScaling = 1;
        }
        timerLimit = maxTimerLimit / cutPatternTimeScaling;
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

	public void SetEnabled(bool b) {
		enabled = b;

		if (!b) {
			Reset ();
		}
	}
}
