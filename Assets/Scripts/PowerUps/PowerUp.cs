using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public float spinSpeed = 90;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + spinSpeed * Time.deltaTime, transform.eulerAngles.z);
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
