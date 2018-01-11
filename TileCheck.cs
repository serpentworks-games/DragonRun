using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCheck : MonoBehaviour {

	public Player player;
	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "RightTile"){
			player.canMoveRight = true;
			Debug.Log ("Right tile in range! Can move right!");
		} else {
			player.canMoveRight = false;
		}
	}
}
