using System;
using System.Collections.Generic;
using UnityEngine;

public class SensorArray : MonoBehaviour
{
	[Serializable]
	public class Configuration
	{
		public List<Sensor> sensors = new List<Sensor>();
	}
	public Configuration configuration = new Configuration();

	public void ActivateSensor(int sensor)
	{
		if (configuration.sensors.Count <= sensor)
		{
			Debug.LogError("Invalid sensor index " + sensor);
			return;
		}

		configuration.sensors[sensor].ActivateSensor();
	}
}
