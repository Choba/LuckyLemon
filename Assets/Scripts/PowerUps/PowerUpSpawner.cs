using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour {
	
	public float maxWaitTime = 10;
	float currentWaitTime = 0;
	public float checkTick = 0.5f;
	float nextTick = 0;
	float probability = 0;
	public float boundsX1, boundsX2, boundsY1, boundsY2;
	public float spawnOffset = 0.5f;
	
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
			int rand = (int)Random.Range(2, 4);
			int horver = (int)Random.Range(0, 2);
			Vector3 pos = new Vector3(Random.Range(boundsX1, boundsX2), .5f, Random.Range(boundsY1, boundsY2));
			for (int i = 0; i < rand; i++)
			{
				Vector3 posI = new Vector3();
				if (horver == 0)
					posI = new Vector3(pos.x, pos.y, pos.z + (spawnOffset * Mathf.Ceil((float)i / 2.0f) * (Mathf.Pow(-1, i))));
				else
					posI = new Vector3(pos.x + (spawnOffset * Mathf.Ceil((float)i / 2.0f) * (Mathf.Pow(-1, i))), pos.y, pos.z);
				spawnPowerUp(posI);
			}
			resetWaitTime();
			nextTick += checkTick;
		}
		else
		{
			currentWaitTime += Time.deltaTime;
		}
	}
	
	void spawnPowerUp(Vector3 pos)
	{
		Instantiate(powerUpPrefab, pos, Quaternion.identity);
	}
	
	void resetWaitTime()
	{
		currentWaitTime = 0;
		probability = 0;
		nextTick = 0;
	}
}

