// TileManager.cs
// Author:
//       Alexis Barta <alexisbarta@outlook.com>
// Copyright (c) 2018 
// Description: This script manages the tile spawning.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class TileManager : MonoBehaviour
{
	//Arrays to hold the tile prefabs, assigned in editor
	public GameObject[] tilesPrefabs;
	public GameObject[] largeTilesPrefabs;

	//What is the current title spawned
	public GameObject currentTile;

	//Path tiles that attach on the left
	private Stack<GameObject> leftTiles = new Stack<GameObject> ();

	public Stack<GameObject> LeftTiles {
		get { return leftTiles; }
		set { leftTiles = value; }
	}

	//Path tiles that attach on the top(forward)
	private Stack<GameObject> topTiles = new Stack<GameObject> ();

	public Stack<GameObject> TopTiles {
		get { return topTiles; }
		set { topTiles = value; }
	}

	//Large Platform: TwinHeads, attaches on the left
	private Stack<GameObject> lt_TwinHeads = new Stack<GameObject> ();

	public Stack<GameObject> LT_TwinHeads {
		get { return lt_TwinHeads; }
		set { lt_TwinHeads = value; }
	}

	//Large Platfrom: Snowdrifts, attaches on the left
	private Stack<GameObject> lt_Snowdrifts = new Stack<GameObject> ();

	public Stack<GameObject> LT_Snowdrifts {
		get { return lt_Snowdrifts; }
		set { lt_Snowdrifts = value; }
	}

	//Large Platform: Island, attaches on the top
	private Stack<GameObject> tl_Island = new Stack<GameObject> ();

	public Stack<GameObject> TL_Island {
		get { return tl_Island; }
		set { tl_Island = value; }
	}

	//Large Platform: Lava, attaches on the top
	private Stack<GameObject> tl_Lava = new Stack<GameObject> ();

	public Stack<GameObject> TL_Lava {
		get { return tl_Lava; }
		set { tl_Lava = value; }
	}

	//Singleton instance of the Tilemanager, so there's only ever one
	private static TileManager instance;

	public static TileManager Instance {
		get {
			if (instance == null) {
				instance = GameObject.FindObjectOfType<TileManager> ();
			}
			return instance;
		}
	}

	//How many tiles spawn before a large tile spawns
	private int tileCount;

	//Reference to the Game Manager script
	private GameManager gameManager;

	void Start ()
	{
		gameManager = FindObjectOfType<GameManager> ();
		CreateTiles (50);
		//CreateLargeTiles (0);

		for (int i = 0; i < 30; i++) 
		{
			SpawnTile ();
		}		
	}

	//Creates the Path Tiles
	public void CreateTiles (int amount)
	{
		for (int i = 0; i < amount; i++) 
		{
			leftTiles.Push (Instantiate (tilesPrefabs [0])); //Left Path Tiles
			topTiles.Push (Instantiate (tilesPrefabs [1])); //Top Path Tiles

			leftTiles.Peek ().name = "LeftTile";
			topTiles.Peek ().name = "TopTile";

			leftTiles.Peek ().SetActive (false);
			topTiles.Peek ().SetActive (false);
		}
	}

	//Creates the Large Tiles
	public void CreateLargeTiles (int amount)
	{
		for (int i = 0; i < amount; i++) 
		{

			lt_TwinHeads.Push (Instantiate (largeTilesPrefabs [0])); //TwinHeads Tile
			lt_Snowdrifts.Push (Instantiate (largeTilesPrefabs [1])); //Snowdrift Tile
			tl_Island.Push (Instantiate (largeTilesPrefabs [2])); //Island Tile
			tl_Lava.Push (Instantiate (largeTilesPrefabs [3])); //Lava Tile

			lt_TwinHeads.Peek ().name = "LT_TwinHeads";
			lt_Snowdrifts.Peek ().name = "LT_Snowdrifts";
			tl_Island.Peek ().name = "TL_Island";
			tl_Lava.Peek ().name = "TL_Lava";

			lt_TwinHeads.Peek ().SetActive (false);
			lt_Snowdrifts.Peek ().SetActive (false);
			tl_Island.Peek ().SetActive (false);
			tl_Lava.Peek ().SetActive (false);
		}
	}

	public void SpawnTile ()
	{
		//These make sure we always have tiles to pull from, even if they do not get put back into the stack
		if (leftTiles.Count == 0 || topTiles.Count == 0)
		{
			CreateTiles (10);
		}

		if (lt_TwinHeads.Count == 0 || lt_Snowdrifts.Count == 0) 
		{
			CreateLargeTiles (10);
		}

		if (tl_Island.Count == 0 || tl_Lava.Count == 0) 
		{
			CreateLargeTiles (10);
		}


		//Get random range for path tile 
		int randomIndex = Random.Range (0, 2);

		//Get random range for large tile
		int randomIndexForLarge = Random.Range (0, 2);

		//Regular sized tiles
		if (randomIndex == 0) 
		{
			GameObject tmp = leftTiles.Pop ();
			tmp.SetActive (true);
			tmp.transform.position = currentTile.transform.GetChild (0).transform.GetChild (randomIndex).position;
			currentTile = tmp;

			//When you spawn a path tile, increase the tile count
			gameManager.tileCount++;
		} 
		else if (randomIndex == 1) 
		{	
			GameObject tmp = topTiles.Pop ();
			tmp.SetActive (true);
			tmp.transform.position = currentTile.transform.GetChild (0).transform.GetChild (randomIndex).position;
			currentTile = tmp;

			//When you spawn a path tile, increase the tile count
			gameManager.tileCount++;
		} 

		//Generate a number for what stone is spawned on the path tiles
		int spawnStone = Random.Range (0, 3);

		if (currentTile.GetComponent<Tile> ().stonesList != null) 
		{
			GameObject stones = currentTile.GetComponent<Tile> ().stonesList;
			if (spawnStone == 0) 
			{
				stones.gameObject.transform.GetChild (0).transform.gameObject.SetActive (true);
			} 
			else if (spawnStone == 1) 
			{
				stones.gameObject.transform.GetChild (1).transform.gameObject.SetActive (true);
			} 
			else if (spawnStone == 2) 
			{
				stones.gameObject.transform.GetChild (2).transform.gameObject.SetActive (true);
			} 
			else 
			{
				// Do nothing!
			}
		} 
		else 
		{
			if (currentTile.transform.childCount == 2)
			{
			 //TODO: Fix this section apparently this never finishhhhheeedd?????
			}
		}
		
		//Large tiles
		if (gameManager.tileCount > 45 && gameManager.tileCount < 60) 
		{
			if (randomIndex == 0) 
			{
				// Spawn a "Left" tile
				if (randomIndexForLarge == 0) 
				{
					//Spawan a Twins Heads Tile
					GameObject tmp = lt_TwinHeads.Pop ();
					tmp.SetActive (true);
					tmp.transform.position = currentTile.transform.GetChild (0).transform.GetChild (randomIndex).position;
					currentTile = tmp;

				} 
				else if (randomIndexForLarge == 1) 
				{ 
					//Spawn a Snowdrifts Tile
					GameObject tmp = lt_Snowdrifts.Pop ();
					tmp.SetActive (true);
					tmp.transform.position = currentTile.transform.GetChild (0).transform.GetChild (randomIndex).position;
					currentTile = tmp;
				}
			} 
			else if (randomIndex == 1) 
			{ 
				// Spawn a "Top" tile
				if (randomIndexForLarge == 0) 
				{
					//Spawan a Twins Heads Tile
					GameObject tmp = tl_Island.Pop ();
					tmp.SetActive (true);
					tmp.transform.position = currentTile.transform.GetChild (0).transform.GetChild (randomIndex).position;
					currentTile = tmp;
				} 
				else if (randomIndexForLarge == 1) 
				{ 
					//Spawn a Lava Tile
					GameObject tmp = tl_Lava.Pop ();
					tmp.SetActive (true);
					tmp.transform.position = currentTile.transform.GetChild (0).transform.GetChild (randomIndex).position;
					currentTile = tmp;
				}
			}
			// Rest the tile count to 0 after you spawn a large tile
			gameManager.tileCount = 0;
		}
		//Generate a number for whether the tile spawns a pick up
		int spawnPickup = Random.Range (0, 10);

		if (spawnPickup > 0 && spawnPickup < 3) 
		{
			if (currentTile.transform.childCount > 0) 
			{
				if (currentTile.GetComponent<Tile> ().pickUp != null) 
				{
					currentTile.GetComponent<Tile> ().pickUp.SetActive (true);
				} 
				else 
				{
					Debug.Log ("# of children on this object: " + currentTile.transform.childCount + " | Object: " + currentTile.name);
				}	
			}
		}
	}
}
