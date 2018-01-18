/*/// ScrollingText.cs
// Author:
//       Alexis Barta <alexisbarta@outlook.com>
// Copyright (c) 2018 SerpentWorks Games
// Description: Scrolling point text when a player passes over a gem
// May or may not be staying, as it seems to have broken
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingText : MonoBehaviour {

	private float speed;
	private Vector3 dir;
	private float fadeTime;


	// Use this for initialization
	void Start () {
		transform.LookAt (2 * transform.position - Camera.main.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		float translation = speed * Time.deltaTime;

		transform.Translate (dir * translation);

		transform.LookAt (transform.position.normalized - Camera.main.transform.position.normalized);
	}

	public void Initialize(float speed, Vector3 dir, float fadeTime){
		this.speed = speed;
		this.dir = dir;
		this.fadeTime = fadeTime;

		StartCoroutine (FadeText ());

	}

	private IEnumerator FadeText(){

		float startAlpha = GetComponent<Text> ().color.a;
		float rate = 1.0f / fadeTime;
		float progress = 0.0f;

		while (progress < 1.0) {
			Color tmpColor = GetComponent<Text> ().color;

			GetComponent<Text> ().color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, Mathf.Lerp (startAlpha, 0, progress));

			progress += rate * Time.deltaTime;

			yield return null;
		}

		Destroy (gameObject);
	}
}
*/