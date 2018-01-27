using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController carController; // the car controller we want to use
		private float carAcceleration = 0.0f;
		private float carWheelDirection = 0.0f;

        private void Awake()
        {
            // get the car controller
            carController = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {
			carController.Move(carWheelDirection, carAcceleration, carAcceleration, 0f);
        }

		public void Accelerate()
		{
			carController.MaxSpeed = 20f;
			carAcceleration = 1.0f;
		}

		public void Stop()
		{
			carController.MaxSpeed = 0f;
			carAcceleration = 0.0f;
		}

		public void Reverse()
		{
			carController.MaxSpeed = 20f;
			carAcceleration = -1.0f;
		}

		public void WheelsLeft()
		{
			carController.MaximumSteerAngle = 25.0f;
			carWheelDirection = -1.0f;
		}

		public void WheelsRight()
		{
			carController.MaximumSteerAngle = 25.0f;
			carWheelDirection = 1.0f;
		}

		public void WheelsForward()
		{
			carController.MaximumSteerAngle = 0.0f;
			carWheelDirection = 0.0f;
		}
	}
}
