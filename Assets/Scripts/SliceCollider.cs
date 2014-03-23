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
            audio.clip = killSounds[Random.Range(0, killSounds.Count)];
            audio.Play();
            if (other.gameObject.transform.parent.gameObject.GetComponent<PlayerController>().coins <= hitPenalty)
            {
                Destroy(other.gameObject.transform.parent.gameObject);
                other.gameObject.transform.parent.gameObject.GetComponent<PlayerController>().coins = 0;
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
