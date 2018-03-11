using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;  

public class LevelManager : MonoBehaviour {
	private Board board;
	private TileFactory tf;
	private RoomFactory rf;
	private Transform boardHolder;

	private int min = 0, max = 3;
	private delegate bool endCondition(int[] p);
	private int right = 0, left = 1, up = 2, down = 3;

	// Use this for initialization
	void Start () {
		boardHolder = new GameObject ("Tiles").transform;
		board = Board.Instance;
		tf = GetComponent<TileFactory>();
		rf = GetComponent<RoomFactory>();
		LoadTemplates ();
		LoadLevel ();
		renderLevel ();
	}

	public void LoadTemplates() {
		DirectoryInfo dir = new DirectoryInfo("Assets/RoomTemplates");
		FileInfo[] info = dir.GetFiles("*.txt");
		StreamReader reader;
		RoomTemplate newTemplate;

		string[,] rt; string fileName;
		foreach (FileInfo f in info) {
			rt = new string[8, 8];
			fileName = f.Name;
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
			if (exits.Equals("0")) { // Non-essential room
				roomName = "0";
				rf.addRoomTemplateToMap (roomName, newTemplate);
			}
			else if (exits.Equals("4")) { // Start/end room
				roomName = "4";
				rf.addRoomTemplateToMap (roomName, newTemplate);
			} 
			else {
				for (int i=0; i< exits.Length - 1; i++) {
					for (int j=i+1; j< exits.Length; j++) {
						roomName = exits [i].ToString () + exits [j].ToString ();
						rf.addRoomTemplateToMap (roomName, newTemplate);
					}
				}
			}
		}
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
	}

	public void renderLevel() {
		int cols = 8 * 4,
		rows = 8 * 4;

		int roomX = 0, roomY = 0;

		Room roomCache = board.getRoom (roomY, roomX);
		if (roomCache == null)
			roomCache = rf.getRoom ();
		
		string tile;
		for (int i = 0; i < rows+2; i++) {
			for (int j = 0; j < cols+2; j++) {
				if (i == 0 || i == rows + 1 || j == 0 || j == cols + 1) {
					tile = "W";
				}
				else {
					tile = roomCache.getTile (j%8, i%8);
				}
				GameObject goTile = Instantiate (tf.makeTile(tile).getPrefab (), new Vector2 (j, i), Quaternion.identity) as GameObject;
				board.addTile (j, i, goTile);
                board.addNodes(tile != "W", new Vector2(j, i));
				goTile.transform.SetParent (boardHolder);

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

	private int getDirection(char c) {
		if (c == 'r') {
			return 0;
		} else if (c == 'l') {
			return 1;
		} else if (c == 'u') {
			return 2;
		} else {
			return 3;
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
