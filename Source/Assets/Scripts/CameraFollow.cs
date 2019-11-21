using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField]
    GameObject objectToFollow;

    Camera cam;

    Vector3 currentVel = Vector3.zero;
	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
        if (cam == null)
            Debug.LogError("Camera is not ging to work.");

        if (objectToFollow == null)
            Debug.LogError("Camera will not follow anything as reference is not set.");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 target = new Vector3(objectToFollow.transform.position.x, objectToFollow.transform.position.y, cam.transform.position.z);
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, target, ref currentVel, 0.2f);
    }
}
