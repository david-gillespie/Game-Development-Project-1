using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour {

    public Text timerText;
    public Text pauseText;
    public Button nextGameButton;
    
    private bool isPaused;
    private float elapsedTime;
	void Start () {
        StartNewGame();
	}
	
    public void StartNewGame()
    {
        elapsedTime = 0;
        isPaused = false;
        if (pauseText.text != "You Win!" || pauseText.text != "You Lose.")
            pauseText.text = "";
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
}
