using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour {

    public Transform rover;
    RectTransform rectTransform;

    void Start() {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update () {
        rectTransform.rotation = Quaternion.Euler(0,0, rover.transform.rotation.eulerAngles.y);
	}
}
