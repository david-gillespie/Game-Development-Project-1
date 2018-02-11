using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    
    public Button nextGameButton;
    public string startingText;
    public Slider boostText;
	public Text winText;
    public Vector3 movespeed;

    private bool canMove;
	private bool grounded;
    private bool isBoosting;
    private float speed = 100;
	private float boost;
    private const int minSpeed = 75;
    private const int maxSpeed = 200;
    private GameObject[] pickups;
	private Rigidbody player;
    private string levelName;
	private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 launchPower = new Vector3(0, 75, 0);
    private Vector3 jump = new Vector3(0, 15, 0);

    void Start ()
    {
		player = GetComponent<Rigidbody> ();
		levelName = SceneManager.GetActiveScene().name;
        startNewGame();
    }
	
    public void startNewGame()
    {
        if (levelName == "Jump")
        {
            jump = new Vector3(0, 40, 0);
            winText.color = Color.blue;
        }
        else if (levelName == "New Maze")
        {
            winText.color = Color.green;
        }
        else if (levelName == "Race")
        {
            speed = minSpeed;
            winText.color = Color.blue;
            isBoosting = false;
            boost = 200;
            boostText.value = boost;
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
			if (levelName == "Race" && player.position != startPosition && Input.GetKey (KeyCode.LeftShift)) {
				if (boost > 1 && speed < maxSpeed) {
					IncreaseSpeed ();
				}

			} else if (levelName == "Race" && boost < maxSpeed - 1 && !isBoosting) {
				RestoreBoost ();
			}

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

            movespeed = movement * speed;
            player.AddForce(movespeed);
			if ((levelName == "Race" && speed > minSpeed) || speed > 100) {
				speed -= 2;
			}
            if (winText.text == startingText && player.position != startPosition)
            {
                winText.color = Color.black;
                winText.text = "";
				SendMessage ("StartTimer");
            }
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (grounded && winText.text == "") {
					player.AddForce (jump * speed);
				}
			}
            if (winText.text == "Game Over!")
            {
                canMove = false;
                player.MovePosition(startPosition);
                player.Sleep();
                player.WakeUp();
            }
            isBoosting = false;
        }
    }

    private void IncreaseSpeed()
    {
        speed += 5;
        boost -= 1;
        boostText.value = boost;
        isBoosting = true;
    }

    private void RestoreBoost()
    {
        boost += 0.5f;
        boostText.value = boost;
    }

	private void OutOfBounds(){
        if (levelName != "Jump")
        {
            canMove = false;
            player.MovePosition(endPosition);
            winText.text = "You Lose.";
            nextGameButton.gameObject.SetActive(true);
            player.Sleep();
            player.WakeUp();
        }
        else {
            SendMessage("ResetPosition");
        }
    }

	private void WinZone(){
		player.MovePosition(endPosition);
		winText.fontSize = 27;
        winText.color = Color.yellow;
        winText.text = "You Win!";
		player.Sleep();
		player.WakeUp();
		canMove = false;
		ChangeButtonScene ();
	}

	private void ChangeButtonScene(){
		nextGameButton.gameObject.SetActive(true);
		nextGameButton.onClick.AddListener (ChangeScene);
	}

    private void LoseZone()
    {
        player.MovePosition(endPosition);
        winText.fontSize = 27;
        winText.color = Color.yellow;
        winText.text = "You Lose!";
        player.Sleep();
        player.WakeUp();
        canMove = false;
        nextGameButton.gameObject.SetActive(true);
        nextGameButton.GetComponentInChildren<Text>().text = "Restart Game";
        nextGameButton.onClick.AddListener(RestartGame);
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

    private void RestartGame()
    {
        SceneManager.LoadScene(levelName);
    }

    private void ChangeScene()
    {
        print(levelName);
        switch (levelName)
        {
            case "Jump":
                SceneManager.LoadScene("Race");
                break;
			case "New Maze":
				SceneManager.LoadScene ("Jump");
				break;
            case "Race":
                SceneManager.LoadScene("MainMenu");
                break;
        	default:
                break;
        }
    }

	private void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "Untagged"){
			grounded = true;
		}
	}

	private void OnCollisionExit(Collision collision){
		if(collision.gameObject.tag == "Untagged"){
			grounded = false;
		}
	}
}
