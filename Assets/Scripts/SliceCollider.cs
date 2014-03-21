using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SliceCollider : MonoBehaviour {

    public List<AudioClip> killSounds = new List<AudioClip>();

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("destroy " + other.gameObject.tag);
		if (other.gameObject.tag == "Fruit" || other.gameObject.tag == "Player") {
            audio.clip = killSounds[Random.Range(0, killSounds.Count)];
            audio.Play();
            Destroy(other.gameObject);
		}
		PlayerController pc = (PlayerController) GameObject.Find("Player").GetComponent(typeof(PlayerController));
		pc.updateHUD();
	}
}
