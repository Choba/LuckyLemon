using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public int powerUpType;
    public float doubleSpeedTime = 5;
    public float invertControlsTime = 2;
    public float doubleSizeTime = 5;

	// Use this for initialization
	void Start () {
        powerUpType = (int)Random.Range(0, 2);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            applyPowerUp(col.gameObject);
            Destroy(gameObject);
        }
    }

    void applyPowerUp(GameObject col)
    {
        if (powerUpType == 0)
        {
            col.GetComponent<PlayerController>().doubleSpeed(doubleSpeedTime);
        }
        else if (powerUpType == 1)
        {
            col.GetComponent<PlayerController>().opponent.GetComponent<PlayerController>().InvertControls(invertControlsTime);
        }
        else if (powerUpType == 2)
        {
            col.GetComponent<PlayerController>().opponent.GetComponent<PlayerController>().increaseSize(doubleSizeTime);
        }
    }
}
