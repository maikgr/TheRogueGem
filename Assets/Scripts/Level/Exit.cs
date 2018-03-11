using System.Collections;
using System.Collections.Generic;
using RogueGem.Utilities;
using UnityEngine;

public class Exit : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Exit");
		EventBehaviour.TriggerEvent (GameEvent.NextLevel);
	}
}
