using RogueGem.Player;
using UnityEngine;

namespace RogueGem.Utilities {
    class CameraBehaviour : MonoBehaviour {

        private PlayerBehaviour player;

        void Start() {
            player = FindObjectOfType(typeof(PlayerBehaviour)) as PlayerBehaviour;
            MoveCamera(player.transform.position);
        }

        void LateUpdate() {
            MoveCamera(player.transform.position);
        }

        public void MoveCamera(Vector2 playerPos) {
            float xPos = Mathf.Clamp(playerPos.x - 0.5f, 7.5f, 25.5f);
            float yPos = Mathf.Clamp(playerPos.y, 4f, 29f);
            transform.position = new Vector3(xPos, yPos, transform.position.z);
        }
    }
}
