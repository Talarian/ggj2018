using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandParser : MonoBehaviour
{
	private char[] charSeparators = { ' ' };

	private Queue<Command> waitingCommands = new Queue<Command>(4);

	// This clears our waiting command queue
	public Queue<Command> GetCommands()
	{
		Queue<Command> sendQueue = new Queue<Command>(waitingCommands);
		waitingCommands.Clear();
		return sendQueue;
	}

	public string ParseCommand(string command)
	{
		List<string> result;
		result = new List<string>(command.Split(charSeparators));

		if (result.Count == 0)
		{
			return CreateErrorString(command);
		}

		string errorCode = ParseArgument(result.GetEnumerator());
		if (!string.IsNullOrEmpty(errorCode))
		{
			return CreateErrorString(errorCode);
		}

		return "Command Transmitted: " + command;
	}

	private string ParseArgument(List<string>.Enumerator arguments)
	{
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
				command.commandValue = 1.0f;
				break;
			case "rev":
				command.commandType = Command.CommandType.Velocity;
				command.commandValue = -1.0f;
				break;
			case "halt":
				command.commandType = Command.CommandType.Velocity;
				command.commandValue = 0.0f;
				break;
			case "trn":
				if (!ParseTurn(arguments, command))
				{
					return "Could not parse arguments for trn.";
				}
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
			default:
				return "Could not parse command: " + argument;
		}

		// We've gotten this far, we have a valid command
		command.timeStamp = DateTime.Now;
		waitingCommands.Enqueue(command);

		return null; // No errorcode
	}

	private bool ParseTurn(List<string>.Enumerator arguments, Command command)
	{
		command.commandType = Command.CommandType.Wheels;
		if (!arguments.MoveNext())
		{
			return false;
		}
		string commandValue = arguments.Current;

		switch (commandValue)
		{
			case "lft":
				command.commandValue = -1.0f;
				break;
			case "rgt":
				command.commandValue = 1.0f;
				break;
			case "fwd":
				command.commandValue = 0.0f;
				break;
			default:
				return false;
		}

		return true;
	}

	private string CreateErrorString(string command)
	{
		return "Invalid Command: " + command;
	}
}
