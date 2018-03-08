using UnityEngine;
using System.Collections;

public class TestClient : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TileFactory factory = GetComponent<TileFactory>();
		for (int i = 0; i < 10; i++) {
			for (int j = 0; j < 10; j++) {
				Tile tile = factory.makeTile ("wall");
				Instantiate(tile.getPrefab(), new Vector2 (i, j), Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
