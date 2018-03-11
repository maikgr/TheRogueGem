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

        private void ExecuteEnemyTurn() {
            foreach(EnemyBehaviour enemy in enemies) {
                enemy.EnemyTurn();
            }
            ++turnNumber;
            EventBehaviour.TriggerEvent(GameEvent.EnemyTurnEnd);
        }
    }
}
