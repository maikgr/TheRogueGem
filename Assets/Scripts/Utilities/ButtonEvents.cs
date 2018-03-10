using RogueGem.Enemies;
using RogueGem.Items;
using RogueGem.Player;
using System.Collections.Generic;
using UnityEngine;

namespace RogueGem.Utilities {
    public class ButtonEvents : MonoBehaviour {
        public void RandomSpawn() {
            Vector2 pos = new Vector2(Random.Range(1, 33), Random.Range(1, 33));
            while (!WorldController.IsTileEmpty(pos)) {
                pos = new Vector2(Random.Range(1, 33), Random.Range(1, 33));
            }
            GameObject enemy = EnemyFactory.GetEnemy();
            Instantiate(enemy, pos, Quaternion.identity);
        }

        public void HurtPlayer() {
            PlayerBehaviour player = FindObjectOfType(typeof(PlayerBehaviour)) as PlayerBehaviour;
            player.ReceiveDamage(1);
        }

        public void RandomItem() {
            Vector2 pos = new Vector2(Random.Range(1, 33), Random.Range(1, 33));
            while (!WorldController.IsTileEmpty(pos)) {
                pos = new Vector2(Random.Range(1, 33), Random.Range(1, 33));
            }
            GameObject item = ItemFactory.GetRandomItem();
            Instantiate(item, pos, Quaternion.identity);
        }
    }
}
