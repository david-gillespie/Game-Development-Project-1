using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class Timer : MonoBehaviour {

    public Text timerText;
    public Text pauseText;
	public GameObject mazeControlObject;
    
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
		timerText.text = "0";
        isPaused = true;
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
    }

	void StartTimer(){
        if (SceneManager.GetActiveScene().name == "New Maze")
    		isPaused = false;
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
				Scores.createRunningScore (200.0f-float.Parse (timerText.text));
				print (Scores.readRunningScore ());
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
			if (pauseText.text != "You Lose." && pauseText.text != "You Win!"
			             && pauseText.text != "Paused") {
				pauseText.text = "Paused";
				mazeControlObject.SendMessage ("pauseGhosts");
			} else if (pauseText.text == "Paused") {
				pauseText.text = "";
				mazeControlObject.SendMessage ("resumeGhosts");
			}
        }

    }
		
}
