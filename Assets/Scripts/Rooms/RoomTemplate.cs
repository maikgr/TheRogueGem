using UnityEngine;
using System.Collections;

public class RoomTemplate {
	const int roomX = 8;
	const int roomY = 8;

	private string[,] tiles;

	public RoomTemplate(string[,] tiles) {
		this.tiles = tiles;
	}

	public string getTile(int x, int y) {
		return tiles [y, x];
	}
}
