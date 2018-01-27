using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraOrbitLock : MonoBehaviour {

    private Vector3 initialRotation = new Vector3(0,0,0);

	// Use this for initialization
	void Start ()
    {
        initialRotation = transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.rotation = Quaternion.Euler(initialRotation.x, transform.rotation.eulerAngles.y, initialRotation.y);
	}
}
