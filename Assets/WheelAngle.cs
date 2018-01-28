using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WheelAngle : MonoBehaviour {

    [System.Serializable]
    public class Configuration {
        public InfoPacket info;
    }
    private RectTransform rectTransform;
    private Indicator ind;

    public Configuration configuration;

    private List<GameObject> pings = new List<GameObject>();

    private void Start() {

        
        rectTransform = GetComponent<RectTransform>();
        ind = GetComponent<Indicator>();

        Debug.Assert(rectTransform != null, "wheel info rectTransform");
        Debug.Assert(ind != null, "wheel info needs an indicator");
        Debug.Assert(configuration.info != null, "HeightmapDisplay requires a heightmap sensor");

        configuration.info.OnSensorDataAvailable += Info_OnSensorDataAvailable;

    }

    private void Info_OnSensorDataAvailable(SensorData obj) {
        InfoData data = obj as InfoData;
        Debug.Assert(data != null);

        rectTransform.rotation = Quaternion.Euler(0, 0, data.WheelAngle-180);
        ind.DoFlashing();
    }

}
