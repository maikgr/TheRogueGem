using RogueGem.Enemies;
using RogueGem.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Diagnostics;

namespace RogueGem.Utilities {
    public class TurnBehaviour : MonoBehaviour{
        private ICollection<EnemyBehaviour> enemies;
        private int turnNumber;

        void Start() {
            enemies = new List<EnemyBehaviour>();
            turnNumber = 0;
			
        }

        void OnEnable() {
            EventBehaviour.StartListening(GameEvent.PlayerTurnEnd, ExecuteEnemyTurn);
			EventBehaviour.StartListening(GameEvent.EnemyTurnEnd, SpawnEnemy);
        }

        void OnDisable() {
            EventBehaviour.StopListening(GameEvent.PlayerTurnEnd, ExecuteEnemyTurn);
        }

        public void RegisterEnemy(EnemyBehaviour enemy) {
            enemies.Add(enemy);
        }

        public void RemoveEnemy(EnemyBehaviour enemy) {
            enemies.Remove(enemy);
        }

        public int GetTurnNumber() {
            return turnNumber;
        }

        public void Reset() {
            Start();
        }

        private void ExecuteEnemyTurn() {
            foreach(EnemyBehaviour enemy in enemies) {
                enemy.EnemyTurn();
            }
            ++turnNumber;
            EventBehaviour.TriggerEvent(GameEvent.EnemyTurnEnd);
        }

		// Spawns a new enemy every 10 steps
		public void SpawnEnemy() {
			if (LevelManager.currentLevel == 10)
				return;
			
			if (turnNumber % 10 == 0) {
				int max = LevelManager.numRooms * 8 - 2,
				min = 1;

				int j = UnityEngine.Random.Range (1, max),
				i = UnityEngine.Random.Range (1, min);

				Vector2 pos = new Vector2 (i, j);
				while (!WorldController.IsTileEmpty(pos)) {
					if (i > max)
						i = min;

					if (j > max)
						j = min;
					
					pos = UnityEngine.Random.Range(0,2) == 0 ? new Vector2 (i++, j) : new Vector2 (i, j++);
				}

                Transform enemyHolder = GameObject.Find("Enemies").transform;
                GameObject enemy = Instantiate (EnemyFactory.GetEnemy(LevelManager.currentLevel), pos, Quaternion.identity, enemyHolder) as GameObject;
			}
		}
    }
}
