﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.IO;

public class Timer : MonoBehaviour {

    public Text timerText;
    public Text pauseText;
    public Button nextGameButton;
    
    private bool isPaused;
    private float elapsedTime;
    private double[] scores = new double[10];
    private StreamReader r;
    private string path = "./Assets/Resources/scores.txt";
    void Start () {
        StartNewGame();
	}
	
    public void StartNewGame()
    {
        elapsedTime = 0;
        isPaused = false;
        if (pauseText.text != "You Win!" || pauseText.text != "You Lose.")
            pauseText.text = "";
        r = File.OpenText(path);
        string line;
        int lineNumber = 0;
        while ((line = r.ReadLine()) != null && lineNumber < 11)
        {
            scores[lineNumber] = Convert.ToDouble(line);
        }
        r.Close();
    }

	// Update is called once per frame
	void Update () {
        if (!isPaused)
        {
            if (pauseText.text != "You Win!" && pauseText.text != "You Lose.")
            {
                string seconds = "";
                elapsedTime += Time.deltaTime;
				seconds = elapsedTime.ToString("f1");
                timerText.text = seconds;
            }
            else if (pauseText.text == "You Win!")
            {
                isPaused = true;
                AddToScores(Convert.ToDouble(timerText.text));
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (pauseText.text != "You Lose." && pauseText.text != "You Win!"
                && pauseText.text != "Paused")
                pauseText.text = "Paused";
            else if (pauseText.text == "Paused")
                pauseText.text = "";
        }

    }

    public void AddToScores(double newScore)
    {
        string[] scoresToWrite = new string[10];
        int spotToAdd = -1;
        for (int i = 0; i < scores.Length; i++)
        {
            if (newScore < scores[i])
            {
                spotToAdd = i;
            }
        }
        if (spotToAdd != -1)
        {
            for (int i = spotToAdd; i < scores.Length; i++)
            {
                if (i + 1 < scores.Length)
                    scores[i + 1] = scores[i];
            }
            scores[spotToAdd] = newScore;
        }
        for (int i = 0; i < scores.Length; i++)
        {
            scoresToWrite[i] = Convert.ToString(scores[i]);
        }
        File.WriteAllLines(path, scoresToWrite);
    }
}
