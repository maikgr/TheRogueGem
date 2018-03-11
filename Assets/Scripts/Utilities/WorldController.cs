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

            for (int xStart = (int)(cameraPos.x - 4); xStart <= xEnd; ++xStart) {
                for (int yStart = (int)(cameraPos.y - 7.5f); yStart <= yEnd; ++yEnd) {
                    tilesInSight.Add(new Vector2(xStart, yStart));
                }
            }

            return tilesInSight;
        }

        public static IEnumerable<Vector2> FindPath(Vector2 startPos, Vector2 targetPos) {
            Board board = Board.Instance;
            Node startNode = board.getNode(startPos);
            Node endNode = board.getNode(targetPos);

            Heap<Node> openSet = new Heap<Node>(board.getBoardXSize() * board.getBoardYSize());
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0) {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == endNode) {                    
                    return RetracePath(startNode, endNode);
                }

                foreach (Node neighbour in board.getNodeNeighbours(currentNode)) {
                    if (!neighbour.isFloor || closedSet.Contains(neighbour)) {
                        continue;
                    }

                    int newPathCost = currentNode.gCost + GetDistanceBetweenNode(currentNode, neighbour);
                    if (newPathCost < neighbour.gCost || !openSet.Contains(neighbour)) {
                        neighbour.gCost = newPathCost;
                        neighbour.hCost = GetDistanceBetweenNode(neighbour, endNode);
                        neighbour.parentNode = currentNode;

                        if (!openSet.Contains(neighbour)) {
                            openSet.Add(neighbour);
                        } else {
                            openSet.UpdateItem(neighbour);
                        }
                    }
                }
            }

            return null;
        }

        private static int GetDistanceBetweenNode(Node fromNode, Node toNode) {
            Board board = Board.Instance;
            int xDist = (int)Mathf.Abs(fromNode.position.x - toNode.position.x);
            int yDist = (int)Mathf.Abs(fromNode.position.y - toNode.position.y);

            return xDist + yDist;
        }

        private static IEnumerable<Vector2> RetracePath(Node startNode, Node endNode) {
            List<Vector2> path = new List<Vector2>();
            Node currentNode = endNode;

            while (currentNode != startNode) {
                path.Add(currentNode.position);
                currentNode = currentNode.parentNode;
            }
            path.Reverse();
            return path;
        }
    }
}