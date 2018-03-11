using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RogueGem.Utilities;
using System.Linq;

public class MinimapBehavior : MonoBehaviour {

	void Start () {
		revealTiles ();
		EventBehaviour.StartListening(GameEvent.PlayerTurnEnd, revealTiles);
	}

	public void revealTiles() {
		var walls = GameObject.FindGameObjectsWithTag ("Obstacle").Concat(GameObject.FindGameObjectsWithTag ("Exit"));
		foreach (GameObject wall in walls) {
			if (WorldController.IsTileInSight(wall.transform.position)) {
				wall.transform.Find("Minimap").transform.GetComponent<SpriteRenderer> ().enabled = true;
			}

		}

	}


}
