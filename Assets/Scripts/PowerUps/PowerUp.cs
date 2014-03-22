using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public float doubleSpeedTime = 5;
    public float invertControlsTime = 2;
    public float doubleSizeTime = 5;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            GameObject playerMesh = col.gameObject;
            GameObject player = playerMesh.transform.parent.gameObject;
            PlayerController controller = player.GetComponent<PlayerController>();
            controller.getCoins(1);
            Destroy(gameObject);
        }
    }
}
