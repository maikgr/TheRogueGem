using UnityEngine;
using System.Collections;

public class TileFactory : MonoBehaviour {
	[SerializeField]
	public GameObject[] floorPrefabs;
	public GameObject[] wallPrefabs;

	public Tile makeTile(string type) {
		GameObject[] pref;
		GameObject randPref;
		if (type.Equals("floor")) {
			pref = floorPrefabs;
			randPref = getPref (pref);
			return new FloorTile (randPref);

		} else {
			pref = wallPrefabs;
			randPref = getPref (pref);
			return new WallTile(randPref);
		}
	}

	private GameObject getPref(GameObject[] pref) {
		int randIdx = Random.Range (0, pref.Length);
		GameObject randPref = pref [randIdx];
		return randPref;
	}
}
