﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gem : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("triggered");
		SceneManager.LoadScene ("Intro");

	}
}
