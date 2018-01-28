using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarCompass : MonoBehaviour {

    [System.Serializable]
    public class Configuration {
        public BasicRadar radar;
		public GameObject pingPrefab;
		public float radarRadius;
    }

    public Configuration configuration;
    public RectTransform arrow;

	private List<GameObject> pings = new List<GameObject>();

    private void Start() {
        Debug.Assert(arrow != null, "Heightmap requires a text mesh pro textbox");
        Debug.Assert(configuration.radar != null, "HeightmapDisplay requires a heightmap sensor");

        configuration.radar.OnSensorDataAvailable += Radar_OnSensorDataAvailable;

    }

    private void Radar_OnSensorDataAvailable(SensorData obj)
	{
		RadarData data = obj as RadarData;
		Debug.Assert(data != null);

		arrow.rotation = data.compassDirection;
		ClearPings();

		foreach (Vector3 ping in data.pings)
		{
			DisplayPing(ping);
		}
	}

	private void ClearPings()
	{
		while (pings.Count > 0)
		{
			DestroyImmediate(pings[0]);
			pings.RemoveAt(0);
		}
	}

	private void DisplayPing(Vector3 ping)
	{
		GameObject obj = Instantiate(configuration.pingPrefab);
		RectTransform rectTransform = obj.GetComponent<RectTransform>();
		rectTransform.SetParent(this.transform);
		pings.Add(obj);

		ping.y = 0f;
		ping = (ping / 50f) * configuration.radarRadius;

		// Convert to UI space
		ping.y = ping.z;
		ping.z = 0f;

		if (ping.magnitude > configuration.radarRadius)
		{
			ping.Normalize();
			ping = ping * configuration.radarRadius;
		}

		rectTransform.localPosition = ping;
	}
}
