using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityUpdate : MonoBehaviour {

    [System.Serializable]
    public class Configuration {
        public InfoPacket info;
    }
    private DataObject dataObject;

    public Configuration configuration;



    private void Start() {


        dataObject = GetComponent<DataObject>();

        Debug.Assert(dataObject != null, "wheel info rectTransform");
        Debug.Assert(configuration.info != null, "HeightmapDisplay requires a heightmap sensor");

        configuration.info.OnSensorDataAvailable += Info_OnSensorDataAvailable;
    }

    private void Info_OnSensorDataAvailable(SensorData obj) {
        InfoData data = obj as InfoData;
        Debug.Assert(data != null);

        dataObject.ChangeValue(data.MaxSpeed.ToString() +"kph");
    }
}
