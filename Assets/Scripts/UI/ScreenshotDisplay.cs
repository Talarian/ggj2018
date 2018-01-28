using System;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotDisplay : MonoBehaviour
{
	[Serializable]
	public class Configuration
	{
		public ScreenshottableCamera camera;
	}
	public Configuration configuration = new Configuration();

	private RawImage image;

	private void Start()
	{
		image = GetComponent<RawImage>();
		Debug.Assert(image != null, "ScreenshotDisplay requires a RawImage", this);

		Debug.Assert(configuration.camera != null, "ScreenshotDisplay must have a camera assigned", this);
		configuration.camera.OnSensorDataAvailable += Camera_OnSensorDataAvailable;
	}

	private void Camera_OnSensorDataAvailable(SensorData obj)
	{
		ScreenshotData data = obj as ScreenshotData;
		Debug.Assert(data != null);

		RenderTexture oldTexture = image.texture as RenderTexture;
		data.renderTexture.Create();
		image.texture = data.renderTexture;

		if (oldTexture != null)
		{
			oldTexture.Release();
		}
	}
}
