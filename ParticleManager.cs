// ParticleManager.cs
// Author:
//       Alexis Barta <alexisbarta@outlook.com>
// Copyright (c) 2018 SerpentWorks Games
// Description: Simple script to manage particle systems the player encounters with the pick ups
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

	private ParticleSystem ps;

	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!ps.isPlaying) {
			Destroy (gameObject);
		}
	}
}
