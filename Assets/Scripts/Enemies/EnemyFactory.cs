using UnityEngine;

namespace RogueGem.Enemies {
    public class EnemyFactory {

        public static IEnemy GetEnemy() {
            IEnemy[] enemies = new IEnemy[] { new Skully(), new Bubugg() };
            return enemies[Random.Range(0, enemies.Length)];
        }
    }
}
