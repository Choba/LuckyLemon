using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	public float rotateSpeed = 1.0f;
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3(15,30,45) * Time.deltaTime * rotateSpeed, Space.Self);
	}
}
