using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRadar : Sensor
{

	// Update is called once per frame
	public Vector3[] Ping () {
        GameObject[] transmitters = GameObject.FindGameObjectsWithTag("Transmitter");
        if (transmitters.Length < 0) {
            Debug.LogError("no transmitters to ping");
        }

        Vector3[] pings = new Vector3[transmitters.Length];
        for (int i = 0; i < transmitters.Length; i++) {
            pings[i] = transmitters[i].transform.position - transform.position;
        }

		return pings;
	}

	public override void ActivateSensor()
	{
		RadarData data = new RadarData();
		data.pings = Ping();
        data.compassDirection = getCompassDirection();
		AddSensorData(data);
	}

    private Quaternion getCompassDirection() {
        return Quaternion.Euler(0, 0, transform.rotation.eulerAngles.y * -1);
    }
}
