using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;


public class TileManager : MonoBehaviour {

	public GameObject[] tilesPrefabs;
	public GameObject[] largeTilesPrefabs;

	public GameObject currentTile;

	public float tileFallSpeed;

	private Stack<GameObject> leftTiles = new Stack<GameObject>();

	public Stack<GameObject> LeftTiles {
		get { return leftTiles; }
		set { leftTiles = value; }
	}

	private Stack<GameObject> topTiles = new Stack<GameObject>();

	public Stack<GameObject> TopTiles {
		get { return topTiles; }
		set { topTiles = value; }
	}

	private Stack<GameObject> rightTiles = new Stack<GameObject>();

	public Stack<GameObject> RightTiles {
		get { return rightTiles; }
		set { rightTiles = value; }
	}

	private Stack<GameObject> leftTilesLarge = new Stack<GameObject>();

	public Stack<GameObject> LeftTilesLarge {
		get { return leftTilesLarge; }
		set { leftTilesLarge = value; }
	}

	private Stack<GameObject> topTilesLarge = new Stack<GameObject>();

	public Stack<GameObject> TopTilesLarge {
		get { return topTilesLarge; }
		set { topTilesLarge = value; }
	}

	private Stack<GameObject> rightTilesLarge = new Stack<GameObject>();

	public Stack<GameObject> RightTilesLarge {
		get { return rightTilesLarge; }
		set { rightTilesLarge = value; }
	}
		

	private static TileManager instance;

	public static TileManager Instance { get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<TileManager> ();
			}
			return instance;
		}
	}

	private int tileCount;

	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager> ();
		CreateTiles (100);
		CreateLargeTiles (25);

		for (int i = 0; i < 30; i++) {
			SpawnTile ();

		}
	}
	
	// Update is called once per frame
	void Update () {
		

	}

	public void CreateTiles(int amount){
		for (int i = 0; i < amount; i++) {
			
			leftTiles.Push (Instantiate(tilesPrefabs[0])); //Left Tiles
			topTiles.Push (Instantiate(tilesPrefabs[1])); //Top Tiles
			//rightTiles.Push (Instantiate (tilesPrefabs[2]));

			leftTiles.Peek ().name = "LeftTile";
			topTiles.Peek ().name = "TopTile";
			//rightTiles.Peek ().name = "RightTile";

			leftTiles.Peek ().SetActive (false);
			topTiles.Peek ().SetActive (false);
			//rightTiles.Peek ().SetActive (false);
		}
	}

	public void CreateLargeTiles(int amount){
		for (int i = 0; i < amount; i++) {

			leftTilesLarge.Push (Instantiate(largeTilesPrefabs[0])); //Large Left Tile
			topTilesLarge.Push (Instantiate(largeTilesPrefabs[1])); //Large Top Tile
		//	rightTilesLarge.Push (Instantiate (tilesPrefabs[2]));

			leftTilesLarge.Peek ().name = "LeftTileLarge";
			topTilesLarge.Peek ().name = "TopTileLarge";
			//rightTilesLarge.Peek ().name = "RightTile";

			leftTilesLarge.Peek ().SetActive (false);
			topTilesLarge.Peek ().SetActive (false);
			//rightTilesLarge.Peek ().SetActive (false);

		}
	}

	public void SpawnTile ()
	{

		if (leftTiles.Count == 0 || topTiles.Count == 0) {
			CreateTiles (10);
			//Debug.LogAssertion ("Left or top tiles empty! Spawning more!");
		}

		if (leftTilesLarge.Count == 0 || topTilesLarge.Count == 0) {
			CreateLargeTiles (10);
		}

		//When you spawn a tile, increase the tile count
		gameManager.tileCount++;

		//Get random range for tile 
		int randomIndex = Random.Range (0, 2);

		//Regular sized tiles
		if (randomIndex == 0) {
			GameObject tmp = leftTiles.Pop ();
			tmp.SetActive (true);
			tmp.transform.position = currentTile.transform.GetChild (0).transform.GetChild (randomIndex).position;
			currentTile = tmp;

		} else if (randomIndex == 1) {
			
			GameObject tmp = topTiles.Pop ();
			tmp.SetActive (true);
			tmp.transform.position = currentTile.transform.GetChild (0).transform.GetChild (randomIndex).position;
			currentTile = tmp;
		} 

		//Large tiles
		if (gameManager.tileCount > 35 && gameManager.tileCount < 60){
			if (randomIndex == 0) { //BottomLeft Anchor
				GameObject tmp = leftTilesLarge.Pop ();
				tmp.SetActive (true);
				tmp.transform.position = currentTile.transform.GetChild (0).transform.GetChild (randomIndex).position;
				currentTile = tmp;

			} else if (randomIndex == 1) { //TopLeft Anchor

				GameObject tmp = topTilesLarge.Pop ();
				tmp.SetActive (true);
				tmp.transform.position = currentTile.transform.GetChild (0).transform.GetChild (randomIndex).position;
				currentTile = tmp;
			}

			// Rest the tile count to 0 after you spawn a large tile
			gameManager.tileCount = 0;
		}

		int spawnPickup = Random.Range (0, 10);

		if (spawnPickup > 0 && spawnPickup < 3) {
			if (currentTile.transform.childCount > 0) {
				if (currentTile.GetComponent<Tile> ().pickUp != null) {
					currentTile.GetComponent<Tile> ().pickUp.SetActive (true);
				} else {
					Debug.Log ("# of children on this object: " + currentTile.transform.childCount + " | Object: " + currentTile.name);
				}
					
			}
		}
	}
}
