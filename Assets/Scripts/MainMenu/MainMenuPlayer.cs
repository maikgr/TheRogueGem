using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Horizontal")) {
			int move = ((int) Input.GetAxisRaw("Horizontal")) * 35;
			float newXPos = transform.position.x + move;
			if (newXPos >= 770 && newXPos <= 840) 
				transform.position = new Vector2 (newXPos, 730);
		}
	}
}