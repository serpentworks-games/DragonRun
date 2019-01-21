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

public class UIManager : MonoBehaviour 
{
	
    [Header("Text Elements")]
	public Text scoreText;
	public Text gameOverScoreText;
	public Text highScoreText;

    [Header("Ui Elements")]
	public GameObject resetGameCanvas;
	public GameObject scoreCanvas;
	public GameObject mainUICanvas;
	public GameObject tutorialText;
    public GameObject buttonPanel;
    public GameObject playButton;
    public GameObject resetButton;

    [Header("Misc")]
	public Player player;
	public GameManager gameManager;
	public AudioListener gameAudio;


	private static UIManager instance;
	private int highScore;

	public static UIManager Instance 
	{
		get {
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<UIManager> ();
			}
			return instance;
		}
	}

	// Use this for initialization
	void Start () 
	{
        StartMenu();
    	}

	// Update is called once per frame
	void Update () 
	{
		UpdateScoreText ();
		if(player.isDead)
		{
			GameOver ();
		}
	}

	public void ResetGame()
	{

        StartCoroutine(ResettingGame());
    }

	public void ExitGame()
	{
		Application.Quit ();
	}

	public void PlayGame()
	{
		
        buttonPanel.GetComponent<Animator>().SetTrigger("Triggered");
        mainUICanvas.GetComponent<Animator>().SetTrigger("Triggered");

        StartCoroutine(MainMenuAnimation());

        gameManager.gameStarted = true;
        tutorialText.SetActive(true);
        tutorialText.GetComponent<Animator>().SetTrigger("fadeIn");
    }

	public void DisableAudio(bool value)
	{
		gameAudio.enabled = value;
	}

	public void ClearTutorialText()
	{
		tutorialText.GetComponent<Animator> ().SetTrigger("fadeOut");
		StartCoroutine(TutorialTextFadeOut ());
	}
	void UpdateScoreText()
	{
		scoreText.text = Mathf.Round (gameManager.score).ToString ();
	}
		
	void GameOver()
	{

        ResetMenu();

		highScore = PlayerPrefs.GetInt ("HighScore",0);
		if(gameManager.score > highScore)
		{
			
			PlayerPrefs.SetInt ("HighScore", (int)gameManager.score);
		}
		
        gameOverScoreText.text = Mathf.Round (gameManager.score).ToString();
	highScoreText.text = PlayerPrefs.GetInt ("HighScore", 0).ToString ();
	}

    void StartMenu()
    {
        resetGameCanvas.SetActive(false);
        scoreCanvas.SetActive(false);
        mainUICanvas.SetActive(true);
        playButton.SetActive(true);
        resetButton.SetActive(false);
        buttonPanel.SetActive(true);
    }

    void ResetMenu()
    {
        mainUICanvas.SetActive(false);
        resetGameCanvas.SetActive(true);
        scoreCanvas.SetActive(false);
        resetButton.SetActive(true);
        buttonPanel.SetActive(true);

        buttonPanel.GetComponent<Animator>().SetTrigger("TriggeredBack");
    }
		
	IEnumerator TutorialTextFadeOut()
	{
		yield return new WaitForSeconds (1.3f);
		tutorialText.SetActive (false);
		//tutorialText.GetComponent<Text> ().color = new Color(255,255,255,255);
	}

    IEnumerator MainMenuAnimation()
    {
        yield return new WaitForSeconds(2.5f);
        mainUICanvas.SetActive(false);
        buttonPanel.SetActive(false);
        scoreCanvas.SetActive(true);
        playButton.SetActive(false);
        resetButton.SetActive(false);
    }

    IEnumerator ResettingGame()
    {
        resetButton.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("DragonRun");

        gameManager.gameStarted = false;

        StartMenu();
    }
}
