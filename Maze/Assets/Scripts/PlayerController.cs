﻿using System.Collections;
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
	private bool startPositionSet = false;
    private float speed = 10;
    private int bordersize;
    private GameObject[] pickups;
    private string levelName;
	private Vector3 startPostion;
    private Vector3 endPosition;
    private Vector3 launchPower = new Vector3(0, 25, 0);
    private Vector3 jump = new Vector3(0, 10, 0);

    void Start ()
    {
        levelName = Application.loadedLevelName;
        nextGameButton.onClick.AddListener(changeScene);
        startNewGame();
    }
	
    public void startNewGame()
    {
        if (levelName == "Maze")
        {
            startPostion = new Vector3(-68, 0.5f, -68);
            endPosition = new Vector3(-66, 0.5f, -66);
            bordersize = 71;
			startPositionSet = true;
        }
        else if (levelName == "Jump")
        {
            startPostion = new Vector3(0, 0.5f, 0);
            endPosition = new Vector3(0, 5.5f, 40);
            launchPower = new Vector3(0, 75, 0);
            bordersize = 61;
			startPositionSet = true;
        }

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
		if (startPositionSet) {
			player.MovePosition (startPostion);
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
            if (other.gameObject.CompareTag("Launch Pad"))
            {
                other.gameObject.SetActive(false);
                player.AddForce(launchPower * speed);
            }
            if (other.gameObject.CompareTag("Teleporter"))
            {
                teleport(other, "Teleporter 2");
            }
            if (other.gameObject.CompareTag("Teleporter 2"))
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

    private void changeScene()
    {
        
        switch (levelName)
        {
            case "Maze":
                SceneManager.LoadScene("Jump");
                break;
            case "Jump":
                SceneManager.LoadScene("MainMenu");
                break;
		case "New Maze":
				SceneManager.LoadScene ("MainMenu");
				break;
            default:
                break;
        }
        //SceneManager.LoadScene("MainMenu");
    }
}