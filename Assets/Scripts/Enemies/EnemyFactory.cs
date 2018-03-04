using UnityEngine;

namespace RogueGem.Enemies {
    public class EnemyFactory {

        public static ICreature GetEnemy() {
            ICreature[] enemies = new ICreature[] { new Skully()};
            return enemies[Random.Range(0, enemies.Length)];
        }
    }
}
