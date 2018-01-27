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
					//HandleSensorCommand(command);
					break;
			}
		}

		private void HandleWheelCommand(Command command)
		{
			if (command.commandValue == 0.0f)
			{
				WheelsForward();
			}
			else if (command.commandValue < 0.0f)
			{
				WheelsLeft();
			}
			else if (command.commandValue > 0.0f)
			{
				WheelsRight();
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
				Reverse();
			}
			else if (command.commandValue > 0.0f)
			{
				Accelerate();
			}
		}
	}
}
