using System;
using UnityEngine;

public class ShutdownController : MonoBehaviour
{
	[System.Serializable]
	public class Configuration
	{
		public float timeToShutdown = 3.0f;
	}
	public Configuration configuration = new Configuration();

	private bool shuttingDown = false;
	private float timeSinceShutdownRequest = 0f;

	private void Update()
	{
		if (shuttingDown)
		{
			timeSinceShutdownRequest += Time.deltaTime;
		}

		if (timeSinceShutdownRequest >= configuration.timeToShutdown)
		{
			Debug.Log("Shutting Down");
			Application.Quit();
		}
	}

	public void Shutdown()
	{
		// Play Sound
		shuttingDown = true;
	}
}