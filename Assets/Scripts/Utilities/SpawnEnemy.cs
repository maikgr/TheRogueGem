using RogueGem.Controllers;
using RogueGem.Enemies;
using RogueGem.Player;
using System.Collections.Generic;
using UnityEngine;

namespace RogueGem.Utilities {
    public class SpawnEnemy : MonoBehaviour {
        public void RandomSpawn() {
            Vector2 pos = new Vector2(Random.Range(4, 15), Random.Range(3, 12));
            GameObject enemy = EnemyFactory.GetEnemy();
            Instantiate(enemy, pos, Quaternion.identity);
        }

        public void HurtPlayer() {
            PlayerBehaviour player = FindObjectOfType(typeof(PlayerBehaviour)) as PlayerBehaviour;
            player.ReceiveDamage(1);
        }
    }
}
