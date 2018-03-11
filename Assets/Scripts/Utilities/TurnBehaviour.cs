using RogueGem.Enemies;
using RogueGem.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RogueGem.Utilities {
    public class TurnBehaviour : MonoBehaviour{
        private ICollection<EnemyBehaviour> enemies;
        private PlayerBehaviour player;

        void Start() {
            enemies = new List<EnemyBehaviour>();
            player = FindObjectOfType(typeof(PlayerBehaviour)) as PlayerBehaviour;
        }

        void OnEnable() {
            EventBehaviour.StartListening(GameEvent.PlayerTurnEnd, MoveEnemies);
        }

        void OnDisable() {
            EventBehaviour.StopListening(GameEvent.PlayerTurnEnd, MoveEnemies);
        }

        public void RegisterEnemy(EnemyBehaviour enemy) {
            enemies.Add(enemy);
        }

        public void RemoveEnemy(EnemyBehaviour enemy) {
            enemies.Remove(enemy);
        }

        private void MoveEnemies() {
            foreach(EnemyBehaviour enemy in enemies) {
                enemy.Move();
            }
            EventBehaviour.TriggerEvent(GameEvent.EnemyTurnEnd);
        }
    }
}
