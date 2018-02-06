using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    
    public Button nextGameButton;
	public Text winText;
    public string startingText;

    private bool canMove;
    private float speed = 10;
    private GameObject[] pickups;
	private Rigidbody player;
    private int coinsCollected;
    private string levelName;
	private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 launchPower = new Vector3(0, 75, 0);
    private Vector3 jump = new Vector3(0, 15, 0);

    void Start ()
    {
		player = GetComponent<Rigidbody> ();
		levelName = SceneManager.GetActiveScene().name;
        nextGameButton.onClick.AddListener(changeScene);
        startNewGame();
    }
	
    public void startNewGame()
    {
        if (levelName == "Jump")
        {
            jump = new Vector3(0, 40, 0);
            coinsCollected = 0;
            winText.color = Color.blue;
        }
        else if (levelName == "New Maze")
        {
            winText.color = Color.green;
        }
        startPosition = endPosition = player.transform.position;
        
        winText.text = startingText;
        canMove = true;
        nextGameButton.gameObject.SetActive(false);

        player.Sleep();
        player.WakeUp();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

            player.AddForce(movement * speed);
            if (winText.text == startingText && player.position != startPosition)
            {
                winText.color = Color.black;
                winText.text = "";
				SendMessage ("startTimer");
            }
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (player.velocity.y == 0 && winText.text == "") {
					player.AddForce (jump * speed);
				}
			}
        }
    }

	private void OutOfBounds(){
		canMove = false;
		player.MovePosition(endPosition);
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

    private void Teleporter1Collision()
    {
        teleport("Teleporter 2");
    }

    private void Teleporter2Collision()
    {
        teleport("Teleporter");
    }

    private void teleport(string tagToMatch)
    {
        Vector3 tpLoc = jump;
        pickups = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        foreach (GameObject go in pickups)
        {
            if (go.gameObject.CompareTag(tagToMatch))
            {
                go.SetActive(false);
                tpLoc = go.transform.position;
                tpLoc.y += 0.5f;
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
        player.Sleep();
        player.WakeUp();
    }

    private void LaunchPlayer()
    {
        player.AddForce(launchPower * speed);
    }

    private void CoinCollected()
    {
        coinsCollected++;
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
    }
}