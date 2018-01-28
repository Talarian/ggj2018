using System;
using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
	[Serializable]
	public class Configuration
	{
		public float easyDelay = 3f;
		public float normalDelay = 5f;
		public float hardDelay = 420f;
	}
	public Configuration configuration = new Configuration();

	private enum Difficulty
	{
		Easy,
		Normal,
		Hard
	}

	private CommandQueue commandQueue;
	private CommandParser commandParser;
	private float lastRequest = float.MaxValue;

	private Difficulty difficulty = Difficulty.Normal;
	private Difficulty DifficultyValue
	{
		get { return difficulty; }
		set
		{
			difficulty = value;
			if (difficulty == Difficulty.Easy)
			{
				commandQueue.configuration.commandDelayInSeconds = configuration.easyDelay;
			}
			else if (difficulty == Difficulty.Normal)
			{
				commandQueue.configuration.commandDelayInSeconds = configuration.normalDelay;
			}
			else if (difficulty == Difficulty.Hard)
			{
				commandQueue.configuration.commandDelayInSeconds = configuration.hardDelay;
			}
		}
	}

	private void Start()
	{
		commandQueue = FindObjectOfType<CommandQueue>();
		commandParser = FindObjectOfType<CommandParser>();
	}

	private void Update()
	{
		if (DifficultyValue == Difficulty.Easy)
		{
			lastRequest += Time.deltaTime;
			if (lastRequest > configuration.easyDelay)
			{
				lastRequest = 0f;
				commandParser.ParseCommand("sts", true);
				commandParser.ParseCommand("img", true);
				commandParser.ParseCommand("rdr", true);
				commandParser.ParseCommand("hgtmp", true);
			}
		}
	}

	public void SetEasy()
	{
		DifficultyValue = Difficulty.Easy;
	}

	public void SetNormal()
	{
		DifficultyValue = Difficulty.Normal;
	}

	public void SetMars()
	{
		DifficultyValue = Difficulty.Hard;
	}
}