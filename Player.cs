// Player.cs
// Author:
//       Alexis Barta <alexisbarta@outlook.com>
// Copyright (c) 2018 SerpentWorks Games
// Description: Controller for the player, handles input, death, and picking up gems
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{	public float speed;
    [System.NonSerialized]
    public bool isDead;
	public ParticleSystem blueGemParticles;
	public ParticleSystem redGemParticles;
	public LayerMask groundLayer;
	public Transform contactPoint;

    public AudioClip blueGemSound;
    public AudioClip redGemSound;
    public AudioClip deathSound;

	private Vector3 dir;
	private bool isMoving;
	private Animator playerAnim;
	private GameManager gameManager;
	private GameObject particleSpawn;
    private AudioSource deathAudioSource;
    private AudioSource gemAudioSource;

    // Use this for initialization
    void Start () 
	{
		dir = Vector3.zero;
		isDead = false;
		isMoving = false;
		playerAnim = GetComponent<Animator> ();
		playerAnim.SetBool ("isMoving",false);
		gameManager = FindObjectOfType<GameManager> ();
        deathAudioSource = GameObject.Find("DragonDeathFX").GetComponent<AudioSource>();
        gemAudioSource = GameObject.Find("DragonSoundFX").GetComponent<AudioSource>();
        speed = gameManager.playerSpeed;
		particleSpawn = GameObject.Find ("ParticleSpawnPoint");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!Grounded () && gameManager.gameStarted == true && isDead == false) 
		{
			isDead = true;
			isMoving = false;
			GetComponent<CapsuleCollider> ().enabled = false;
			playerAnim.SetBool ("isDead", true);
            if (!deathAudioSource.isPlaying)
            {
                deathAudioSource.PlayOneShot(deathSound);
            }
            
        }
			
		if (gameManager.gameStarted == true) {
			playerAnim.SetBool ("playGame", true);
			if (Input.GetMouseButtonDown (0) && !isDead) 
			{
				isMoving = true;
				if (dir == Vector3.forward) 
				{
					dir = Vector3.left;

					transform.GetChild (0).rotation = Quaternion.LookRotation (dir);
				} 
				else 
				{
					dir = Vector3.forward;
					
					transform.GetChild (0).rotation = Quaternion.LookRotation (dir);
				}
				playerAnim.SetBool ("isMoving", true);
			}
			
			float amountToMove = speed * Time.deltaTime;
			
			transform.Translate (dir * amountToMove);
		} 
	}

	void OnCollision(Collision col)
	{
		if(col.gameObject.layer == 8)
		{
			isDead = true;
			isMoving = false;
			GetComponent<CapsuleCollider> ().enabled = false;
			playerAnim.SetBool ("isDead", true);
            if (!deathAudioSource.isPlaying)
            {
                deathAudioSource.PlayOneShot(deathSound);
            }
        }
	}

	//Pick up behavior
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "BlueGem") 
		{
			//When a player hits a 'blue gem'

			gameManager.score+= gameManager.blueGemPoints;
			other.gameObject.SetActive (false);
			Instantiate (blueGemParticles, particleSpawn.transform.position, Quaternion.identity);
            gemAudioSource.PlayOneShot(blueGemSound);
		} 
		else if( other.gameObject.tag == "RedGem")
		{
			//When a player hits a 'red gem'

			gameManager.score+= gameManager.redGemPoints;
			other.gameObject.SetActive (false);
			Instantiate (redGemParticles, particleSpawn.transform.position, Quaternion.identity);
            gemAudioSource.PlayOneShot(redGemSound);
        }
	}
		
	//Checks to see if the player is grounded
	private bool Grounded()
	{	
		Collider[] colliders = Physics.OverlapSphere (contactPoint.position, 0.3f, groundLayer);

		for (int i = 0; i < colliders.Length; i++) 
		{
			if (colliders[i].gameObject != gameObject) 
			{
				return true;
			}
		}
		return false;
	}	
}
