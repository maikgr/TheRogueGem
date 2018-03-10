using UnityEngine;
using System.Collections;
using System.IO;  

public class RoomTestClient : MonoBehaviour {
	TileFactory tf;
	RoomFactory rf;

	public Tile[,] sToTile(string [,] s) {
		Tile[,] tiles = new Tile[8, 8];
		for (int i=0; i<8; i++) {
			for (int j=0; j<8; j++) {
				tiles[7-i,j] = tf.makeTile(s[i,j]);
			}
		}
		return tiles;
	}
	// Use this for initialization
	void Start () {
		rf = GetComponent<RoomFactory>();
		tf = GetComponent<TileFactory>();
		DirectoryInfo dir = new DirectoryInfo("Assets/RoomTemplates");
		FileInfo[] info = dir.GetFiles("023_1.txt");
		StreamReader reader;
		RoomTemplate newTemplate;

		foreach (FileInfo f in info) {
			string[,] rt = new string[8, 8];
			string fileName = f.Name;
			Debug.Log (" " + fileName);
			reader = f.OpenText();
			string text = reader.ReadLine();

			int k = 7, l = 0;
			while (text != null) { 
				foreach (char c in text) {
					rt [k, l] = c.ToString ();
					l++;
				}
				l = 0;
				k--;
				text = reader.ReadLine();
			}

			newTemplate = new RoomTemplate (rt);

			string exits = fileName.Substring (0, fileName.IndexOf ("_")), roomName;
			for (int i=0; i< exits.Length - 1; i++) {
				for (int j=i+1; j< exits.Length; j++) {
					roomName = exits [i].ToString () + exits [j].ToString ();
					rf.addRoomTemplateToMap (roomName, newTemplate);
				}
			}
		}


		Room room1 = rf.getRoom (0,2);
		for (int i=0; i<8; i++) {
			for (int j=0; j<8; j++) {
				string tile = room1.getTile (i, j);
				Instantiate(tf.makeTile(tile).getPrefab(), new Vector2 (i, j), Quaternion.identity);
			}
		}
	}

}
