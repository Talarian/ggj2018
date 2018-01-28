using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class CommandQueue : MonoBehaviour
{
	public event System.Action<Command> OnCommandQueued = delegate { };
	public event System.Action<Command> OnCommandDequeued = delegate { };

	[Serializable]
	public class Configuration
	{
		public float commandDelayInSeconds = 5.0f;
	}
	public Configuration configuration = new Configuration();

	private CommandParser commandParser;
	private CarUserControl carUserControl;

	private Queue<Command> waitingCommands = new Queue<Command>(4);

	private void Start()
	{
		commandParser = FindObjectOfType<CommandParser>();
		if (commandParser == null)
		{
			Debug.LogError("CommandQueue requires a CommandParser in the scene.");
		}

		carUserControl = FindObjectOfType<CarUserControl>();
		if (carUserControl == null)
		{
			Debug.LogError("CommandQueue requires a CarUserControl in the scene.");
		}
	}

	private void Update()
	{
		GetNewCommands();
		ForwardCommands();
	}

	private void GetNewCommands()
	{
		Queue<Command> newCommands = commandParser.GetCommands();
		foreach (Command command in newCommands)
		{
			waitingCommands.Enqueue(command);
			OnCommandQueued(command);
		}
	}

	private void ForwardCommands()
	{
		while (waitingCommands.Count > 0)
		{
			Command peekCommand = waitingCommands.Peek();
			var timeDifference = DateTime.Now - peekCommand.timeStamp;
			if (timeDifference.TotalSeconds > configuration.commandDelayInSeconds)
			{
				Command command = waitingCommands.Dequeue();
				carUserControl.AcceptCommand(command);
				OnCommandDequeued(command);
			}
			else
			{
				break;
			}
		}
	}
}
