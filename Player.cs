// Player.cs
// Author:
//       Alexis Barta <alexisbarta@outlook.com>
// Copyright (c) 2018 SerpentWorks Games
// Description: Controller for the player, handles input, death, and picking up gems
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework.Constraints;
using NUnit.Framework;

public class Player : MonoBehaviour {

	public float speed;
	public bool isDead;
	public ParticleSystem blueGemParticles;
	public ParticleSystem redGemParticles;

	private Vector3 dir;
	private bool isMoving;
	private Animator playerAnim;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		dir = Vector3.zero;
		isDead = false;
		isMoving = false;
		playerAnim = GetComponent<Animator> ();
		playerAnim.SetBool ("isMoving",false);
		gameManager = FindObjectOfType<GameManager> ();
		speed = gameManager.playerSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown (0) && !isDead){
			isMoving = true;
	
			if (dir == Vector3.forward) {
				dir = Vector3.left;
				
				transform.GetChild (0).rotation = Quaternion.LookRotation (dir);
				
				
			} else {
				dir = Vector3.forward;
				
				transform.GetChild (0).rotation = Quaternion.LookRotation (dir);
				
			}
			playerAnim.SetBool ("isMoving", true);
		}
		
		float amountToMove = speed * Time.deltaTime;

		transform.Translate (dir * amountToMove);


	}

	//Pick up behavior
	void OnTriggerEnter(Collider other){
		
		if (other.gameObject.tag == "BlueGem") {
			//When a player hits a 'blue gem'

			gameManager.score+= gameManager.blueGemPoints;
			other.gameObject.SetActive (false);
			Instantiate (blueGemParticles, transform.position, Quaternion.identity);
			UIManager.Instance.CreateFloatingText (other.transform.position, "+1");

		} else if( other.gameObject.tag == "RedGem"){
			//When a player hits a 'red gem'

			gameManager.score+= gameManager.redGemPoints;
			other.gameObject.SetActive (false);
			Instantiate (redGemParticles, transform.position, Quaternion.identity);
			UIManager.Instance.CreateFloatingText (other.transform.position, "+5");
		}
	}

	//Handles death
	void OnTriggerExit(Collider other){

		if (other.gameObject.tag == "Tile") {

			RaycastHit hit;
			Ray downRay = new Ray (transform.position, -Vector3.up);
			if (!Physics.Raycast (downRay, out hit)) {
				isDead = true;
				isMoving = false;
				GetComponent<CapsuleCollider> ().enabled = false;
				playerAnim.SetBool ("isDead",true);

			}
		}	
	}
		


}
