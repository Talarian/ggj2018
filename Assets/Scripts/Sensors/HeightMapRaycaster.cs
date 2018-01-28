using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightMapRaycaster : Sensor {
    [System.Serializable]
    public class RadarSize {
        public int height;
        public int width;
        public float distance;
    }

    public RadarSize radar;

    // Update is called once per frame
    private float[,] Cast() {
        float[,] output = new float[radar.width, radar.height];

        // calculate top corner
        Vector3 topLeftCorner = transform.position;
        topLeftCorner += (50 * Vector3.up);
        topLeftCorner += (((radar.height / 2) * radar.distance) * Vector3.forward);
        topLeftCorner += (((radar.width / 2) * radar.distance) * Vector3.left);

        for (int h = 0; h < radar.height; h++) {
            for (int w = 0; w < radar.width; w++) {
                RaycastHit hit;
                Physics.Raycast(new Vector3(topLeftCorner.x + (w * radar.distance),
                                            topLeftCorner.y,
                                            topLeftCorner.z + (h * radar.distance)),
                                    Vector3.down,
                                    out hit);

                output[w, h] = hit.distance - 50;
            }
        }

		return output;
	}

	public override void ActivateSensor()
	{
		HeightMapData data = new HeightMapData();
		data.heightMap = Cast();
		AddSensorData(data);
	}
}
