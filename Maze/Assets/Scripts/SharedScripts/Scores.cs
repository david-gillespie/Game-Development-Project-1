using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class Scores {
	
	private static double[] scores = new double[10];
	private static StreamReader r;
	private const string path = "./Assets/Resources/scores.txt";
	private const string runningScoresPath = "./Assets/Resources/runningScore.txt";

	public static void createRunningScore(float value){
		System.IO.File.WriteAllText(runningScoresPath, value.ToString());
	}

	public static float readRunningScore(){
		r = File.OpenText(runningScoresPath);
		string line;
		line = r.ReadLine ();
		if (line == null) {
			line = "0";
		}
		r.Close ();
		return float.Parse (line);
	}

	public static void AddToScores(double newScore)
	{
		readScores ();
		string scoresToWrite = "";
		int spotToAdd = -1;
		for (int i = 0; i < scores.Length; i++)
		{
			if (newScore > scores[i])
			{
				spotToAdd = i;
				break;
			}
		}
		if (spotToAdd != -1)
		{
			for (int i = scores.Length; i >= spotToAdd; i--)
			{
				if (i + 1 < scores.Length)
					scores[i + 1] = scores[i];
			}
			scores[spotToAdd] = newScore;
		}
		for (int i = 0; i < scores.Length; i++)
		{
			scoresToWrite += Convert.ToString(scores[i]) + "\n";
		}        
		File.WriteAllText(path, scoresToWrite);
	}

	public static double[] readScores(){
		r = File.OpenText(path);
		string line;
		int lineNumber = 0;
		// reading and writing to file from here :
		// https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-open-and-append-to-a-log-file
		while ((line = r.ReadLine()) != null && lineNumber < 11)
		{
			scores[lineNumber] = Convert.ToDouble(line);
			lineNumber++;
		}
		r.Close();
		return scores;
	}
}
