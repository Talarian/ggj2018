using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sensor : MonoBehaviour
{
	public event System.Action<SensorData> OnSensorDataAvailable = delegate { };
	protected Queue<SensorData> waitingSensorData = new Queue<SensorData>();

	private CommandQueue commandQueue;

	protected virtual void Start()
	{
		commandQueue = FindObjectOfType<CommandQueue>();
		Debug.Assert(commandQueue != null, "Must be one CommandQueue in the scene");
	}

	private void Update()
	{
		while (waitingSensorData.Count > 0)
		{
			SensorData peekData = waitingSensorData.Peek();
			var timeDifference = DateTime.Now - peekData.timeStamp;
			if (timeDifference.TotalSeconds > commandQueue.configuration.commandDelayInSeconds)
			{
				SensorData actualData = waitingSensorData.Dequeue();
				OnSensorDataAvailable(actualData);
			}
			else
			{
				break;
			}
		}
	}

	public abstract void ActivateSensor();

	protected void AddSensorData(SensorData data)
	{
		data.timeStamp = DateTime.Now;
		waitingSensorData.Enqueue(data);
	}
}
