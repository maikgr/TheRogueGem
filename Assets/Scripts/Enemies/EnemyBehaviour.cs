using RogueGem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using RogueGem.Items;
using RogueGem.Utilities;

namespace RogueGem.Enemies {
    public abstract class EnemyBehaviour : CreatureBehaviour{

        private MovementController movementControl;
        protected EnemyState state;
        public override void ReceiveDamage(int damage) {
            currentHp = currentHp - (Mathf.Max(0, damage - GetDEF()));
            if (currentHp <= 1 && state.Equals(EnemyState.Normal)) {
                currentHp = 1;
                state = EnemyState.Fainted;
            } else if (state.Equals(EnemyState.Fainted)) {
                currentHp = 0;
                state = EnemyState.Dead;
            }
        }

        void Start() {
            movementControl = new MovementController(this);                               
        }
        
        void Update() {
            if (!EventBehaviour.instance.isPlayerTurn) {
                Move();
            }
        }

        private void Move() {
            Vector2 destination = GetDestination();
            movementControl.MoveByTile(gameObject, (int)destination.x, (int)destination.y);
        }
    }
}
