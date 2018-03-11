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
        private PlayerBehaviour player;

        void Start() {
            enemies = new List<EnemyBehaviour>();
            player = FindObjectOfType(typeof(PlayerBehaviour)) as PlayerBehaviour;
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

        private void ExecuteEnemyTurn() {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach(EnemyBehaviour enemy in enemies) {
                enemy.EnemyTurn();
            }
            stopwatch.Stop();
            EventBehaviour.TriggerEvent(GameEvent.EnemyTurnEnd);
        }
    }
}
