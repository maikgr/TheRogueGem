using System.Collections;
using System.Collections.Generic;
using RogueGem.Utilities;
using UnityEngine;
using RogueGem.Player;

public class Exit : MonoBehaviour {

	void Start() {
		GetComponent<BoxCollider2D> ().isTrigger = true;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		GetComponent<BoxCollider2D>().isTrigger = false;
        if (other.tag.Equals("Player")) {
            StartCoroutine(GoToNextLevel(other.GetComponent<PlayerBehaviour>()));
        }
	}

    IEnumerator GoToNextLevel(PlayerBehaviour player) {
        while (player.IsAnimating()) {
            yield return null;
        }
        EventBehaviour.TriggerEvent(GameEvent.NextLevel);
    }

}
