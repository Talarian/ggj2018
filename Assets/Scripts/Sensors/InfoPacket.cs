using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class InfoPacket : Sensor {
    public CarController controller;

    public override void ActivateSensor() {
        InfoData data = new InfoData();
        data.MaxSpeed = controller.MaxSpeed;
        data.WheelAngle = controller.MaximumSteerAngle;
        AddSensorData(data);
    }

    
	
}
