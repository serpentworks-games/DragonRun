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
	private AudioSource musicSource;
    private AudioSource gameOverSource;
    public AudioClip music;
    public AudioClip gameOverSound;
    public AudioSource[] audioSources;


	private static UIManager instance;
	private int highScore;
    private float startMusicVol;
    private float startGOVol;

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
        musicSource = GameObject.Find("Music").GetComponent<AudioSource>();
        gameOverSource = GameObject.Find("GameOver").GetComponent<AudioSource>();
        startMusicVol = musicSource.volume;

        audioSources = FindObjectsOfType<AudioSource>();

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
            for (int i = 0; i < audioSources.Length; i++)
            {
                audioSources[i].mute = value;
            }
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

        musicSource.volume = Mathf.Lerp(musicSource.volume, 0.05f, Time.deltaTime);
        if (!gameOverSource.isPlaying)
        {
            gameOverSource.PlayOneShot(gameOverSound);
        }

		highScore = PlayerPrefs.GetInt ("HighScore",0);
		if(gameManager.score > highScore)
		{
			PlayerPrefs.SetInt ("HighScore", (int)gameManager.score);
		}
		
        gameOverScoreText.text = Mathf.Round (gameManager.score).ToString();
	    highScoreText.text = PlayerPrefs.GetInt ("HighScore", 0).ToString ();
        gameOverSource.gameObject.SetActive(false);

    }

    void StartMenu()
    {
        resetGameCanvas.SetActive(false);
        scoreCanvas.SetActive(false);
        mainUICanvas.SetActive(true);
        playButton.SetActive(true);
        resetButton.SetActive(false);
        buttonPanel.SetActive(true);
        gameOverSource.gameObject.SetActive(true);

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
        gameOverSource.gameObject.SetActive(true);
        StartMenu();
        musicSource.volume = 0.55f;
    }
}
