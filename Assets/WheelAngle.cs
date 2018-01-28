using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelAngle : MonoBehaviour {
    [Serializable]
    public class Configuration {
        public HeightMapRaycaster raycaster;
    }

    public Configuration configuration = new Configuration();
    private TMPro.TextMeshProUGUI text;

    private void Start() {
        text = GetComponent<TextMeshProUGUI>();
        Debug.Assert(text != null, "Heightmap requires a text mesh pro textbox");
        Debug.Assert(configuration.raycaster != null, "HeightmapDisplay requires a heightmap sensor");

        configuration.raycaster.OnSensorDataAvailable += Raycaster_OnSensorDataAvailable;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
