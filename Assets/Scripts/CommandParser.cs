using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandParser : MonoBehaviour
{
	private char[] charSeparators = { ' ' };

	public string ParseCommand(string command)
	{
		string[] result;
		result = command.Split(charSeparators);


		return "";
	}
}
