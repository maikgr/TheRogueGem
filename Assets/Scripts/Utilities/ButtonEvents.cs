using RogueGem.Enemies;
using RogueGem.Items;
using RogueGem.Player;
using System.Collections.Generic;
using UnityEngine;

namespace RogueGem.Utilities {
    public class ButtonEvents : MonoBehaviour {
        int xMin;
        int yMin;
        int xMax;
        int yMax;

        void Start() {
            xMin = 1;
            yMin = 1;
            xMax = Board.Instance.getBoardXSize();
            yMax = Board.Instance.getBoardYSize();
        }

        public void RandomSpawn() {
            GameObject enemy = EnemyFactory.GetRandomEnemy();
            Instantiate(enemy, RandomPos(), Quaternion.identity);
        }

        public void HurtPlayer() {
            PlayerBehaviour player = FindObjectOfType(typeof(PlayerBehaviour)) as PlayerBehaviour;
            player.ReceiveDamage(1);
        }

        public void RandomItem() {           
            GameObject item = ItemFactory.GetRandomItem();
            Instantiate(item, RandomPos(), Quaternion.identity);
        }

        private Vector2 RandomPos() {
            Vector2 pos = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
            while (!WorldController.IsTileEmpty(pos)) {
                pos = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
            }
            return pos;
        }
    }
}
