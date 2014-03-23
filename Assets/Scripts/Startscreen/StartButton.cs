using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnMouseEnter(){
        transform.parent.gameObject.GetComponent<Animator>().SetBool("HoverStart", true);
    }
    void OnMouseExit()
    {
        transform.parent.gameObject.GetComponent<Animator>().SetBool("HoverStart", false);
    }

    void OnMouseDown()
    {
        transform.parent.gameObject.GetComponent<Animator>().SetTrigger("StartPressed");
        GetComponent<AudioSource>().Play();
        Invoke("LoadMiniGame", 0.15f);
    }
    void LoadMiniGame()
    {
        Application.LoadLevel(1);
    }
}
