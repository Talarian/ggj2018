using System;
using System.Collections;
using System.Collections.Generic;

public class Command
{
	public enum CommandType
	{
		Velocity,
		Wheels,
		Sensors,
	};

	public CommandType commandType;
	public float commandValue;
	public DateTime timeStamp;
	public string originalText;
	public bool localOnly;
}
