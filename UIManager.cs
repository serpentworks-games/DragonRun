using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour {

	public GameManager gameManager;
	public Player player;
	public GameObject resetButton;
	public Text scoreText;
	public GameObject scrollingTextPanel;

	public Text gameOverScoreText;
	public Text highScoreText;

	public GameObject floatingText;
	public float speed;
	public Vector3 dir;
	public float fadeTime;

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
		resetButton.SetActive (false);
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
		resetButton.SetActive (false);
	}

	void UpdateScoreText(){
		scoreText.text = "Score: " + Mathf.Round (gameManager.score);
	}

	public void CreateFloatingText(Vector3 position, string text){
			GameObject sct = Instantiate (floatingText, position, Quaternion.identity);

			sct.transform.SetParent (scrollingTextPanel.transform);
			sct.GetComponent<RectTransform> ().localScale = new Vector3 (1,1,1);
			sct.GetComponent<ScrollingText> ().Initialize (speed, dir, fadeTime);
			sct.GetComponent<Text> ().text = text;

	}

	void GameOver(){
		highScore = PlayerPrefs.GetInt ("HighScore",0);
		if(gameManager.score > highScore){
			
			PlayerPrefs.SetInt ("HighScore", (int)gameManager.score);
		}
		resetButton.SetActive (true);
		gameOverScoreText.text = Mathf.Round (gameManager.score).ToString();
		highScoreText.text = PlayerPrefs.GetInt ("HighScore", 0).ToString ();
	}
}
