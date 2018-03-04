using RogueGem.Controllers;
using RogueGem.Enemies;
using System.Collections.Generic;
using UnityEngine;

namespace RogueGem.Utilities {
    public class SpawnEnemy : MonoBehaviour {
        public void RandomSpawn() {
            Vector2 pos = new Vector2(Random.Range(-8, 8), Random.Range(-4, 4));
            GameObject enemy = EnemyFactory.GetEnemy();
            Instantiate(enemy, pos, Quaternion.identity);
        }
    }
}
