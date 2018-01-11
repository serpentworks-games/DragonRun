using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public float fallDelay;
	public GameObject pickUp;
	// Use this for initialization

	void Awake(){
		pickUp = this.transform.GetChild (1).gameObject;
	}
	void Start () {
		fallDelay = TileManager.Instance.tileFallSpeed;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerExit(Collider other){

		if (other.tag == "Player") {
			if (this.transform.childCount > 0) {
				
				TileManager.Instance.SpawnTile ();
				StartCoroutine (FallDown ());
			}
		} else if (other.tag == "CleanUp"){
			if (this.transform.childCount > 0) {

				TileManager.Instance.SpawnTile ();
				StartCoroutine (FallDown ());
			}
		}
	}

	IEnumerator FallDown(){
		yield return new WaitForSeconds (fallDelay);
		this.GetComponent<Rigidbody> ().isKinematic = false;
		yield return new WaitForSeconds (0.5f);
		switch(gameObject.name)
		{
		case "LeftTile":
			TileManager.Instance.LeftTiles.Push (gameObject);
			gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			gameObject.SetActive (false);
		//	Debug.LogError ("Pushing to stack!");
				break;

		case "TopTile":
			TileManager.Instance.TopTiles.Push (gameObject);
			gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			gameObject.SetActive (false);
				break;

		case "RightTile":
			TileManager.Instance.RightTiles.Push (gameObject);
			gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			gameObject.SetActive (false);
			break;

		case "LeftTileLarge":
			TileManager.Instance.LeftTilesLarge.Push (gameObject);
			gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			gameObject.SetActive (false);
			break;

		case "TopTileLarge":
			TileManager.Instance.TopTilesLarge.Push (gameObject);
			gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			gameObject.SetActive (false);
			break;

		case "RightTileLarge":
			TileManager.Instance.RightTilesLarge.Push (gameObject);
			gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			gameObject.SetActive (false);
			break;
	
		}
	}

}
