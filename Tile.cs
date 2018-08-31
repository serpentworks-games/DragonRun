// Tile.cs
// Author:
//       Alexis Barta <alexisbarta@outlook.com>
// Copyright (c) 2018 SerpentWorks Games
// Description: Behavior for the individual tiles
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour 
{
	//How long it takes the platform to fall
	public float fallDelay;

	//The pick up on that tile
	public GameObject pickUp;
	public GameObject stonesList;


	void Awake()
	{
		//Find the first pick up on the object
		pickUp = this.transform.GetChild (1).gameObject;
		if (this.transform.childCount == 3) 
		{
			stonesList = this.transform.GetChild (2).gameObject;
		} 
		else 
		{
			// do nothing
		}
	}

	void Start () 
	{
		//Set the falldelay to the speed set in the game manager
		fallDelay = GameManager.Instance.tileFallSpeed;
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") 
		{
			// When the player exits the tile's collider, spawn the next tile, and
			// have this tile fall down
			if (this.transform.childCount > 0) 
			{	
				TileManager.Instance.SpawnTile ();
				StartCoroutine (FallDown ());
			}
		} 
	}

	IEnumerator FallDown()
	{
		//This Enumerator controlls the falling and reseting behavior of the tiles
		//This will repush the tiles to the stack (supposedly, this is known to break)

		yield return new WaitForSeconds (fallDelay);
		this.GetComponent<Rigidbody> ().isKinematic = false;
		yield return new WaitForSeconds (0.5f);
		switch(gameObject.name)
		{
		case "LeftTile":
			TileManager.Instance.LeftTiles.Push (gameObject);
			gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			gameObject.SetActive (false);
			stonesList.transform.GetChild (0).gameObject.SetActive (false);
			stonesList.transform.GetChild (1).gameObject.SetActive (false);
			stonesList.transform.GetChild (2).gameObject.SetActive (false);
				break;

		case "TopTile":
			TileManager.Instance.TopTiles.Push (gameObject);
			gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			gameObject.SetActive (false);
			stonesList.transform.GetChild (0).gameObject.SetActive (false);
			stonesList.transform.GetChild (1).gameObject.SetActive (false);
			stonesList.transform.GetChild (2).gameObject.SetActive (false);
				break;

		case "LT_TwinHeads":
			TileManager.Instance.LT_TwinHeads.Push (gameObject);
			gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			gameObject.SetActive (false);
			break;

		case "LT_Snowdrifts":
			TileManager.Instance.LT_Snowdrifts.Push (gameObject);
			gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			gameObject.SetActive (false);
			break;

		case "TL_Island":
			TileManager.Instance.TL_Island.Push (gameObject);
			gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			gameObject.SetActive (false);
			break;

		case "TL_Lava":
			TileManager.Instance.TL_Lava.Push (gameObject);
			gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			gameObject.SetActive (false);
			break;
		}
	}
}
