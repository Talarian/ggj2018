using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeightmapDisplay : MonoBehaviour
{
    String[] colours = {"<color=#F37120>", "<color=#E27B37>", "<color=#D2854E>", "<color=#C28F66>", "<color=#B2997D>",
                        "<color=#A2A394>", "<color=#92ADAC>", "<color=#82B7C3>", "<color=#72C1DA>", "<color=#62CCF2>" };

    [Serializable]
	public class Configuration
	{
		public HeightMapRaycaster raycaster;
	}
	public Configuration configuration = new Configuration();
    private TMPro.TextMeshProUGUI text;

    private void Start()
	{
        text = GetComponent<TextMeshProUGUI>();
        Debug.Assert(text != null, "Heightmap requires a text mesh pro textbox");
        Debug.Assert(configuration.raycaster != null, "HeightmapDisplay requires a heightmap sensor");

        configuration.raycaster.OnSensorDataAvailable += Raycaster_OnSensorDataAvailable;
        
    }

    private void Raycaster_OnSensorDataAvailable(SensorData obj) {
        HeightMapData data = obj as HeightMapData;
        Debug.Assert(data != null);

        text.text = ToRichText(data.heightMap);
    }

    private string ToRichText(float[,] heightMap) {
        string richText = "";
        float tempFloat = 0.0f;
        int tempInt = 0;

        for(int i = 0; i < heightMap.GetLength(1); i++) {
            for(int j = 0; j < heightMap.GetLength(0); j++) {
                tempFloat = heightMap[j, i] * -1;
                tempFloat += 5;
                tempFloat *= 0.5f;
                tempInt = Mathf.RoundToInt(tempFloat);
                int middlew = heightMap.GetLength(0) / 2;
                int middleh = heightMap.GetLength(1) / 2;
                if (((j == middlew-1) || (j == middlew) || (j == middlew +1)) &&
                    ((i == middleh-1) || (i == middleh) || (i    == middleh +1))){
                    richText += "<color=\"white\">#";
                } else if ((tempInt < 0) || (tempInt > 9)) {
                    richText += "<color=\"black\">X";
                } else {
                    richText += colours[tempInt];
                    richText += tempInt.ToString();
                }
            }
            richText += "\n";
        }

        return richText;
    }
}
