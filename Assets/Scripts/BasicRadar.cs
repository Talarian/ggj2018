﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRadar : MonoBehaviour {

	
	// Update is called once per frame
	public void Ping () {
        GameObject[] transmitters = GameObject.FindGameObjectsWithTag("Transmitter");
        if (transmitters.Length < 0) {
            Debug.LogError("no transmitters to ping");
        }

        Vector3[] pings = new Vector3[transmitters.Length];
        for (int i = 0; i < transmitters.Length; i++) {
            pings[i] = transmitters[i].transform.position - transform.position;
        }
	}
}
