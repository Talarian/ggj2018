using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommandQueueTextBox : MonoBehaviour
{
	[Serializable]
	public class Configuration
	{
		public TextMeshProUGUI queuedCommandTextBox;
		public TextMeshProUGUI commandProgressTextBox;
	}
	public Configuration configuration = new Configuration();

	protected List<Command> waitingCommands = new List<Command>();
	protected CommandQueue commandQueue;

	protected virtual void Start()
	{
		commandQueue = FindObjectOfType<CommandQueue>();
		Debug.Assert(commandQueue != null);
	}

	private void Update()
	{
		ClearOldCommands();
		BuildCommandStrings();
	}

	private void ClearOldCommands()
	{
		DateTime now = DateTime.Now;

		while (waitingCommands.Count > 0)
		{
			var difference = now - waitingCommands[0].timeStamp;
			if (difference.TotalSeconds > commandQueue.configuration.commandDelayInSeconds + 2f)
			{
				waitingCommands.RemoveAt(0);
			}
			else
			{
				break;
			}
		}
	}

	private void BuildCommandStrings()
	{
		string commandsRichText = "";
		string progressRichText = "";

		foreach (Command command in waitingCommands)
		{
			AddCommandText(command, ref commandsRichText);
			AddProgressText(command, ref progressRichText);
		}

		configuration.queuedCommandTextBox.text = commandsRichText;
		configuration.commandProgressTextBox.text = progressRichText;
	}

	private void AddCommandText(Command command, ref string commandsRichText)
	{
		if (!string.IsNullOrEmpty(commandsRichText))
		{
			commandsRichText = string.Concat(commandsRichText, "\n");
		}

		commandsRichText = string.Concat(commandsRichText, command.originalText);
	}

	private void AddProgressText(Command command, ref string progressRichText)
	{
		if (!string.IsNullOrEmpty(progressRichText))
		{
			progressRichText = string.Concat(progressRichText, "\n");
		}

		// [oooooooooo]
		// [oooooo----]
		// [ COMPLETE ]
		DateTime now = DateTime.Now;
		var difference = now - command.timeStamp;
		float secondsDifference = (float)difference.TotalSeconds;
		float percentage = commandQueue.configuration.commandDelayInSeconds != 0.0f ? secondsDifference / commandQueue.configuration.commandDelayInSeconds : 1.0f;

		if (percentage >= 1.0f)
		{
			progressRichText = string.Concat(progressRichText, "[ COMPLETE ]");
		}
		else
		{
			int truncatedValue = (int)(percentage * 10);
			string progress = "[";
			int i = 0;
			for (; i < truncatedValue; i++ )
			{
				progress = string.Concat(progress, "o");
			}
			for (; i < 10; i++)
			{
				progress = string.Concat(progress, "-");
			}
			progress = string.Concat(progress, "]");

			progressRichText = string.Concat(progressRichText, progress);
		}
	}
}