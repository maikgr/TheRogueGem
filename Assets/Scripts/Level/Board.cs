using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {
	const int roomX = 4, roomY = 4;
	GameObject[,] tiles = new GameObject[34, 34];
	Room[,] rooms = new Room[roomX, roomY];
	private static Board _instance;
	public static Board Instance { get { return _instance; } }

	void Start () {
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		} else {
			_instance = this;
		}
	}

	public void addRoom(int x, int y, Room room) {
		rooms [x, y] = room;
	}

	public Room getRoom(int x, int y) {
		return rooms [x, y];
	}

	public bool isEmpty(int row, int col) {
		return rooms [row, col] == null;
	}

	public void addTile(int x, int y, GameObject tile) {
		this.tiles[x, y] = tile;
	}

	public GameObject getTile(int x, int y) {
		return tiles [x, y];
		
	}

	public GameObject getTileChild(int x, int y) {
		return tiles [x,y];
	}
}
