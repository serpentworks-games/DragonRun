using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public int score;
	public int tileCount;
	public float playerSpeed;

	public int blueGemPoints;
	public int redGemPoints;

	private Player player;
	private TileManager tileManager;
	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player> ();
		tileManager = FindObjectOfType<TileManager> ();

		score = 0;
		tileCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
