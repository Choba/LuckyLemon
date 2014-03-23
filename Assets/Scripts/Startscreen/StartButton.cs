using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

    bool showFlare;
    float fadeTime;
    public float fadingTime = 0.5f;
    public SpriteRenderer flare, flare2;

	// Use this for initialization
	void Start () {
        fadeTime = fadingTime;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (fadeTime < fadingTime)
        {
            fadeTime += Time.deltaTime;
            if (showFlare)
            {
                Color color = flare.color;
                color.a = fadeTime / fadingTime;
                flare.color = color;
                flare2.color = color;
            }
            else
            {
                Color color = flare.color;
                color.a = 1 - fadeTime / fadingTime;
                flare.color = color;
                flare2.color = color;
            }
        }
	}

    void OnMouseEnter()
    {
        showFlare = true;
        fadeTime = 0;
    }
    void OnMouseExit()
    {
        showFlare = false;
        fadeTime = 0;
    }

    void OnMouseDown()
    {
        GetComponent<Animator>().SetTrigger("Mouse Down");
        GetComponent<AudioSource>().Play();
        Invoke("LoadMiniGame", 0.2f);
    }
    void LoadMiniGame()
    {
        Application.LoadLevel(1);
    }
}
