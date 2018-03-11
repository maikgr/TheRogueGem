using RogueGem.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RogueGem.Utilities {
    public class WorldController {

        public static GameObject GetGameObjectOnPos(Vector2 pos) {
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.down);
            if (hit.collider != null) {
                return hit.transform.gameObject;
            }
            return null;
        }

        public static bool IsTileEmpty(Vector2 pos) {
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            return hit.collider == null;
        }

        public static bool IsTileInSight(Vector2 pos) {
            Vector2 cameraPos = GameObject.FindGameObjectWithTag("Player").transform.position;
			return (pos.x >= cameraPos.x - 7.5f && pos.x <= cameraPos.x + 7.5f
                    && pos.y >= cameraPos.y - 4 && pos.y <= cameraPos.y + 4);
        }

        public static IEnumerable<Vector2> GetTilesInSight() {
            Vector2 cameraPos = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
            ICollection<Vector2> tilesInSight = new List<Vector2>();
            int xEnd = (int)(cameraPos.x + 4);
            int yEnd = (int)(cameraPos.y + 7.5f);

            for(int xStart = (int)(cameraPos.x - 4); xStart <= xEnd; ++xStart) {
                for (int yStart = (int)(cameraPos.y - 7.5f); yStart <= yEnd; ++yEnd) {
                    tilesInSight.Add(new Vector2(xStart, yStart));
                }
            }

            return tilesInSight;
        }

        public static void FindPath(Vector2 startPos, Vector2 targetPos) {
            Board board = Board.Instance;
            Node startNode = board.getNode(startPos);
            Node endNode = board.getNode(targetPos);

            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();

            while (openSet.Count > 0) {
                Node currentNode = openSet[0];
                for(int i = 1; i < openSet.Count; ++i) {
                    if (openSet[i].GetFCost() < currentNode.GetFCost()
                        || openSet[i].GetFCost() == currentNode.GetFCost()
                        && openSet[i].GetFCost() < currentNode.GetFCost()) {
                        currentNode = openSet[i];
                    }
                }
            }
        }
    }
}