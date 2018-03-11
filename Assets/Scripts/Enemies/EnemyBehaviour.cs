using UnityEngine;
using RogueGem.Utilities;
using RogueGem.Skills;
using RogueGem.Player;
using System.Collections.Generic;
using RogueGem.Level;

namespace RogueGem.Enemies {
    public abstract class EnemyBehaviour : CreatureBehaviour {

        protected EnemyState state;
        protected int turnPassed;
        protected int immobilizedOnTurn;
        protected int immobileTurnLength;
        protected PlayerBehaviour player;
        protected TurnBehaviour turns;
        private Vector2 lastPosition;
        private Board board;
        public abstract Skill GetSkill();
        public abstract int GetSightDistance();

        void Start() {
            player = FindObjectOfType(typeof(PlayerBehaviour)) as PlayerBehaviour;
            turns = FindObjectOfType(typeof(TurnBehaviour)) as TurnBehaviour;
            turns.RegisterEnemy(this);
            lastPosition = transform.position;
            board = Board.Instance;
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
            Node oldNode = new Node(true, lastPosition);
            Node newNode = new Node(false, transform.position);
            board.updateBoardNode(oldNode);
            board.updateBoardNode(newNode);
        }

        public override void OnDead() {
            currentHp = 0;
            turns.RemoveEnemy(this);
            Destroy(gameObject);
        }

        public void OnFainted() {
            currentHp = 1;
            GetComponent<SpriteRenderer>().color = Color.gray;
            SetImmobile(5);
        }

        public void Move() {
            ++turnPassed;
            if (IsAbleToMove()) {
                lastPosition = transform.position;
                TryMoveBy(GetDestination());
            }
        }

        private bool IsAbleToMove() {
            if (turnPassed - immobilizedOnTurn >= immobileTurnLength) {
                if (state == EnemyState.Fainted) {
                    GetComponent<SpriteRenderer>().color = Color.white;
                    currentHp = 1;
                }
                state = EnemyState.Normal;
            }
            return state == EnemyState.Normal;
        }

        protected bool IsPlayerInSight() {
            int xMin = (int)transform.position.x - GetSightDistance();
            int xMax = (int)transform.position.x + GetSightDistance();
            int yMin = (int)transform.position.y - GetSightDistance();
            int yMax = (int)transform.position.y + GetSightDistance();
            int xPlayerPos = (int)player.transform.position.x;
            int yPlayerPos = (int)player.transform.position.y;

            return (xPlayerPos >= xMin && xPlayerPos <= xMax
                    && yPlayerPos >= yMin && yPlayerPos <= yMax);
        }

        protected Vector2 GetNextRandomGrid() {
            int xMovement = 0;
            int yMovement = 0;
            while (xMovement.Equals(0) && yMovement.Equals(0)) {
                xMovement = UnityEngine.Random.Range(-1, 2);
                yMovement = xMovement.Equals(0) ? UnityEngine.Random.Range(-1, 2) : 0;
            }
            return new Vector2(xMovement, yMovement);
        }

        protected Vector2 GetPathfinderFirstGrid() {
            List<Vector2> path = WorldController.FindPath(transform.position, player.transform.position) as List<Vector2>;
            if (path == null || path.Count == 0) {
                return transform.position;
            }
            return path[0];
        }

        public void Absorb() {
            state = EnemyState.Dead;
            OnDead();
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
