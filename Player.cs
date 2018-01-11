using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework.Constraints;
using NUnit.Framework;

public class Player : MonoBehaviour {

	public float speed;
	public bool isDead;
	public ParticleSystem particles;
	public bool canMoveRight;

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
			if (!canMoveRight) {
				if (dir == Vector3.forward) {
					dir = Vector3.left;
					
					transform.GetChild (0).rotation = Quaternion.LookRotation (dir);
					
					
				} else {
					dir = Vector3.forward;
					
					transform.GetChild (0).rotation = Quaternion.LookRotation (dir);
					
				}
				playerAnim.SetBool ("isMoving", true);
			} else if(canMoveRight){
				if (dir == Vector3.forward) {
					dir = Vector3.right;

					transform.GetChild (0).rotation = Quaternion.LookRotation (dir);


				} else {
					dir = Vector3.forward;

					transform.GetChild (0).rotation = Quaternion.LookRotation (dir);

				}
				playerAnim.SetBool ("isMoving", true);
			}
		}
			
		float amountToMove = speed * Time.deltaTime;

		transform.Translate (dir * amountToMove);


	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "PickUp") {
			gameManager.score+= gameManager.blueGemPoints;
			other.gameObject.SetActive (false);
			Instantiate (particles, transform.position, Quaternion.identity);
			UIManager.Instance.CreateFloatingText (other.transform.position, "+1");
			}
	}

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
