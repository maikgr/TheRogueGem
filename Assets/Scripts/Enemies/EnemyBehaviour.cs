using UnityEngine;
using RogueGem.Utilities;
using RogueGem.Skills;
using RogueGem.Player;
using System.Collections.Generic;
using RogueGem.Level;
using RogueGem.Items;

namespace RogueGem.Enemies {
    public abstract class EnemyBehaviour : CreatureBehaviour {

        protected EnemyState state;
        protected PlayerBehaviour player;
        protected TurnBehaviour turns;
        private Vector2 lastPosition;
        public abstract Skill GetSkill();
        public abstract int GetSightDistance();
        public abstract void AttackPlayer();
        public abstract bool IsInAttackRange();

        void Start() {
            player = FindObjectOfType(typeof(PlayerBehaviour)) as PlayerBehaviour;
            turns = FindObjectOfType(typeof(TurnBehaviour)) as TurnBehaviour;
            turns.RegisterEnemy(this);
            lastPosition = transform.position;
            
        }        

        public override void ReceiveDamage(int damage) {
            damage = Mathf.Max(0, damage - GetDEF());
            currentHp = currentHp - damage;
            Debug.Log(GetName() + " received " + damage + " damage.");
            if (currentHp <= 1 && state.Equals(EnemyState.Normal)) {
                state = EnemyState.Fainted;
                Debug.Log(GetName() + " has fainted.");
                OnFainted();
            } else if (state.Equals(EnemyState.Fainted)) {
                state = EnemyState.Dead;
                Debug.Log(GetName() + " is dead.");
                OnDead();
            }
        }

        public override void OnTurnEnds() {
            Board board = Board.Instance;
            Node oldNode = new Node(true, lastPosition);
            Node newNode = new Node(false, transform.position);
            board.updateBoardNode(oldNode);
            board.updateBoardNode(newNode);
        }

        public override void OnDead() {
            currentHp = 0;
            turns.RemoveEnemy(this);
            Node currentNode = new Node(true, transform.position);
            Board.Instance.updateBoardNode(currentNode);
            DropLoot();
            Destroy(gameObject);
        }

        public void OnFainted() {
            currentHp = 1;
            GetComponent<SpriteRenderer>().color = Color.gray;
            base.Root(5);
        }

        public void EnemyTurn() {            
            if (IsInAttackRange() && state != EnemyState.Fainted) {
                AttackPlayer();
            } else if (IsAbleToMove()) {
                lastPosition = transform.position;
                TryMoveBy(GetDestination());
            }
        }

        private void DropLoot() {
            string itemName = string.Empty;
            List<string> possibleItems = new List<string>();
            int dice = UnityEngine.Random.Range(0, 101);
            foreach(var drop in GetItemDropChance()) {
                if(dice <= drop.Value) {
                    possibleItems.Add(drop.Key);
                }
            }

            if(possibleItems.Count > 0) {
                itemName = possibleItems[UnityEngine.Random.Range(0, possibleItems.Count)];
                Instantiate(ItemFactory.GetItem(itemName), transform.position, Quaternion.identity);
            }
        }

        private bool IsAbleToMove() {
            if (rootedTurnLength.Equals(0) && state != EnemyState.Normal) {
                if (state == EnemyState.Fainted) {
                    GetComponent<SpriteRenderer>().color = Color.white;
                    currentHp = 1;
                }
                state = EnemyState.Normal;
            }
            if (rootedTurnLength > 0) {
                rootedTurnLength = Mathf.Max(0, --rootedTurnLength);
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

        public override void Root(int turns){
            if (state.Equals(EnemyState.Normal)) {
                base.Root(turns);
                state = EnemyState.Rooted;
                Debug.Log(GetName() + " movement has been restricted.");
            }
        }

        public EnemyState GetState() {
            return state;
        }

        public override CreatureType GetCreatureType() {
            return CreatureType.Enemy;
        }
    }
}
