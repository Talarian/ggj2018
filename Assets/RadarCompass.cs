using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarCompass : MonoBehaviour {

    [System.Serializable]
    public class Configuration {
        public BasicRadar radar;
    }

    public Configuration configuration;
    public RectTransform arrow;

    private void Start() {
        Debug.Assert(arrow != null, "Heightmap requires a text mesh pro textbox");
        Debug.Assert(configuration.radar != null, "HeightmapDisplay requires a heightmap sensor");

        configuration.radar.OnSensorDataAvailable += Radar_OnSensorDataAvailable;

    }

    private void Radar_OnSensorDataAvailable(SensorData obj) {
        RadarData data = obj as RadarData;
        Debug.Assert(data != null);

        arrow.rotation = data.compassDirection;
    }
}
