using UnityEngine;
using System.Collections;
using RogueGem.Level;
using System.Collections.Generic;

public class Board : MonoBehaviour {
	const int roomX = 4, roomY = 4;
	GameObject[,] tiles = new GameObject[34, 34];
	Room[,] rooms = new Room[roomX, roomY];
    Node[,] nodes = new Node[34, 34];
	private static Board _instance;
	public static Board Instance { get { return _instance; } }

	void Start () {
		if (_instance != null && _instance != this)
		{
			Destroy(gameObject);
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

    public void addNodes(bool isFloor, Vector2 position) {
        nodes[(int)position.x, (int)position.y] = new Node(isFloor, position);
    }

    public Node getNode(Vector2 position) {
        return nodes[(int)position.x, (int)position.y];
    }

    public IEnumerable<Node> getNodeNeighbours(Node sourceNode) {
        List<Node> neighbours = new List<Node>();

        int xPos = (int)sourceNode.position.x;
        int yPos = (int)sourceNode.position.y;

        if (!xPos.Equals(0)) {
            neighbours.Add(nodes[xPos - 1, yPos]);
        }
        if (!xPos.Equals(34)) {
            neighbours.Add(nodes[xPos + 1, yPos]);
        }
        if (!yPos.Equals(0)) {
            neighbours.Add(nodes[xPos, yPos + 1]);
        }
        if (!yPos.Equals(34)) {
            neighbours.Add(nodes[xPos, yPos - 1]);
        }

        return neighbours;
    }
}
