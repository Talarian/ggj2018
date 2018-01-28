using System;
using UnityEngine;

public class ScreenshottableCamera : Sensor
{
	private Camera currentCamera;

	protected override void Start()
	{
		base.Start();

		currentCamera = GetComponent<Camera>();
		Debug.Assert(currentCamera != null, "ScreenshottableCamera must be on a Camera");
	}

	public override void ActivateSensor()
	{
		ScreenshotData data = new ScreenshotData();
		data.renderTexture = new RenderTexture(currentCamera.activeTexture);
		data.renderTexture.Create();
		Graphics.CopyTexture(currentCamera.activeTexture, data.renderTexture);
		AddSensorData(data);
	}
}
