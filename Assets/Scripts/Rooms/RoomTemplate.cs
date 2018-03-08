using UnityEngine;
using System.Collections;

public class RoomTemplate {
	const int roomX = 8;
	const int roomY = 8;

	private Tile[,] tiles;

	public RoomTemplate(Tile[,] tiles) {
		this.tiles = tiles;
	}

	public Tile getTile(int x, int y) {
		return tiles [y, x];
	}
}
