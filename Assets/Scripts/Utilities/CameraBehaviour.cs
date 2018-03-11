using RogueGem.Player;
using UnityEngine;

namespace RogueGem.Utilities {
    class CameraBehaviour : MonoBehaviour {

        private PlayerBehaviour player;
        private Camera camera;
        void Start() {
            player = FindObjectOfType(typeof(PlayerBehaviour)) as PlayerBehaviour;
            camera = GetComponent<Camera>();
            MoveCamera(player.transform.position);
        }

        void LateUpdate() {
            MoveCamera(player.transform.position);
        }

        public void MoveCamera(Vector2 playerPos) {
            Board board = Board.Instance;
            float offSet = 0.5f;
            float xMin = camera.orthographicSize * 2 - offSet - 1;
            float xMax = board.getBoardXSize() - (camera.orthographicSize * 2 - offSet);
            float yMin = camera.orthographicSize - offSet;
            float yMax = board.getBoardYSize() - (camera.orthographicSize + offSet);

            float xPos = Mathf.Clamp(playerPos.x - 0.5f, xMin, xMax);
            float yPos = Mathf.Clamp(playerPos.y, yMin, yMax);
            transform.position = new Vector3(xPos, yPos, transform.position.z);
        }
    }
}
