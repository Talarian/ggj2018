using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommandOutputTextbox : MonoBehaviour
{
	private List<string> lines = new List<string>();
	private TextMeshProUGUI textBox;
	private CommandParser commandParser;

	private void Start()
	{
		textBox = GetComponent<TextMeshProUGUI>();
		Debug.Assert(textBox != null);

		commandParser = FindObjectOfType<CommandParser>();
		Debug.Assert(commandParser != null);

		commandParser.OnCommandParseAttempt += AddValid;
		commandParser.OnCommandParseError += AddError;
	}

	public void AddValid(string line)
	{
		string newLine = string.Concat(">", line);
		AddLine(newLine);
	}

	public void AddError(string error)
	{
		string newLine = string.Concat("<color=red>", error);
		newLine = string.Concat(newLine, "</color>");
		AddLine(newLine);
	}

	public void AddLine(string line)
	{
		lines.Add(line);

		if (lines.Count > 12)
		{
			lines.RemoveAt(0);
		}

		UpdateText();
	}

	private void UpdateText()
	{
		string richString = "";
		foreach (string line in lines)
		{
			if (!string.IsNullOrEmpty(richString))
			{
				richString = string.Concat(richString, "\n");
			}

			richString = string.Concat(richString, line);
		}

		textBox.text = richString;
	}

	public void Clear()
	{
		lines.Clear();
		UpdateText();
	}
}