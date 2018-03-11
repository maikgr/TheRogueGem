using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomFactory : MonoBehaviour {
	/* Constants
	 * 0: LEFT
	 * 1: RIGHT
	 * 2: TOP
	 * 3: BOTTOM
	 * /

	/* Room Types
	* 0: Rand
	* 01: Left, right
	* 02, 12: top, left, right
	* 03, 13: bot, left, right
	* 23: top, bot
	*/

	public IDictionary<string, RoomType> roomMap = new Dictionary<string, RoomType>();

	public Room getRoom() {
		RoomType roomType = roomMap ["0"];
		return new Room(roomType.getTemplate (), "0");
	}

	public Room getRoom(int entry, int exit) {
		string key;
		if (entry == 4 || exit == 4) {
			key = "4";
		} 
		else {
		 	key = directionToString (entry, exit);
		}
		RoomType roomType = roomMap [key];
		return new Room(roomType.getTemplate (), key);
	}

	public string directionToString(int entry, int exit) {
		if (entry == exit) {
			return "0";
		} else if (entry < exit) {
			return entry.ToString () + exit.ToString ();
		} else {
			return exit.ToString () + entry.ToString ();
		}
	}
		
	public void addRoomTemplateToMap(string typeName, RoomTemplate roomTemplate) {
		if (hasRoomName (typeName)) {
			roomMap [typeName].addTemplate (roomTemplate);
		} else {
			roomMap [typeName] = new RoomType ();
			roomMap [typeName].addTemplate (roomTemplate);
		}
	}

	public bool hasRoomName(string typeName) {
		return roomMap.ContainsKey (typeName);
	}
}
