using UnityEngine;
using RogueGem.Utilities;
using RogueGem.Skills;

namespace RogueGem.Enemies {
    public abstract class EnemyBehaviour : CreatureBehaviour {

        protected EnemyState state;
        protected int turnPassed;
        protected int immobilizedOnTurn;
        protected int immobileTurnLength;

        public abstract Skill GetSkill();

        void OnEnable() {
            EventBehaviour.StartListening(GameEvent.MoveEnemy, Move);
        }

        void OnDisable() {
            EventBehaviour.StopListening(GameEvent.MoveEnemy, Move);
        }

        public override void ReceiveDamage(int damage) {
            damage = Mathf.Max(0, damage - GetDEF());
            currentHp = currentHp - damage;
            Debug.Log(GetName() + " received " + damage + " damage.");
            if (currentHp <= 1 && state.Equals(EnemyState.Normal)) {
                state = EnemyState.Fainted;
                OnFainted();
            } else if (state.Equals(EnemyState.Fainted)) {
                state = EnemyState.Dead;
                OnDead();
            }
        }

        public override void OnAnimationEnds() {

        }

        public override void OnDead() {
            currentHp = 0;
            Destroy(gameObject);
        }

        public void OnFainted() {
            currentHp = 1;
            GetComponent<SpriteRenderer>().color = Color.gray;
            SetImmobile(5);
        }

        private void Move() {
            if (turnPassed - immobilizedOnTurn >= immobileTurnLength) {
                if (state == EnemyState.Fainted) {
                    GetComponent<SpriteRenderer>().color = Color.white;
                    currentHp = 1;
                }
                state = EnemyState.Normal;
            }

            ++turnPassed;
            if (state == EnemyState.Normal) {
                TryMoveBy(GetDestination());
            }
        }

        public void Root(int turns) {
            state = EnemyState.Rooted;
            SetImmobile(turns);
            Debug.Log(GetName() + " movement has been restricted.");
        }

        private void SetImmobile(int turns) {
            immobileTurnLength = turns;
            immobilizedOnTurn = turnPassed;
        }

        public EnemyState GetState() {
            return state;
        }
    }
}
