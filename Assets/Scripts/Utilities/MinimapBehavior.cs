using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RogueGem.Utilities;

public class MinimapBehavior : MonoBehaviour {
	private Board board;

	void Start () {
		board = Board.Instance;
		EventBehaviour.StartListening(GameEvent.MoveEnemy, revealTiles);
	}

	public void revealTiles() {
		GameObject[] walls = GameObject.FindGameObjectsWithTag ("Obstacle");
		foreach (GameObject wall in walls) {
			if (WorldController.IsTileInSight(wall.transform.position)) {
				Debug.Log ("In sight: " + wall.transform.Find("Minimap").transform.position);
				wall.transform.Find("Minimap").transform.GetComponent<SpriteRenderer> ().enabled = true;
			}

		}

	}


}
