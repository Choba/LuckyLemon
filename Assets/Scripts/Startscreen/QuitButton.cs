using UnityEngine;
using System.Collections;

public class QuitButton : MonoBehaviour {

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseEnter()
    {
        transform.parent.gameObject.GetComponent<Animator>().SetBool("HoverQuit", true);
    }
    void OnMouseExit()
    {
        transform.parent.gameObject.GetComponent<Animator>().SetBool("HoverQuit", false);
    }

    void OnMouseDown()
    {
        transform.parent.gameObject.GetComponent<Animator>().SetTrigger("QuitPressed");
        GetComponent<AudioSource>().Play();
        Invoke("LoadMiniGame", 0.15f);
    }
    void LoadMiniGame()
    {
        Application.Quit();
    }
}
