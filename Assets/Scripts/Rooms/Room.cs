using UnityEngine;
using System.Collections;

public class Room {
	private RoomTemplate layout;
	private string name;

	public Room(RoomTemplate template, string key) {
		this.layout = template;
		this.name = key;
	}

	public string getTile (int x, int y) {
		return layout.getTile (x, y);
	}

	public string getName() {
		return name;
	}
}
