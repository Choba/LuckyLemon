using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public float velocity = .1f;

	// Use this for initialization
	void Start () {
		Vector3 impulse = Time.deltaTime * velocity * new Vector3(Random.Range(-100,100),0,Random.Range(-100,100));
		Debug.Log("start impulse " + impulse);
		rigidbody.AddForce(impulse,ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 vel = rigidbody.velocity;
		vel.z = 0;
		rigidbody.velocity = vel;
	}
}
