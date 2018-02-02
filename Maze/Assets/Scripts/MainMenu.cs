using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    private List<Button> buttons;
    void Start()
    {
        buttons = new List<Button>();
        buttons.AddRange(GameObject.FindObjectsOfType<Button>());
        foreach (Button button in buttons)
        {
            switch (button.tag)
            {
                case "Start Game Button":
                    button.onClick.AddListener(startGame);
                    break;
                case "High Scores":
                    button.onClick.AddListener(showHighScores);
                    break;
                default:
                    break;
            }
        }
    }
    
    private void startGame()
    {
        SceneManager.LoadScene("Maze");
    }
    private void showHighScores()
    {
        foreach (Button b in buttons)
            b.gameObject.SetActive(false);
        // TODO: 
            // read a file and write the file to the screen.
    }
}
