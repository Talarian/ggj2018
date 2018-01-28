using System;
using System.Collections.Generic;
using UnityEngine;

public class SampleCollectorReceiver : MonoBehaviour
{
	[Serializable]
	public class Configuration
	{
		public float distanceToGrab = 10f;
		public Indicator uiElement;
	}
	public Configuration configuration = new Configuration();

	SampleCollector sensor;
	List<GameObject> transmitters;

	public bool HasCollected = false;

	private void Start()
	{
		sensor = FindObjectOfType<SampleCollector>();
		sensor.OnSensorDataAvailable += OnSensorDataAvailable;
		transmitters = new List<GameObject>(GameObject.FindGameObjectsWithTag("Transmitter"));
	}

	private void OnSensorDataAvailable(SensorData obj)
	{
		SampleData data = obj as SampleData;
		Debug.Assert(data != null);

		foreach (GameObject transmitter in transmitters)
		{
			if (Vector3.Distance(transmitter.transform.position, data.location) < configuration.distanceToGrab)
			{
				if (transmitter.gameObject.name == "Astronaut" && HasCollected)
				{
					// Win condition
					WinController controller = GameObject.FindObjectOfType<WinController>();
					if (controller != null)
					{
						controller.Win();
					}
				}
				else
				{
					configuration.uiElement.DoFlashing();
					transmitter.SetActive(false);
					HasCollected = true;
				}
			}
		}
	}
}