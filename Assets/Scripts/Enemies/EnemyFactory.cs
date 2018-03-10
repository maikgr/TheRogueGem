using UnityEngine;

namespace RogueGem.Enemies {
    public class EnemyFactory {

        public static GameObject GetRandomEnemy() {
            GameObject[] enemies = Resources.LoadAll<GameObject>("Prefabs/Creatures");
            return enemies[Random.Range(0, enemies.Length)];
        }
    }
}
