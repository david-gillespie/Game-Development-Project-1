using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public Text winText;
    public Button nextGameButton;
    public Rigidbody player;
    
    private bool canMove;
    private bool gameOver;
    private float speed = 10;
    private int bordersize;
    private GameObject[] pickups;
    private string levelName;
	private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 launchPower = new Vector3(0, 75, 0);
    private Vector3 jump = new Vector3(0, 10, 0);

    void Start ()
    {
		levelName = SceneManager.GetActiveScene().name;
        nextGameButton.onClick.AddListener(changeScene);
        startNewGame();
    }
	
    public void startNewGame()
    {
        if (levelName == "Jump")
        {
            bordersize = 61;
        }
        startPosition = endPosition = player.transform.position;

        winText.text = "";
        gameOver = false;
        canMove = true;
        nextGameButton.gameObject.SetActive(false);

        pickups = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        foreach (GameObject g in pickups)
        {
            if (g.CompareTag("Launch Pad"))
                g.SetActive(true);
        }
        player.Sleep();
        player.WakeUp();
    }


	private void Update ()
    {   if (Input.GetKeyDown(KeyCode.Space))
            if ((player.position.y == 0.5 || player.position.y == 5.5) && winText.text == "")
            player.AddForce(jump * speed);
	}

    private void FixedUpdate()
    {
        if (canMove)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

            player.AddForce(movement * speed);
        }
		if ((levelName != "New Maze") && (Math.Abs(player.position.x) > bordersize || Math.Abs(player.position.z) > bordersize))
		{
			OutOfBounds ();
        }
    }

	//All of these should be replaced with functions
    private void OnTriggerEnter(Collider other)
    {
        if (!gameOver && !nextGameButton.IsActive())
        {
            if (other.gameObject.CompareTag("Win Zone"))
            {
				WinZone ();
            }
            else if (other.gameObject.CompareTag("Launch Pad"))
            {
                other.gameObject.SetActive(false);
                player.AddForce(launchPower * speed);
            }
            else if (other.gameObject.CompareTag("Teleporter"))
            {
                teleport(other, "Teleporter 2");
            }
            else if (other.gameObject.CompareTag("Teleporter 2"))
            {
                teleport(other, "Teleporter");
            }
        }
    }

	private void OutOfBounds(){
		canMove = false;
		player.MovePosition(endPosition);
		gameOver = true;
		winText.text = "You Lose.";
		nextGameButton.gameObject.SetActive(true);
		player.Sleep();
		player.WakeUp();
	}

	private void WinZone(){
		player.MovePosition(endPosition);
		winText.text = "You Win!";
		player.Sleep();
		player.WakeUp();
		canMove = false;
		nextGameButton.gameObject.SetActive(true);
	}

    private void teleport(Collider other, string tagToMatch)
    {
        other.gameObject.SetActive(false);
        Vector3 tpLoc = jump;
        pickups = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        foreach (GameObject go in pickups)
        {
            if (go.gameObject.CompareTag(tagToMatch))
            {
                go.SetActive(false);
                tpLoc = go.transform.position;
                break;
            }
        }
        if (tpLoc != jump)
        {
            player.MovePosition(tpLoc);
        }
    }

    private void EnemyCollision()
    {
        player.transform.position = startPosition;
    }

    private void changeScene()
    {
        
        switch (levelName)
        {
            case "Jump":
                SceneManager.LoadScene("MainMenu");
                break;
			case "New Maze":
				SceneManager.LoadScene ("Jump");
				break;
        	default:
                break;
        }
        //SceneManager.LoadScene("MainMenu");
    }
}