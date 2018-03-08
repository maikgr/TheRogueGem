using UnityEngine;
using System.Collections;

public class RoomTestClient : MonoBehaviour {
	string f = "floor", w = "wall";
	TileFactory tf;

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
		tf = GetComponent<TileFactory>();

		string[,] s0 = new string[8,8] {
			{ f, f, f, f, f, f, f, f },
			{ w, w, w, w, w, w, w, w },
			{ f, f, f, f, f, f, f, f },
			{ w, w, w, w, w, w, w, w },			
			{ f, f, f, f, f, f, f, f },
			{ w, w, w, w, w, w, w, w },
			{ f, f, f, f, f, f, f, f },
			{ w, w, w, w, w, w, w, w }		
		};

		RoomTemplate a0 = new RoomTemplate(sToTile(s0));

		string[,] s10 = new string[8,8] {
			{ f, f, f, f, f, f, f, f },
			{ w, f, w, w, w, w, w, w },
			{ f, f, f, f, f, f, f, w },
			{ w, w, w, w, f, w, w, w },			
			{ f, f, f, f, f, f, f, w },
			{ w, w, w, w, w, w, w, w },
			{ w, w, w, f, f, f, f, w },
			{ w, w, w, w, w, w, w, w }		
		};

		RoomTemplate a10 = new RoomTemplate(sToTile(s10)); 

		string[,] s20 = new string[8,8] {
			{ w, f, f, f, f, f, f, f },
			{ w, w, w, w, w, w, f, w },
			{ w, f, f, f, f, f, f, w },
			{ w, w, w, w, f, w, w, w },			
			{ w, f, f, f, f, f, f, w },
			{ w, w, f, w, w, w, w, w },
			{ f, f, f, f, f, f, f, w },
			{ f, w, w, w, w, w, w, w }		
		};

		RoomTemplate a20 = new RoomTemplate(sToTile(s20)); 

		string[,] s01 = new string[8,8] {
			{ w, w, w, w, w, w, w, w },
			{ w, w, w, w, w, w, w, w },
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },		
			{ f, f, f, f, f, f, f, f },
			{ w, w, w, w, w, w, w, w },
			{ w, w, w, w, w, w, w, w },
			{ w, w, w, w, w, w, w, w }		
		};

		RoomTemplate a01 = new RoomTemplate(sToTile(s01)); 

		string[,] s02 = new string[8,8] {
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },		
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },
			{ w, w, w, w, w, w, w, w }		
		};

		RoomTemplate a02 = new RoomTemplate(sToTile(s02)); 

		string[,] s03 = new string[8,8] {
			{ w, w, w, w, w, w, w, w },
			{ w, w, w, w, w, w, w, w },
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },		
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f }	
		};

		RoomTemplate a03 = new RoomTemplate(sToTile(s03)); 

		string[,] s23 = new string[8,8] {
			{ w, f, f, f, f, f, w, w },
			{ w, f, f, f, f, f, w, w },
			{ w, f, f, f, f, f, w, w },
			{ w, f, f, f, f, f, w, w },
			{ w, f, f, f, f, f, w, w },
			{ w, f, f, f, f, f, w, w },
			{ w, f, f, f, f, f, w, w },
			{ w, w, f, f, f, f, w, w },	
		};

		RoomTemplate a23 = new RoomTemplate(sToTile(s23)); 

		string[,] s5 = new string[8,8] {
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f }
		};

		RoomTemplate a5 = new RoomTemplate(sToTile(s5)); 

		string[,] s4 = new string[8,8] {
			{ w, w, w, w, w, w, w, w },
			{ w, f, f, f, f, f, f, w },
			{ w, f, f, f, f, f, f, w },
			{ w, f, f, f, f, f, f, w },
			{ w, f, f, f, f, f, f, w },
			{ w, f, f, f, f, f, f, w },
			{ w, f, f, f, f, f, f, w },
			{ w, w, w, w, w, w, w, w }
		};

		RoomTemplate a4 = new RoomTemplate(sToTile(s4)); 

		RoomType r0 = new RoomType (a0),

		r01 = new RoomType (a01),

		r02 = new RoomType (a02),

		r03 = new RoomType (a03),

		r23 = new RoomType (a23),

		r5 = new RoomType (a5),

		r4 = new RoomType (a4);


		//		r0.addTemplate (a10);
		//		r0.addTemplate (a20);



		RoomFactory rf = GetComponent<RoomFactory>();
		rf.addToRoomMap ("0", r0);

		rf.addToRoomMap ("01", r01);

		rf.addToRoomMap ("02", r02);

		rf.addToRoomMap ("03", r03);

		rf.addToRoomMap ("12", r23); //!

		rf.addToRoomMap ("13", r23); //!

		rf.addToRoomMap ("23", r23);

		rf.addToRoomMap ("5", r5);
		rf.addToRoomMap ("4", r4);

		Room room1 = rf.getRoom (0, 0);
		for (int i=0; i<8; i++) {
			for (int j=0; j<8; j++) {
				Tile tile = room1.getTile (i, j);
				Instantiate(tile.getPrefab(), new Vector2 (i, j), Quaternion.identity);
			}
		}
	}

}
