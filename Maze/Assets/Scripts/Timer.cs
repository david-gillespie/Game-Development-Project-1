using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour {

    public Text timerText;
    public Text pauseText;
    public Button nextGameButton;
    public Rigidbody pc;

    private Vector3 startPosition = new Vector3(-68, 0.5f, -68);
    private bool isPaused;
    private int maxSeconds = 120;
    private float elapsedTime;
	void Start () {
        startNewGame();
	}
	
    public void startNewGame()
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
