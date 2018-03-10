using UnityEngine;

namespace RogueGem.Enemies {
    public class EnemyFactory {

        public static GameObject GetEnemy() {
            GameObject enemies = Resources.Load("Prefabs/Creatures/Skully") as GameObject;
            return enemies;
        }
    }
}
