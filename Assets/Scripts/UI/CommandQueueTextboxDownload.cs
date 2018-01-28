using System;

public class CommandQueueTextboxDownload : CommandQueueTextBox
{
	protected override void Start()
	{
		base.Start();

		commandQueue.OnCommandDequeued += CommandQueue_OnCommandDequeued;
	}

	private void CommandQueue_OnCommandDequeued(Command obj)
	{
		if (obj.commandType == Command.CommandType.Sensors)
		{
			Command commandCopy = new Command();
			commandCopy.commandType = obj.commandType;
			commandCopy.commandValue = obj.commandValue;
			commandCopy.originalText = obj.originalText;
			commandCopy.timeStamp = DateTime.Now;
			waitingCommands.Add(commandCopy);
		}
	}
}

