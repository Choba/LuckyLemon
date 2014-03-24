using UnityEngine;
using System.Collections;

public class BlobShadowController : MonoBehaviour {
    public Vector3 offset;

    void Update() {
        transform.position = transform.parent.position + Vector3.up + offset;
        transform.rotation = Quaternion.LookRotation(-Vector3.up, transform.parent.forward);
    }
}