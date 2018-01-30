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
                    button.onClick.AddListener(showGameModes);
                    break;
                case "High Scores":
                    button.onClick.AddListener(showHighScores);
                    break;
                case "Easy":
                    button.onClick.AddListener(easyGame);
                    button.gameObject.SetActive(false);
                    break;
                case "Medium":
                    button.onClick.AddListener(mediumGame);
                    button.gameObject.SetActive(false);
                    break;
                case "Hard":
                    button.onClick.AddListener(hardGame);
                    button.gameObject.SetActive(false);
                    break;
                case "Extreme":
                    button.onClick.AddListener(extremeGame);
                    button.gameObject.SetActive(false);
                    break;
                case "Back":
                    button.onClick.AddListener(back);
                    button.gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }

    private void showGameModes()
    {
        foreach(Button button in buttons)
        {
            switch (button.tag)
            {
                case "Start Game Button":
                    button.gameObject.SetActive(false);
                    break;
                case "High Scores":
                    button.gameObject.SetActive(false);
                    break;
                case "Easy":
                    button.gameObject.SetActive(true);
                    break;
                case "Medium":
                    button.gameObject.SetActive(true);
                    break;
                case "Hard":
                    button.gameObject.SetActive(true);
                    break;
                case "Extreme":
                    button.gameObject.SetActive(true);
                    break;
                case "Back":
                    button.gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }

    private void showHighScores()
    {
        foreach (Button b in buttons)
            b.gameObject.SetActive(false);
        // TODO: 
            // read a file and write the file to the screen.
    }

    private void easyGame()
    {
        //SceneManager.LoadScene("Easy");
    }

    private void mediumGame()
    {
        SceneManager.LoadScene("Maze");
    }

    private void hardGame()
    {
        //SceneManager.LoadScene("Hard");
    }

    private void extremeGame()
    {
        //SceneManager.LoadScene("Extreme");
    }

    private void back()
    {
        foreach (Button button in buttons)
        {
            switch (button.tag)
            {
                case "Start Game Button":
                    button.gameObject.SetActive(true);
                    break;
                case "High Scores":
                    button.gameObject.SetActive(true);
                    break;
                case "Easy":
                    button.gameObject.SetActive(false);
                    break;
                case "Medium":
                    button.gameObject.SetActive(false);
                    break;
                case "Hard":
                    button.gameObject.SetActive(false);
                    break;
                case "Extreme":
                    button.gameObject.SetActive(false);
                    break;
                case "Back":
                    button.gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }
}
