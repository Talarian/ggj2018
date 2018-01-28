using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
		private CarController carController; // the car controller we want to use

		public float carAcceleration = 0.0f;
		public float carWheelDirection = 0.0f;

		private SensorArray sensorArray;

		private void Awake()
        {
            // get the car controller
            carController = GetComponent<CarController>();

			sensorArray = FindObjectOfType<SensorArray>();
			if (sensorArray == null)
			{
				Debug.Log("Need a SensorArray");
			}
		}


        private void FixedUpdate()
        {
			carController.Move(carWheelDirection, carAcceleration, carAcceleration, 0f);
        }

		public void Accelerate(float speed)
		{
			carController.MaxSpeed = Mathf.Abs(speed);
			carAcceleration = 1.0f;
		}

		public void Stop()
		{
			carController.MaxSpeed = 0f;
			carAcceleration = 0.0f;
		}

		public void Reverse(float speed)
		{
			carController.MaxSpeed = Mathf.Abs(speed);
			carAcceleration = -1.0f;
		}

		public void WheelsLeft(float angle)
		{
			carController.MaximumSteerAngle = Mathf.Abs(angle);
			carWheelDirection = -1.0f;
		}

		public void WheelsRight(float angle)
		{
			carController.MaximumSteerAngle = Mathf.Abs(angle);
			carWheelDirection = 1.0f;
		}

		public void WheelsForward()
		{
			carController.MaximumSteerAngle = 0.0f;
			carWheelDirection = 0.0f;
		}

		public void AcceptCommand(Command command)
		{
			switch (command.commandType)
			{
				case Command.CommandType.Velocity:
					HandleVelocityCommand(command);
					break;
				case Command.CommandType.Wheels:
					HandleWheelCommand(command);
					break;
				case Command.CommandType.Sensors:
					HandleSensorCommand(command);
					break;
			}
		}

		private void HandleSensorCommand(Command command)
		{
			sensorArray.ActivateSensor(Mathf.RoundToInt(command.commandValue));
		}

		private void HandleWheelCommand(Command command)
		{
			if (command.commandValue == 0.0f)
			{
				WheelsForward();
			}
			else if (command.commandValue < 0.0f)
			{
				WheelsLeft(command.commandValue);
			}
			else if (command.commandValue > 0.0f)
			{
				WheelsRight(command.commandValue);
			}
		}

		private void HandleVelocityCommand(Command command)
		{
			if (command.commandValue == 0.0f)
			{
				Stop();
			}
			else if (command.commandValue < 0.0f)
			{
				Reverse(command.commandValue);
			}
			else if (command.commandValue > 0.0f)
			{
				Accelerate(command.commandValue);
			}
		}
	}
}
