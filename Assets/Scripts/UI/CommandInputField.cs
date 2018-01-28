using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandInputField : MonoBehaviour
{
	private InputField inputField;
	private CommandParser commandParser;

	private void Start()
	{
		inputField = GetComponent<InputField>();
		if (inputField == null)
		{
			Debug.LogError("CommandInputField needs to be on the same objects as an InputField");
		}

		inputField.onEndEdit.AddListener(OnEndEdit);

		commandParser = FindObjectOfType<CommandParser>();
		if (commandParser == null)
		{
			Debug.LogError("Scene must have a CommandParser");
		}
	}

	private void OnEndEdit(string arg0)
	{
		if (!string.IsNullOrEmpty(arg0))
		{
			string echoString = commandParser.ParseCommand(arg0);
			Debug.Log(echoString);
			inputField.text = "";
		}
		inputField.ActivateInputField();
	}
}
