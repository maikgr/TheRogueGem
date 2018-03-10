﻿using UnityEngine;
using RogueGem.Utilities;
using RogueGem.Skills;

namespace RogueGem.Enemies {
    public abstract class EnemyBehaviour : CreatureBehaviour{

        protected EnemyState state;
        protected int turnPassed;
        protected int turnFainted;

        public abstract Skill GetSkill();

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
                turnFainted = turnPassed;
                OnFainted();
            } else if (state.Equals(EnemyState.Fainted)) {
                currentHp = 0;
                state = EnemyState.Dead;
                OnDead();
            }
        }

        public override void OnAnimationEnds() {
            
        }

        public override void OnDead() {
            Destroy(gameObject);
        }

        public void OnFainted() {
            GetComponent<SpriteRenderer>().color = Color.gray;            
        }

        private void Move() {
            if (turnPassed - turnFainted >= 5 && state == EnemyState.Fainted) {
                currentHp = 1;
                state = EnemyState.Normal;
                GetComponent<SpriteRenderer>().color = Color.white;
            }
            ++turnPassed;
            if (state == EnemyState.Normal) {
                TryMoveBy(GetDestination());
            }
        }

        public EnemyState GetState() {
            return state;
        }
    }
}
