using System.Collections;
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
    private const string path = "./Assets/Resources/scores.txt";
    void Start () {
        StartNewGame();
	}
	
    public void StartNewGame()
    {
        elapsedTime = 0;
        isPaused = false;
        r = File.OpenText(path);
        string line;
        int lineNumber = 0;
        // reading and writing to file from here :
        // https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-open-and-append-to-a-log-file
        while ((line = r.ReadLine()) != null && lineNumber < 11)
        {
            scores[lineNumber] = Convert.ToDouble(line);
            print(scores[lineNumber]);
            lineNumber++;
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
        string scoresToWrite = "";
        int spotToAdd = -1;
        for (int i = 0; i < scores.Length; i++)
        {
            if (newScore < scores[i])
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
}
