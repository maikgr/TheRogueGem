using System.Collections.Generic;
using UnityEngine;

namespace RogueGem.Enemies {
    public class EnemyFactory {

        public static GameObject GetEnemy(int level) {
            string[] enemyRepo;
            switch (level) {
                case 1:
                    
                    break;
            }
            return GetRandomEnemy();
        }

		public static GameObject GetRandomEnemy() {
            GameObject[] enemies = Resources.LoadAll<GameObject>("Prefabs/Creatures");
            return enemies[Random.Range(0, enemies.Length)];
        }
    }
}
