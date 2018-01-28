using System;
using UnityEngine;

public class ShutdownController : MonoBehaviour
{
	[System.Serializable]
	public class Configuration
	{
        public GameObject monitorUI;
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
            UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
		}
	}

	public void Shutdown()
	{
        if (AudioAmbient.instance != null)
        {
            AudioAmbient.instance.PlayPCShutdown();
            configuration.timeToShutdown = AudioAmbient.instance.shutdownSound.length;
        }

        if (configuration.monitorUI)
        {
            configuration.monitorUI.SetActive(false);
        }
        shuttingDown = true;
	}
}