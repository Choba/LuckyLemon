using UnityEngine;
using System.Collections;

public class StompParticle : MonoBehaviour {

    float time;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time > 0.15f)
        {
            Destroy(gameObject);
        }
	}
}
