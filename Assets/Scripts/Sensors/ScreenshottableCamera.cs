using System;
using UnityEngine;

public class ScreenshottableCamera : Sensor
{
	private Camera currentCamera;
	private int frames = 0;
	private bool waiting = false;

	protected override void Start()
	{
		base.Start();

		currentCamera = GetComponent<Camera>();
		Debug.Assert(currentCamera != null, "ScreenshottableCamera must be on a Camera");

		currentCamera.enabled = false;
	}

	public override void ActivateSensor()
	{
		currentCamera.enabled = true;
		waiting = true;
	}

	private void LateUpdate()
	{
		if (waiting)
		{
			frames++;

			if (frames > 1)
			{
				ScreenshotData data = new ScreenshotData();
				data.renderTexture = new RenderTexture(currentCamera.activeTexture);
				data.renderTexture.Create();
				Graphics.CopyTexture(currentCamera.activeTexture, data.renderTexture);
				AddSensorData(data);

				currentCamera.enabled = false;
				frames = 0;
				waiting = false;
			}
		}
	}
}
