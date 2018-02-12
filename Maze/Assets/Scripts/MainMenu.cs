using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Button startButton;
    public Button highScoreButton;
    public Button backButton;
    public Text highScores;
    public Text devNames;

    private StreamReader r;
    private const string path = "./Assets/Resources/scores.txt";

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        highScoreButton.onClick.AddListener(ShowHighScores);
        backButton.onClick.AddListener(GoBack);
        backButton.gameObject.SetActive(false);
        highScores.text = "";
    }
    
    private void StartGame()
    {
        SceneManager.LoadScene("New Maze");
    }

    private void ShowHighScores()
    {
        devNames.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        highScoreButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
        r = File.OpenText(path);
        string line;
        int lineNumber = 1;
        string scores = "High Scores:\n";
        while ((line = r.ReadLine()) != null)
        {
            scores += Convert.ToString(lineNumber) + ") " + line + " seconds\n";
            lineNumber++;
        }
        highScores.text = scores;
        r.Close();
    }

    private void GoBack()
    {
        startButton.gameObject.SetActive(true);
        highScoreButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
        devNames.gameObject.SetActive(true);
        highScores.text = "";
    }
}
