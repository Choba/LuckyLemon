using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour {

    public float maxWaitTime = 10;
    float currentWaitTime = 0;
    public float checkTick = 0.5f;
    float nextTick = 0;
    float probability = 0;
    public float boundsX1, boundsX2, boundsY1, boundsY2;

    public GameObject powerUpPrefab;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        probability = currentWaitTime / maxWaitTime * 100;
        float random = Random.value * 100;
        if (random < probability && currentWaitTime > nextTick)
        {
            int rand = (int)Random.Range(1, 5);
            for (int i = 0; i < rand; i++)
            {
                spawnPowerUp();
            }
            resetWaitTime();
            nextTick += checkTick;
        }
        else
        {
            currentWaitTime += Time.deltaTime;
        }
	}

    void spawnPowerUp()
    {
        Vector3 pos = new Vector3(Random.Range(boundsX1, boundsX2), .5f, Random.Range(boundsY1, boundsY2));
        Instantiate(powerUpPrefab, pos, Quaternion.identity);
    }

    void resetWaitTime()
    {
        currentWaitTime = 0;
        probability = 0;
        nextTick = 0;
    }
}

