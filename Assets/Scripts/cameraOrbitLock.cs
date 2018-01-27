using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraOrbitLock : MonoBehaviour {

    private Quaternion initialRotation = Quaternion.identity;
    public bool lockX = false;
    public bool lockY = false;
    public bool lockZ = false;

	// Use this for initialization
	void Start ()
    {
        initialRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float x = lockX == true ? initialRotation.eulerAngles.x : transform.rotation.eulerAngles.x;
        float y = lockY == true ? initialRotation.eulerAngles.y : transform.rotation.eulerAngles.y;
        float z = lockY == true ? initialRotation.eulerAngles.z : transform.rotation.eulerAngles.z;
	}
}
