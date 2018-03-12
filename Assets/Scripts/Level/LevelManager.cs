using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RogueGem.Utilities;
using System.IO;  

public class LevelManager : MonoBehaviour {
	private Board board;
	private TileFactory tf;
	private RoomFactory rf;
	private Transform boardHolder;
	private int currentLevel = 1;
	private GameObject bossDialogue;
	private IDictionary<int, GameObject[]> floorPrefabsMap = new Dictionary<int, GameObject[]>();
	private IDictionary<int, GameObject[]> wallPrefabsMap = new Dictionary<int, GameObject[]>();
	private GameObject player;

	public GameObject exitPrefab;

	public static int numRooms = 2;

	private int min = 0, max = numRooms - 1;
	private delegate bool endCondition(int[] p);
	private int right = 0, left = 1, up = 2, down = 3;

	// Use this for initialization
	void Start () {
		board = Board.Instance;
		player = GameObject.FindGameObjectWithTag ("Player");
		LoadPrefabs ();		
		rf = GetComponent<RoomFactory>();
		bossDialogue = GameObject.Find ("BossDialogue");
		LoadTemplates ();
		LoadLevel ();
		RenderLevel (currentLevel);
		//BossLevelPart1(10);
		EventBehaviour.StartListening (GameEvent.NextLevel, newLevel);
	}

	public void LoadPrefabs() {
		exitPrefab = Resources.Load<GameObject>("Prefabs/Floors/Exit");
		GameObject[] gFloors = Resources.LoadAll<GameObject>("Prefabs/Floors/Level1/Floor");
		GameObject[] gWalls = Resources.LoadAll<GameObject>("Prefabs/Floors/Level1/Wall");

		GameObject[] aFloors = Resources.LoadAll<GameObject>("Prefabs/Floors/Level2/Floor");
		GameObject[] aWalls = Resources.LoadAll<GameObject>("Prefabs/Floors/Level2/Wall");

		GameObject[] mFloors = Resources.LoadAll<GameObject>("Prefabs/Floors/Level3/Floor");
		GameObject[] mWalls = Resources.LoadAll<GameObject>("Prefabs/Floors/Level3/Wall");

		floorPrefabsMap [1] = gFloors;
		floorPrefabsMap [2] = aFloors;
		floorPrefabsMap [3] = mFloors;
		floorPrefabsMap [4] = mFloors;

		wallPrefabsMap [1] = gWalls;
		wallPrefabsMap [2] = aWalls;
		wallPrefabsMap [3] = mWalls;
		wallPrefabsMap [4] = mWalls;

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

	public void RenderLevel(int level) {
		boardHolder = new GameObject ("Tiles").transform;
		int levelType = (level-1)/3 + 1;
		tf = new TileFactory (floorPrefabsMap [levelType], wallPrefabsMap [levelType], exitPrefab);
		int cols = 8 * numRooms,
		rows = 8 * numRooms;

		int roomX = 0, roomY = 0;

		Room roomCache = board.getRoom (roomY, roomX);
		if (roomCache == null)
			roomCache = rf.getRoom ();
		
		string tile;
		int[,] specialCoords = new int[2, 2];
		int sInt = 0;
		for (int i = 0; i < rows+2; i++) {
			for (int j = 0; j < cols+2; j++) {
				if (i == 0 || i == rows + 1 || j == 0 || j == cols + 1) {
					tile = "W";
				}
				else {
					tile = roomCache.getTile (j%8, i%8);
				}

				if (tile.Equals("X")) { // Start and End tiles
					specialCoords [sInt, 0] = j;
					specialCoords [sInt, 1] = i;
					sInt++;
				}

				GameObject goTile = Instantiate (tf.makeTile(tile).getPrefab (), new Vector2 (j, i), Quaternion.identity) as GameObject;
				board.addTile (j, i, goTile);
                char tileLetter = goTile.gameObject.name[1];
                board.addNodes(tileLetter != 'W', new Vector2(j, i));
				goTile.transform.SetParent (boardHolder);

				if (j == 0) {
					roomX = 0;

					roomCache = board.getRoom (roomY, roomX);
					if (roomCache == null)
						roomCache = rf.getRoom ();
					
				} else if (j % 8 == 0 && j!= rows && i!=0) {
					roomX++;

					roomCache = board.getRoom (roomY, roomX);
					if (roomCache == null)
						roomCache = rf.getRoom ();
				}
					
				if ((i-1) % 8 == 0 && i != 1 && i!= cols+1 && j == 0) {
					roomY++;

					roomCache = board.getRoom (roomY, roomX);
					if (roomCache == null)
						roomCache = rf.getRoom ();
				}
			}
		}

		player.SetActive (false);
		player.transform.position = new Vector2 (specialCoords [0, 0], specialCoords [0, 1]);
		player.SetActive (true);
		GameObject exit = Instantiate (tf.exitPrefab, new Vector2 (specialCoords [1, 0], specialCoords [1, 1]), Quaternion.identity) as GameObject;
		exit.transform.SetParent (boardHolder);
	}

	// Instantiate the level map, turn off UI layer and turn on the boss dialogue
	public void BossLevelPart1(int level) {
		boardHolder = new GameObject ("Tiles").transform;
		int levelType = (level-1)/3 + 1;
		tf = new TileFactory (floorPrefabsMap [levelType], wallPrefabsMap [levelType], exitPrefab);

		int sizeX = 20, sizeY = 20;
		string tile;

		for (int i = 0; i < sizeY; i++) {
			for (int j = 0; j < sizeX; j++) {
				if (i == 0 || i == sizeY - 1 || j == 0 || j == sizeX - 1) {
					tile = "W";
				} else {
					tile = "F";
				}

				GameObject goTile = Instantiate (tf.makeTile (tile).getPrefab (), new Vector2 (j, i), Quaternion.identity) as GameObject;
				char tileLetter = goTile.gameObject.name [1];
				board.addNodes (tileLetter != 'W', new Vector2 (j, i));
				goTile.transform.SetParent (boardHolder);
			}
		}
			
		player.SetActive (false);
		GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
		bossDialogue.transform.GetChild(0).gameObject.SetActive(true);			
	}

	public void newLevel() {
		currentLevel++;
		Destroy (GameObject.Find("Tiles"));

		if (currentLevel == 10) {
			board.clearBoss ();
			BossLevelPart1(currentLevel);
		} else {		
			board.clear ();
			LoadLevel ();
			RenderLevel (currentLevel);
		}
	}

	// Instantiate player and Mayhoc (and other sprites and items?)
	public void BossLevelPart2() {
		GameObject canvas = GameObject.Find ("Canvas");
		canvas.GetComponent<Canvas>().enabled = true;
		canvas.transform.Find("Minimap").gameObject.SetActive(false); // don't need minimap for boss
		bossDialogue.transform.GetChild(0).gameObject.SetActive(false);

		player.SetActive (true);
		player.transform.position = new Vector2 (10, 1);	

		// instantiate Mayhoc
	}

    public int GetCurrentLevel() {
        return currentLevel;
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
