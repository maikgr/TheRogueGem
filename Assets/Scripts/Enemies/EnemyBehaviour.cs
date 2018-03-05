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

        protected EnemyState state;

        void OnEnable() {
            EventBehaviour.StartListening(GameEvent.MoveEnemy, Move);
        }

        void OnDisable() {
            EventBehaviour.StopListening(GameEvent.MoveEnemy, Move);
        }

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

        public override void OnAnimationEnds() {
            
        }

        private void Move() {
            TryMoveBy(GetDestination());
        }
    }
}
