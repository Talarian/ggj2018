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
	private bool rebooting = false;
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
			if (rebooting)
			{
				UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
			}
			else
			{
				Application.Quit();
			}
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

	public void Reboot()
	{
		rebooting = true;
		Shutdown();
	}
}