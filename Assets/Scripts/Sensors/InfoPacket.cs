using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class InfoPacket : Sensor {
    public CarController controller;
    public CarUserControl userController;

    public override void ActivateSensor() {
        InfoData data = new InfoData();
        data.MaxSpeed = controller.MaxSpeed * userController.carAcceleration;
        data.WheelAngle = controller.MaximumSteerAngle * userController.carWheelDirection;
        AddSensorData(data);
    }

    
	
}
