using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RogueGem.Utilities;

public class MinimapBehavior : MonoBehaviour {

	void Start () {
		revealTiles ();
		EventBehaviour.StartListening(GameEvent.PlayerTurnEnd, revealTiles);
	}

	public void revealTiles() {
		GameObject[] walls = GameObject.FindGameObjectsWithTag ("Obstacle");
		foreach (GameObject wall in walls) {
			if (WorldController.IsTileInSight(wall.transform.position)) {
				wall.transform.Find("Minimap").transform.GetComponent<SpriteRenderer> ().enabled = true;
			}

		}

	}


}
