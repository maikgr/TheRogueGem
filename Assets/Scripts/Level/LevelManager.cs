using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
	private Board board = new Board();
	private TileFactory tf;
	private RoomFactory rf;

	private int min = 0, max = 3;
	private delegate bool endCondition(int[] p);
	private int right = 0, left = 1, up = 2, down = 3;

	private int gcol, grow;

	// Use this for initialization
	void Start () {
		tf = GetComponent<TileFactory>();
		rf = GetComponent<RoomFactory>();
		LoadTemplates ();
		LoadLevel ();
		renderLevel ();
	}

	public Tile[,] sToTile(string [,] s) {
		Tile[,] tiles = new Tile[8, 8];
		for (int i=0; i<8; i++) {
			for (int j=0; j<8; j++) {
				tiles[7-i,j] = tf.makeTile(s[i,j]);
			}
		}
		return tiles;
	}

	public void LoadTemplates() {
		string f = "floor", w = "wall";

		string[,] s0 = new string[8,8] {
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f }	
		};

		RoomTemplate a0 = new RoomTemplate(sToTile(s0));

		string[,] slr = new string[8,8] {
			{ f, f, f, f, f, f, f, f },
			{ f, w, w, w, w, w, w, f },
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f },
			{ f, w, w, w, w, w, w, f },
			{ f, f, f, f, f, f, f, f }
		};

		RoomTemplate alr = new RoomTemplate(sToTile(slr)); 

		string[,] sud = new string[8,8] {
			{ f, f, f, f, f, f, f, f },
			{ f, w, f, f, f, f, w, f },
			{ f, w, f, f, f, f, w, f },
			{ f, w, f, f, f, f, w, f },
			{ f, w, f, f, f, f, w, f },
			{ f, w, f, f, f, f, w, f },
			{ f, w, f, f, f, f, w, f },
			{ f, f, f, f, f, f, f, f },		
		};

		RoomTemplate aud = new RoomTemplate(sToTile(sud)); 

		string[,] sur = new string[8,8] {
			{ f, f, f, f, f, f, f, f },
			{ f, w, f, f, f, f, f, f },
			{ f, w, f, f, f, f, f, f },
			{ f, w, f, f, f, f, f, f },		
			{ f, w, f, f, f, f, f, f },
			{ f, w, f, f, f, f, f, f },
			{ f, w, w, w, w, w, w, f },
			{ f, f, f, f, f, f, f, f }		
		};

		RoomTemplate aur = new RoomTemplate(sToTile(sur)); 

		string[,] sul = new string[8,8] {
			{ f, f, f, f, f, f, f, f },
			{ f, f, f, f, f, f, w, f },
			{ f, f, f, f, f, f, w, f },
			{ f, f, f, f, f, f, w, f },		
			{ f, f, f, f, f, f, w, f },
			{ f, f, f, f, f, f, w, f },
			{ f, w, w, w, w, w, w, f },
			{ f, f, f, f, f, f, f, f }		
		};

		RoomTemplate aul = new RoomTemplate(sToTile(sul)); 

		string[,] sdl = new string[8,8] {
			{ f, f, f, f, f, f, f, f },
			{ f, w, w, w, w, w, w, f },
			{ f, f, f, f, f, f, w, f },
			{ f, f, f, f, f, f, w, f },		
			{ f, f, f, f, f, f, w, f },
			{ f, f, f, f, f, f, w, f },
			{ f, f, f, f, f, f, w, f },
			{ f, f, f, f, f, f, f, f }	
		};

		RoomTemplate adl = new RoomTemplate(sToTile(sdl)); 

		string[,] sdr = new string[8,8] {
			{ f, f, f, f, f, f, f, f },
			{ f, w, w, w, w, w, w, f },
			{ f, w, f, f, f, f, f, f },
			{ f, w, f, f, f, f, f, f },
			{ f, w, f, f, f, f, f, f },
			{ f, w, f, f, f, f, f, f },
			{ f, w, f, f, f, f, f, f },
			{ f, f, f, f, f, f, f, f }
		};

		RoomTemplate adr = new RoomTemplate(sToTile(sdr)); 

		string[,] s4 = new string[8,8] {
			{ w, w, w, w, w, w, w, w },
			{ w, w, w, w, w, w, w, w },
			{ w, w, w, w, w, w, w, w },
			{ w, w, w, w, w, w, w, w },
			{ w, w, w, w, w, w, w, w },
			{ w, w, w, w, w, w, w, w },
			{ w, w, w, w, w, w, w, w },
			{ w, w, w, w, w, w, w, w }
		};

		RoomTemplate a4 = new RoomTemplate(sToTile(s4)); 

		RoomType r0 = new RoomType (a0),

		rlr = new RoomType (alr),

		rud = new RoomType (aud),

		rul = new RoomType (aul),

		rur = new RoomType (aur),

		rdr = new RoomType (adr),

		rdl = new RoomType (adl),

		r4 = new RoomType (a4);



//		r0.addTemplate (a10);
//		r0.addTemplate (a20);



		RoomFactory rf = GetComponent<RoomFactory>();
		rf.addToRoomMap ("0", r0);
		rf.addToRoomMap ("4", r4);

		rf.addToRoomMap ("01", rlr);

		rf.addToRoomMap ("02", rur);

		rf.addToRoomMap ("03", rdr);

		rf.addToRoomMap ("12", rul); //!

		rf.addToRoomMap ("13", rdl); //!

		rf.addToRoomMap ("23", rud);
	}
	
	public void LoadLevel() {

		endCondition rmin = new endCondition (rowMin), rmax = new endCondition (rowMax),
		cmin = new endCondition (colMin), cmax = new endCondition (colMax);

		IDictionary<string, endCondition> conditionMap = new Dictionary<string, endCondition> () {
			{ "row"+min, rmin },
			{ "row"+max, rmax },
			{ "col"+min, cmin },
			{ "col"+max, cmax },
		};

		IDictionary<string, int> destDir = new Dictionary<string, int> () {
			{ "row"+min, up },
			{ "row"+max, down },
			{ "col"+min, right },
			{ "col"+max, left },
		};
			
		List<int> leftRight = new List<int>(){ left, left, right, right },
		upDown = new List<int>(){ up, up, down, down };

		IDictionary<string, List<int>> dirMap = new Dictionary<string, List<int>>(){
			{"row", leftRight},
			{"col", upDown},
		};

		string[] axis = {"col", "row"};
		string type = axis[Random.Range (0, axis.Length)];
		int[] coords = new int[2], newCoords;

		int coordOne = Random.Range(0,2).Equals(1) ? min : max,
			coordTwo = Random.Range (min, max);

		if (type.Equals ("row")) {
			coords [0] = coordOne;
			coords [1] = coordTwo;
		} else {
			coords [0] = coordTwo;
			coords [1] = coordOne;
		}

		int entryDir = 4, exitDir;
		endCondition notDone = conditionMap [type + coordOne];
		List<int> directions = dirMap[type]; 
		directions.Add(destDir[type+coordOne]);
		Room newRoom;
		while (notDone (coords)) {
			int idx = Random.Range (0, directions.Count);
			exitDir = directions [idx];
			newCoords = getNewCoords (exitDir, coords);
			if (!isValidMove (newCoords)) {
				exitDir = destDir [type + coordOne];
				newCoords = getNewCoords (exitDir, coords);
			}
			newRoom = rf.getRoom (entryDir, exitDir);
			board.addRoom (coords[0], coords[1], newRoom);
			entryDir = getOpposite (exitDir);
			coords = newCoords;
		}

		Room endRoom = rf.getRoom (4, 4);
		board.addRoom (coords [0], coords [1], endRoom);
		grow = coords [1];
		gcol = coords [0];
	}

	public void renderLevel() {
		int cols = 8 * 4,
		rows = 8 * 4;

		int roomX = 0, roomY = 0;

		Room roomCache = board.getRoom (roomY, roomX);
		if (roomCache == null)
			roomCache = rf.getRoom ();
		
		Tile tile;
		for (int i = 0; i < rows+2; i++) {
			for (int j = 0; j < cols+2; j++) {
				if (i == 0 || i == rows + 1 || j == 0 || j == cols + 1) {
					tile = tf.makeTile ("floor");
				}
				else {
					tile = roomCache.getTile (j%8, i%8);
				}
				Instantiate (tile.getPrefab (), new Vector2 (j, i), Quaternion.identity);

				if (j == 0) {
					roomX = 0;

					roomCache = board.getRoom (roomY, roomX);
					if (roomCache == null)
						roomCache = rf.getRoom ();
					
				} else if (j % 8 == 0 && j!= 32 && i!=0) {
					roomX++;

					roomCache = board.getRoom (roomY, roomX);
					if (roomCache == null)
						roomCache = rf.getRoom ();
				}
					
				if ((i-1) % 8 == 0 && i != 1 && i!= 33 && j == 0) {
					roomY++;

					roomCache = board.getRoom (roomY, roomX);
					if (roomCache == null)
						roomCache = rf.getRoom ();
				}
			}
		}
	}

	private bool isValidMove(int[] c) {
		return isValid (c [0]) && isValid (c [1]) && board.isEmpty (c[0], c[1]);
	}

	private bool isValid(int p) {
		return p <= max && p >= min;
	}

	private int getOpposite (int dir) {
		if (dir == left)
			return right;
		else if (dir == right)
			return left;
		else if (dir == up)
			return down;
		else
			return up;
	}

	private int[] getNewCoords (int dir, int[] coords) {
		int[] newCoords = new int[coords.Length] ;
		newCoords [0] = coords [0];
		newCoords [1] = coords [1];
		if (dir == left) {
			newCoords [1] -= 1;
		} else if (dir == right) {
			newCoords [1] += 1;
		} else if (dir == down) {
			newCoords[0] -= 1;
		} else {
			newCoords [0] += 1;
		}
		return newCoords;
	}

	private bool rowMin(int[] c) {
		return c[0] < max;
	}

	private bool rowMax(int[] c) {
		return c [0] > min;
	}

	private bool colMin(int[] c) {
		return c [1] < max;
	}

	private bool colMax(int[] c) {
		return c [1] > min;
	}
}
