using UnityEngine;
using System.Collections;

public class FloorPrefabs : MonoBehaviour {
	[SerializeField]
	private static GameObject[] floorPrefabs;

	public FloorPrefabs() {
	}

	public GameObject[] getPrefabs() {
		return floorPrefabs;
	}
}
