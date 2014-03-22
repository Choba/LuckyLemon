using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	public GameObject enemyPrefab;
	public float boundsX1,boundsX2,boundsY1,boundsY2;

	// Use this for initialization
	void Awake () {
		//SpawnEnemies (5);
	}

	void SpawnEnemies(int amount) {

		for (int i = 0; i < amount; i++) {
			Vector3 pos = new Vector3(Random.Range(boundsX1,boundsX2),0,Random.Range(boundsY1,boundsY2));

			Instantiate (enemyPrefab, pos, Quaternion.identity);
		}
	}
}
