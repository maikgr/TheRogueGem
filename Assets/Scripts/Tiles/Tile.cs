using UnityEngine;
using System.Collections;

public class Tile {
	protected GameObject prefab;

	public Tile (GameObject pref) {
		prefab = pref;
	}

	public GameObject getPrefab() {
		return prefab;
	}
}
