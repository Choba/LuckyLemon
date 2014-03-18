using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {
	public float speed;

	void Update() {
		float moveHorizontal = Input.acceleration.x * 2 + Input.GetAxis ("Horizontal");
		float moveVertical = Input.acceleration.y * 2 + Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		transform.Rotate(moveVertical * speed * Time.deltaTime,0.0f,-moveHorizontal * speed * Time.deltaTime, Space.World);
	}
}
