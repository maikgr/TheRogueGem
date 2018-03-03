using RogueGem.Controllers;
using RogueGem.Enemies;
using System.Collections.Generic;
using UnityEngine;

namespace RogueGem.Utilities {
    public class SpawnEnemy : MonoBehaviour {
        public List<GameObject> enemyList = new List<GameObject>();
        public void RandomSpawn() {
            int num = Random.Range(0, enemyList.Count - 1);
            Vector2 pos = new Vector2(Random.Range(-8, 8), Random.Range(-4, 4));
            GameObject enemy = EnemyFactory.GetEnemy().GetPrefab();
            Instantiate(enemy, pos, Quaternion.identity);
        }
    }
}
