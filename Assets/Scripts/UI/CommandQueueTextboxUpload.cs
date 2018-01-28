
public class CommandQueueTextboxUpload : CommandQueueTextBox
{
	protected override void Start()
	{
		base.Start();

		commandQueue.OnCommandQueued += CommandQueue_OnCommandQueued;
	}

	private void CommandQueue_OnCommandQueued(Command obj)
	{
		if (!obj.localOnly)
		{
			waitingCommands.Add(obj);
		}
	}
}

