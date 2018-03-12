using System.Collections;
using System.Collections.Generic;
using RogueGem.Utilities;
using UnityEngine;

public class Exit : MonoBehaviour {

	void Start() {
		GetComponent<BoxCollider2D> ().isTrigger = true;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		GetComponent<BoxCollider2D>().isTrigger = false;
		EventBehaviour.TriggerEvent (GameEvent.NextLevel);
	}
}
