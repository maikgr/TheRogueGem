using RogueGem.Items;
using RogueGem.Player;
using RogueGem.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RogueGem.Enemies {
    public abstract class CreatureBehaviour : MonoBehaviour {
        protected int currentHp;
        protected int rootedTurnLength;
        protected TurnBehaviour turnBehaviour;
        private Coroutine animationCoroutine;

        void Awake() {
            currentHp = GetMaxHP();
        }

        void Start() {
            turnBehaviour = FindObjectOfType(typeof(TurnBehaviour)) as TurnBehaviour;
        }

        public abstract string GetName();
        public abstract int GetMaxHP();
        public abstract int GetATK();
        public abstract int GetDEF();
        public abstract int GetCRIT();
        public abstract IEnumerable<Item> GetItemLoot();
        public abstract Vector2 GetDestination();
        public abstract CreatureType GetCreatureType();
        public abstract void ReceiveDamage(int damage);
        public abstract void OnTurnEnds();
        public abstract void OnDead();

        public int GetCurentHP() {
            return currentHp;
        }

        public virtual void Root(int turns) {
            rootedTurnLength = turns;
        }

        public virtual void Heal(int amount) {
            currentHp = Mathf.Clamp(currentHp + amount, 0, GetMaxHP());
        }

        protected bool TryMoveBy(int xPos, int yPos) {
            return TryMoveBy(new Vector2(xPos, yPos));
        }

        protected bool TryMoveBy(Vector2 vector) {
            Vector2 destinationPos = (Vector2)transform.position + vector;
            RaycastHit2D hit = Physics2D.Raycast(destinationPos, Vector2.zero);
            if (hit.transform == null) {
                animationCoroutine = StartCoroutine(MoveTo(destinationPos));
                return true;
            } else if (hit.transform.GetComponent<CreatureBehaviour>() == null
                        && hit.transform.GetComponent<PlayerBehaviour>() == null
                        && !hit.transform.tag.Equals("Obstacle")) {                
                animationCoroutine = StartCoroutine(MoveTo(destinationPos));
                return true;
            }
            return false;
        }

        protected void ReduceRootedTurn(int amount = 1) {
            rootedTurnLength = Mathf.Max(0, --rootedTurnLength);
        }

        protected void Attack(int xPos, int yPos) {
            Attack(new Vector2(xPos, yPos));
        }

        protected void Attack(Vector2 enemyDistanceFromSelf) {
            Vector2 targetPos = (Vector2)transform.position + (enemyDistanceFromSelf * 0.7f);
            animationCoroutine = StartCoroutine(AttackEnemyOn(targetPos));
        }

        protected IEnumerator MoveTo(Vector2 destination) {
            animationCoroutine = StartCoroutine(AnimateMoving(destination));
            while (animationCoroutine != null) {
                yield return null;
            }
            OnTurnEnds();
        }        

        protected virtual bool TryInteract<T> (int xPos, int yPos, out T component)
            where T : Component {
            Vector2 destinationPos = (Vector2)transform.position + new Vector2(xPos, yPos);
            RaycastHit2D hit = Physics2D.Raycast(destinationPos, Vector2.zero);
            component = null;
            if (hit.transform != null) {
                component = hit.transform.GetComponent<T>();                
            }
            if (component != null) {
                return true;
            }
            return false;
        }

        protected IEnumerator AttackEnemyOn(Vector2 targetPos) {
            Vector2 initialPos = transform.position;
            GetComponent<SpriteRenderer>().sortingLayerName = "Attacker";
            animationCoroutine = StartCoroutine(AnimateMoving(targetPos));
            while (animationCoroutine != null) {
                yield return null;
            }
            animationCoroutine = StartCoroutine(AnimateMoving(initialPos));
            while (animationCoroutine != null) {
                yield return null;
            }
            GetComponent<SpriteRenderer>().sortingLayerName = "Creatures";
            OnTurnEnds();
        }

        private IEnumerator AnimateMoving(Vector2 destination) {
            if (animationCoroutine == null) {
                //Book next grid by moving collider instantly
                BoxCollider2D collider = transform.GetComponent<BoxCollider2D>();
                collider.offset = destination - (Vector2) transform.position;

                transform.GetComponent<SpriteRenderer>().flipX = destination.x < transform.position.x;
                GetComponent<SpriteRenderer>().sortingLayerName = "Attacker";
                float t = 0;
                while (t < 1) {
                    t += Time.deltaTime / 0.2f;
                    transform.position = Vector2.Lerp(transform.position, destination, t);
                    collider.offset = Vector2.Lerp(collider.offset, Vector2.zero, t);
                    yield return null;
                }
                GetComponent<SpriteRenderer>().sortingLayerName = "Default";
                animationCoroutine = null;
            }
        }
    }

    public enum EnemyState {
        Normal,
        Fainted,
        Dead,
        Rooted
    }

    public enum CreatureType {
        Player,
        Friendly,
        Enemy
    }
}
