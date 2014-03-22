using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SliceCollider : MonoBehaviour {

    public List<AudioClip> killSounds = new List<AudioClip>();
    public int hitPenalty;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audio.clip = killSounds[Random.Range(0, killSounds.Count)];
            audio.Play();
            if (other.gameObject.GetComponent<PlayerController>().coins <= hitPenalty)
            {
                Destroy(other.gameObject);
                other.gameObject.GetComponent<PlayerController>().coins = 0;
            }
            else
            {
                other.gameObject.GetComponent<PlayerController>().coins -= hitPenalty;
            }
        }
		//PlayerController pc = (PlayerController) GameObject.Find("Player").GetComponent(typeof(PlayerController));
		//pc.updateHUD();
	}
}
