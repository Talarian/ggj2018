using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandParser : MonoBehaviour
{
	public event System.Action<string> OnCommandParseAttempt = delegate { };
	public event System.Action<string> OnCommandParseError = delegate { };

	private char[] charSeparators = { ' ' };

	private Queue<Command> waitingCommands = new Queue<Command>(4);

	// This clears our waiting command queue
	public Queue<Command> GetCommands()
	{
		Queue<Command> sendQueue = new Queue<Command>(waitingCommands);
		waitingCommands.Clear();
		return sendQueue;
	}

	public string ParseCommand(string command, bool silent = false)
	{
		if (!silent)
		{
			OnCommandParseAttempt(command);
		}

		List<string> result;
		result = new List<string>(command.Split(charSeparators));

		if (result.Count == 0)
		{
			OnCommandParseError(CreateErrorString(command));
			return CreateErrorString(command);
		}

		string errorCode = ParseArgument(result.GetEnumerator(), command);
		if (!string.IsNullOrEmpty(errorCode))
		{
			OnCommandParseError(CreateErrorString(command));
			return CreateErrorString(errorCode);
		}

		return "Command Transmitted: " + command;
	}

	private string ParseArgument(List<string>.Enumerator arguments, string originalString)
	{
		bool statusUpdate = false;

		if (!arguments.MoveNext())
		{
			return "Could not parse command: " + arguments.ToString();
		}

		string argument = arguments.Current;
		Command command = new Command();

		switch (argument)
		{
			case "fwd":
				command.commandType = Command.CommandType.Velocity;
				if (!ParseSpeed(arguments, command)) {
					return "could not parse argument for fwd";
				}
				statusUpdate = true;
				break;
			case "rev":
				command.commandType = Command.CommandType.Velocity;
				if (!ParseSpeed(arguments, command)) {
					return "could not parse argument for rev";
				}
				command.commandValue *= -1;
				statusUpdate = true;
				break;
			case "halt":
				command.commandType = Command.CommandType.Velocity;
				command.commandValue = 0.0f;
				statusUpdate = true;
				break;
			case "trn":
				if (!ParseTurn(arguments, command))
				{
					return "Could not parse arguments for trn.";
				}
				statusUpdate = true;
				break;
			case "img":
				command.commandType = Command.CommandType.Sensors;
				command.commandValue = 0.0f;
				break;
			case "hgtmp":
				command.commandType = Command.CommandType.Sensors;
				command.commandValue = 1.0f;
				break;
			case "rdr":
				command.commandType = Command.CommandType.Sensors;
				command.commandValue = 2.0f;
				break;
			case "imglow":
				command.commandType = Command.CommandType.Sensors;
				command.commandValue = 3.0f;
				break;
			case "sts":
				command.commandType = Command.CommandType.Sensors;
				command.commandValue = 4.0f;
				break;
			case "clr":
				CommandOutputTextbox textbox = FindObjectOfType<CommandOutputTextbox>();
				if (textbox != null)
				{
					textbox.Clear();
				}
				command.localOnly = true;
				break;
			case "shutdown":
			case "quit":
			case "shtdwn":
				ShutdownController controller = FindObjectOfType<ShutdownController>();
				if (controller != null)
				{
					controller.Shutdown();
				}
				command.localOnly = true;
				break;
			case "help":
				CommandOutputTextbox textbox2 = FindObjectOfType<CommandOutputTextbox>();
				if (textbox2 != null)
				{
					textbox2.AddLine(helpStrings[0]);
					textbox2.AddLine(helpStrings[1]);
					textbox2.AddLine(helpStrings[2]);
					textbox2.AddLine(helpStrings[3]);
					textbox2.AddLine(helpStrings[4]);
				}
				command.localOnly = true;
				break;
			case "help2":
				{
					CommandOutputTextbox textbox3 = FindObjectOfType<CommandOutputTextbox>();
					if (textbox3 != null)
					{
						textbox3.AddLine(helpStrings[5]);
						textbox3.AddLine(helpStrings[6]);
						textbox3.AddLine(helpStrings[7]);
						textbox3.AddLine(helpStrings[8]);
						textbox3.AddLine(helpStrings[9]);
						textbox3.AddLine(helpStrings[10]);
					}
					command.localOnly = true;
				}
				break;
			case "help3":
				{
					CommandOutputTextbox textbox3 = FindObjectOfType<CommandOutputTextbox>();
					if (textbox3 != null)
					{
						textbox3.AddLine(helpStrings[11]);
						textbox3.AddLine(helpStrings[12]);
					}
					command.localOnly = true;
				}
				break;
			case "!easy":
				{
					GameSettingsManager manager = FindObjectOfType<GameSettingsManager>();
					if (manager != null)
					{
						manager.SetEasy();
					}
					command.localOnly = true;
					break;
				}
			case "!normal":
				{
					GameSettingsManager manager = FindObjectOfType<GameSettingsManager>();
					if (manager != null)
					{
						manager.SetNormal();
					}
					command.localOnly = true;
					break;
				}
			case "!hard":
				{
					GameSettingsManager manager = FindObjectOfType<GameSettingsManager>();
					if (manager != null)
					{
						manager.SetMars();
					}
					command.localOnly = true;
					break;
				}
			default:
				return "Could not parse command: " + argument;
		}

		// We've gotten this far, we have a valid command
		command.timeStamp = DateTime.Now;
		command.originalText = originalString;
		waitingCommands.Enqueue(command);
		if (statusUpdate) {
			ParseCommand("sts", true);
		}

		return null; // No errorcode
	}

	private bool ParseSpeed(List<string>.Enumerator arguments, Command command) {
		if (!arguments.MoveNext()) {
			command.commandValue = 20.0f;
			return true;
		}

		try {
			float value = Int32.Parse(arguments.Current);
			command.commandValue = value;
		} catch (Exception) {
			return false;
		}

		return true;
	}

	private bool ParseTurn(List<string>.Enumerator arguments, Command command)
	{
		command.commandType = Command.CommandType.Wheels;
		if (!arguments.MoveNext())
		{
			return false;
		}
		string commandValue = arguments.Current;


		try {
			float value = Int32.Parse(commandValue);
            if (value < -60) {
                command.commandValue = -60;
            } else if (value > 60) {
                command.commandValue = 60;
            } else {
                command.commandValue = value;
            }
		} catch (Exception) {
			switch (commandValue) {
				case "lft":
					command.commandValue = -25.0f;
					break;
				case "rgt":
					command.commandValue = 25.0f;
					break;
				case "fwd":
					command.commandValue = 0.0f;
					break;
				default:
					return false;
			}
		}


		return true;
	}

	private string CreateErrorString(string command)
	{
		return "Invalid Command: " + command;
	}

	private string[] helpStrings = {
		"fwd [n]: Tells Rover to move forward.",
		"rev [n]: Tells Rover to move in reverse.",
		"halt: Halts the Rover.",
		"trn [lft|rgt|fwd] [n]: Turns the wheels.",
		"help2 for more.",
		"img: Instructs the Rover to take a picture.",
		"hgtmp: Gets a Satellite Height Map.",
		"rdr: Sends a radar ping from the Rover.",
		"imglow: A picture pointing down.",
		"sts: Requests a status update.",
		"help3 for more.",
		"clr: Clears the console.",
		"shtdwn: Quits the Rover application."
	};
}
