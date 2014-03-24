using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SliceCollider : MonoBehaviour {

    public List<AudioClip> killSounds = new List<AudioClip>();
    public int hitPenalty;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

			ParticleSystem particles = other.transform.parent.GetComponentInChildren<ParticleSystem>();
			particles.transform.position = other.transform.position;
			particles.particleSystem.Play();

            audio.clip = killSounds[Random.Range(0, killSounds.Count)];
            audio.Play();
            if (other.gameObject.transform.parent.gameObject.GetComponent<PlayerController>().coins <= hitPenalty)
            {
				other.gameObject.transform.parent.gameObject.GetComponent<PlayerController>().Kill();
				//Destroy(other.gameObject);
            }
            else
            {
                other.gameObject.transform.parent.gameObject.GetComponent<PlayerController>().loseCoins(hitPenalty);
            }
        }
		//PlayerController pc = (PlayerController) GameObject.Find("Player").GetComponent(typeof(PlayerController));
		//pc.updateHUD();
	}
}
