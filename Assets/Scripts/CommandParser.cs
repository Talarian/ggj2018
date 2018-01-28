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

	public string ParseCommand(string command)
	{
		OnCommandParseAttempt(command);

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
				if(!ParseSpeed(arguments, command)) {
                    return "could not parse argument for fwd";
                }
                statusUpdate = true;
				break;
			case "rev":
				command.commandType = Command.CommandType.Velocity;
				if(!ParseSpeed(arguments, command)) {
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
				break;
			case "shutdown":
			case "quit":
			case "shtdwn":
				ShutdownController controller = FindObjectOfType<ShutdownController>();
				if (controller != null)
				{
					controller.Shutdown();
				}
				break;
			case "help":
				CommandOutputTextbox textbox2 = FindObjectOfType<CommandOutputTextbox>();
				if (textbox2 != null)
				{
					textbox2.AddValid(helpString);
				}
				break;
			default:
				return "Could not parse command: " + argument;
		}

		// We've gotten this far, we have a valid command
		command.timeStamp = DateTime.Now;
		command.originalText = originalString;
		waitingCommands.Enqueue(command);
        if (statusUpdate) {
            ParseCommand("sts");
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
            command.commandValue = value;
        } catch(Exception) {
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

	private const string helpString = "Commands:\n" +
		"fwd [n]: Tells Rover to move forward, n is optional speed.\n" +
		"rev [n]: Tells Rover to move in reverse, n is optional speed.\n" +
		"halt: Halts the Rover.\n" +
		"trn [lft|rgt|fwd] [n]: Turns the Rover's wheels in the direction noted, n is optional degrees.\n" +
		"img: Instructs the Rover to take a forward camera picture.\n" +
		"hgtmp: Gets a Satellite Height Map around the Rover.\n" +
		"rdr: Sends a radar ping from the Rover.\n" +
		"imglow: Instructs the Rover to take a downward's camera picture.\n" +
		"sts: Requests a status update from the Rover.\n" +
		"clr: Clears the console.\n" +
		"shtdwn: Quits the Rover application.\n";
}
