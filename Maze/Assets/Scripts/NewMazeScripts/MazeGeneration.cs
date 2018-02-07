using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneration : MonoBehaviour {

	public GameObject wallPrefabVerical;
	public GameObject wallPrefabHorizontal;
	public GameObject parent;
	public GameObject ghostPrefab;
	public GameObject ghostParents;
    public Material[] materials;
	public GameObject mazeControlObject;

	private const int numberOfGhosts = 20;
	private const int mazeScale = 10;
	private const int mazeOffSet = -50;
	private const int wallSegmentSize = 10;
	private GameObject[,] mazeArrayVertical = new GameObject[mazeScale,mazeScale];
	private GameObject[,] mazeArrayHorizontal = new GameObject[mazeScale,mazeScale];
	private GameObject[] ghosts = new GameObject[numberOfGhosts];

	void Start () {
		FillMaze ();
		GenerateMaze ();
		AddGhosts ();
		mazeControlObject.SendMessage ("getGhosts",  ghosts);
	}

	void AddGhosts(){
		for (int i = 0; i < numberOfGhosts; i++) {
			GameObject newGhostObject = Instantiate (ghostPrefab);
			newGhostObject.transform.SetParent (ghostParents.transform);
			newGhostObject.transform.position = new Vector3 (((int) Mathf.Floor(Random.Range(1,mazeScale-2)))*10+mazeOffSet+5,0.01f,((int) Mathf.Floor(Random.Range(1,mazeScale-2)))*10+mazeOffSet+5);
			float force = Random.Range (100, 400);
            newGhostObject.GetComponentInChildren<Renderer>().material = materials[Random.Range(0, 3)];
            newGhostObject.GetComponent<Rigidbody> ().AddForce (new Vector3 (400.0f-force,0.0f,force));
			ghosts [i] = newGhostObject;
		}

	}

	void FillMaze(){
		//Fill in the horizontal segments for the maze
		for(int x = wallSegmentSize; x<mazeScale*wallSegmentSize;x+=wallSegmentSize){
			for (int z = 0; z < mazeScale*wallSegmentSize; z += wallSegmentSize) {
				GameObject newWallObject = Instantiate (wallPrefabVerical);
				newWallObject.transform.SetParent (parent.transform);
				newWallObject.transform.position = new Vector3 (x+mazeOffSet,0.5f,z+mazeOffSet+(wallSegmentSize/2));
				mazeArrayVertical [(x / wallSegmentSize),z / wallSegmentSize] = newWallObject;
			}
		}
		//Fill in the vertical segments for the maze
		for(int x = 0; x<mazeScale*wallSegmentSize;x+=wallSegmentSize){
			for (int z = wallSegmentSize; z < mazeScale*wallSegmentSize; z += wallSegmentSize) {
				GameObject newWallObject = Instantiate (wallPrefabHorizontal);
				newWallObject.transform.SetParent (parent.transform);
				newWallObject.transform.position = new Vector3 (x+mazeOffSet+(wallSegmentSize/2),0.5f,z+mazeOffSet);
				mazeArrayHorizontal [x / wallSegmentSize,(z / wallSegmentSize)] = newWallObject;
			}
		}
	}

	void removeWall(int x, int y, bool isHorizontal){
		if (!isHorizontal) {
			mazeArrayHorizontal [x, y].SetActive (false);
		} else {
			mazeArrayVertical [x, y].SetActive (false);
		}
	}

	bool checkBounds (int x, int y){
		if (x >= 0 && x < mazeScale) {
			if (y >= 0 && y < mazeScale) {
				return true;
			}
		}
		return false;
	}

	//Code adapted from my c++ oop project
	void GenerateMaze() {
		//int counter = 0;
		bool[,] mazeGenerationArray = new bool[mazeScale,mazeScale];
		for (int x = 0; x < mazeScale; x++) {
			for (int z = 0; z < mazeScale; z++) {
				mazeGenerationArray [x, z] = false;
			}
		}
			
		int startX = (int) Mathf.Floor(Random.Range(0,mazeScale));
		int startY = (int) Mathf.Floor(Random.Range(0,mazeScale));
		Stack <Vector2> mazeStack = new Stack<Vector2>();
		mazeStack.Push(new Vector2(startX,startY));
		Vector2 currentIndex = mazeStack.Peek();
		mazeGenerationArray[(int) currentIndex.x,(int) currentIndex.y] = true;
		int moves;
		int[] possiblesMoves = new int[4];
		while (mazeStack.Count != 0) {
			//counter++;
			moves = 0;
			if (checkBounds((int) currentIndex.x + 1, (int) currentIndex.y) && !mazeGenerationArray[(int) currentIndex.x + 1,(int) currentIndex.y]) {
				possiblesMoves[moves++] = 0;
			}
			if (checkBounds((int) currentIndex.x - 1, (int) currentIndex.y) && !mazeGenerationArray[(int) currentIndex.x - 1,(int) currentIndex.y]) {
				possiblesMoves[moves++] = 1;
			}
			if (checkBounds((int) currentIndex.x, (int) currentIndex.y + 1) && !mazeGenerationArray[(int) currentIndex.x,(int) currentIndex.y + 1]) {
				possiblesMoves[moves++] = 2;
			}
			if (checkBounds((int) currentIndex.x, (int) currentIndex.y - 1) && !mazeGenerationArray[(int) currentIndex.x,(int) currentIndex.y - 1]) {
				possiblesMoves[moves++] = 3;
			}
			if (moves > 0) {
				mazeStack.Push(currentIndex);
				switch (possiblesMoves[(int) Mathf.Floor(Random.Range(0,moves))]) {
				case 0:
					removeWall ((int)currentIndex.x + 1, (int)currentIndex.y, true);
					mazeGenerationArray [(int)currentIndex.x + 1, (int)currentIndex.y] = true;
					currentIndex = new Vector2 ((int) currentIndex.x+1,(int) currentIndex.y);
					break;
				case 1:
					removeWall ((int) currentIndex.x, (int) currentIndex.y, true);
					mazeGenerationArray [(int)currentIndex.x - 1, (int)currentIndex.y] = true;
					currentIndex = new Vector2 ((int) currentIndex.x-1,(int) currentIndex.y);
					break;
				case 2:
					removeWall ((int) currentIndex.x, (int) currentIndex.y + 1, false);
					mazeGenerationArray [(int)currentIndex.x, (int)currentIndex.y + 1] = true;
					currentIndex = new Vector2 ((int) currentIndex.x,(int) currentIndex.y + 1);
					break;
				case 3:
					removeWall ((int) currentIndex.x, (int) currentIndex.y, false);
					mazeGenerationArray [(int)currentIndex.x, (int)currentIndex.y - 1] = true;
					currentIndex = new Vector2 ((int) currentIndex.x,(int) currentIndex.y - 1);
					break;
				}
			}
			else {
				mazeStack.Pop();
				if (mazeStack.Count != 0) {
					currentIndex = mazeStack.Peek();
				}
			}
		}
	}
}
