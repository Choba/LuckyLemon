﻿using UnityEngine;
using System.Collections;

public class SliceCollider : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("destroy " + other.gameObject.tag);
		if (other.gameObject.tag == "Fruit" || other.gameObject.tag == "Player") {
            Destroy(other.gameObject);
		}
		PlayerController pc = (PlayerController) GameObject.Find("Player").GetComponent(typeof(PlayerController));
		pc.updateHUD();
	}
}
