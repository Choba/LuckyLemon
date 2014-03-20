using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public float velocity = .1f;

	// Use this for initialization
	void Start () {
		Vector3 impulse = Time.deltaTime * velocity * new Vector3(Random.Range(-100,100),0,Random.Range(-100,100));
		Debug.Log("start impulse " + impulse);
		//rigidbody.AddForce(impulse,ForceMode.Impulse);
		rigidbody.velocity = velocity * new Vector3(Random.Range(-100,100),0,Random.Range(-100,100));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (rigidbody.velocity.magnitude < velocity) {
			rigidbody.velocity = rigidbody.velocity.normalized * velocity;
		}
	}
}
