using RogueGem.Player;
using UnityEngine;

namespace RogueGem.Utilities {
    class CameraBehaviour : MonoBehaviour {

        private PlayerBehaviour player;
        private Camera mainCamera;
        void Start() {
            player = FindObjectOfType(typeof(PlayerBehaviour)) as PlayerBehaviour;
            mainCamera = GetComponent<Camera>();
            MoveCamera(player.transform.position);
        }

        void LateUpdate() {
            MoveCamera(player.transform.position);
        }

        public void MoveCamera(Vector2 playerPos) {
            Board board = Board.Instance;
            float offSet = 0.5f;
            float xMin = mainCamera.orthographicSize * 2 - offSet - 1;
            float xMax = board.getBoardXSize() - (mainCamera.orthographicSize * 2 - offSet);
            float yMin = mainCamera.orthographicSize - offSet;
            float yMax = board.getBoardYSize() - (mainCamera.orthographicSize + offSet);

            float xPos = Mathf.Clamp(playerPos.x - 0.5f, xMin, xMax);
            float yPos = Mathf.Clamp(playerPos.y, yMin, yMax);
            transform.position = new Vector3(xPos, yPos, transform.position.z);
        }
    }
}
