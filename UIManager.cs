// UIManager.cs
// Author:
//       Alexis Barta <alexisbarta@outlook.com>
// Copyright (c) 2018 SerpentWorks Games
// Description: Manages the UI and all menus there in
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour {
	
	public Text scoreText;
	public Text gameOverScoreText;
	public Text highScoreText;

	public GameObject resetGameCanvas;
	public GameObject scoreCanvas;
	public GameObject mainUICanvas;
	public GameObject tutorialText;

	public Player player;
	public GameManager gameManager;
	public AudioListener gameAudio;


	private static UIManager instance;
	private int highScore;

	public static UIManager Instance {
		get {
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<UIManager> ();
			}
			return instance;
		}
	}

	// Use this for initialization
	void Start () {
		resetGameCanvas.SetActive (false);
		scoreCanvas.SetActive (false);
		mainUICanvas.SetActive (true);
		tutorialText.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		UpdateScoreText ();
		if(player.isDead){
			GameOver ();

		}
	}

	public void ResetGame(){
		SceneManager.LoadScene ("DragonRun");
		resetGameCanvas.SetActive (false);
		scoreCanvas.SetActive (false);
		mainUICanvas.SetActive (true);
		gameManager.gameStarted = false;

	}

	public void ExitGame(){
		Application.Quit ();
	}

	public void HighScoreList(){
		//This will show a panel of highscores eventually
		Debug.Log ("There is nothing here right now!");
	}

	public void PlayGame(){
		gameManager.gameStarted = true;
		scoreCanvas.SetActive (true);
		mainUICanvas.SetActive (false);
		tutorialText.SetActive (true);
		gameManager.gameStarted = true;

	}

	public void DisableAudio(bool value){
		gameAudio.enabled = value;

	}

	public void ClearTutorialText(){
		tutorialText.GetComponent<Animator> ().SetBool ("fadeOut", true);
		TutorialTextFadeOut ();
	}
	void UpdateScoreText(){
		scoreText.text = Mathf.Round (gameManager.score).ToString ();
	}
		
	void GameOver(){
		mainUICanvas.SetActive (false);

		highScore = PlayerPrefs.GetInt ("HighScore",0);
		if(gameManager.score > highScore){
			
			PlayerPrefs.SetInt ("HighScore", (int)gameManager.score);
		}
		resetGameCanvas.SetActive (true);
		gameOverScoreText.text = Mathf.Round (gameManager.score).ToString();
		highScoreText.text = PlayerPrefs.GetInt ("HighScore", 0).ToString ();
	}
		
	IEnumerator TutorialTextFadeOut(){
		yield return new WaitForSeconds (1.3f);
		tutorialText.SetActive (false);
		tutorialText.GetComponent<Text> ().color = new Color(255,255,255,255);
	}
}
