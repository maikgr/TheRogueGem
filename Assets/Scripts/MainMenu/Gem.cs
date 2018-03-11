using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("triggered");
	}
}
