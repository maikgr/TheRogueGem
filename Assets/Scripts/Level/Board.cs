using UnityEngine;
using System.Collections;

public class Board {
	const int roomX = 4, roomY = 4;
	Room[,] rooms = new Room[roomX, roomY];

	public void addRoom(int x, int y, Room room) {
		rooms [x, y] = room;
	}

	public Room getRoom(int x, int y) {
		return rooms [x, y];
	}

	public bool isEmpty(int row, int col) {
		return rooms [row, col] == null;
	}
}
