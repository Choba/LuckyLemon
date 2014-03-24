using UnityEngine;
using System.Collections;

public class PlayerCorpse : MonoBehaviour {

	// Use this for initialization
	public void Activate () {
		print ("enable corpse " + this.gameObject);
		gameObject.SetActive(true);
		foreach (Transform slice in transform) {	
			print ("rigidbody " + slice);	
			slice.gameObject.AddComponent<Rigidbody>();
		}
	}
}
