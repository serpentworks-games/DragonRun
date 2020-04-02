// GameManager.cs
// Author:
//       Alexis Barta <alexisbarta@outlook.com>
// Copyright (c) 2018 SerpentWorks Games
// Description: Game Manager script, holds all the variables that are not player specific
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Game score
    [System.NonSerialized]
	public int score;
	// Player's speed
	public float playerSpeed;
	// Value of the blue gem pickups 
	public int blueGemPoints;
	// Value of the red gem pickups
	public int redGemPoints;
	//How fast the tiles fall
	public float tileFallSpeed;
    //If the player has transitioned from the main menu to the game
    //[System.NonSerialized]
    public bool gameStarted;
    // Path tiles spawned 
    [System.NonSerialized]
    public int tileCount;

    public GameObject uiCanvas;

    private Player player;
	private TileManager tileManager;

	//Singleton pattern for the game manager so there's only ever one
	private static GameManager instance;

	public static GameManager Instance {
		get {
			if (instance == null) {
				instance = GameObject.FindObjectOfType<GameManager> ();
			}
			return instance;
		}
	}

	// Use this for initialization
	void Start () {
        Screen.SetResolution(500, 500, false); //Set screen size
		uiCanvas.SetActive (true);
		player = FindObjectOfType<Player> ();
		tileManager = FindObjectOfType<TileManager> ();
		gameStarted = false;
		score = 0;
		tileCount = 0;
	}

}
