using UnityEngine;
using System.Collections;

class TileFactory {
	public GameObject[] floorPrefabs;
	public GameObject[] wallPrefabs;
	public GameObject exitPrefab;

	public TileFactory (GameObject[] floors, GameObject[] walls, GameObject exit) {
		floorPrefabs = floors;
		wallPrefabs = walls;
		exitPrefab = exit;
	}

	public Tile makeTile(string type) {
		GameObject[] pref;
		GameObject randPref;

		if (type.Equals("W")) {
			pref = wallPrefabs;
		} 
		else if (type.Equals("O")) {
			pref = Random.Range(0,2).Equals(1) ? wallPrefabs : floorPrefabs;
		}
		else if (type.Equals("E")) {
			pref = floorPrefabs;
		}
		else {
			pref = floorPrefabs;
		}

		randPref = getPref (pref);
		return new Tile(randPref);
	}

	private GameObject getPref(GameObject[] pref) {
		int randIdx = Random.Range (0, pref.Length);
		GameObject randPref = pref [randIdx];
		return randPref;
	}
}
