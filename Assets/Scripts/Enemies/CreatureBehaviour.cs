using RogueGem.Controllers;
using RogueGem.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RogueGem.Enemies {
    public abstract class CreatureBehaviour : MonoBehaviour {
        protected int currentHp;
        private Coroutine animationCoroutine;

        void Awake() {
            currentHp = GetMaxHP();
        }

        public abstract string GetName();
        public abstract int GetMaxHP();
        public abstract int GetATK();
        public abstract int GetDEF();
        public abstract int GetCRIT();
        public abstract IEnumerable<Item> GetItemLoot();
        public abstract Vector2 GetDestination();
        public abstract void ReceiveDamage(int damage);
        public abstract void OnAnimationEnds();
        public abstract void OnDead();

        public int GetCurentHP() {
            return currentHp;
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
            } else if (hit.transform.GetComponent<CreatureBehaviour>() == null) {
                animationCoroutine = StartCoroutine(MoveTo(destinationPos));
                return true;
            }
            return false;
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
            OnAnimationEnds();
        }        

        protected virtual bool TryInteract<T> (int xPos, int yPos, out T component)
            where T : Component {
            Vector2 destinationPos = (Vector2)transform.position + new Vector2(xPos, yPos);
            RaycastHit2D hit = Physics2D.Raycast(destinationPos, Vector2.zero);
            component = hit.transform.GetComponent<T>();
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
            OnAnimationEnds();
        }

        private IEnumerator AnimateMoving(Vector2 destination) {
            if (animationCoroutine == null) {
                transform.GetComponent<SpriteRenderer>().flipX = destination.x < transform.position.x;
                float t = 0;
                while (t < 1) {
                    t += Time.deltaTime / 0.2f;
                    transform.position = Vector2.Lerp(transform.position, destination, t);
                    yield return null;
                }
                animationCoroutine = null;
            }
        }
    }

    public enum EnemyState {
        Normal,
        Fainted,
        Dead
    }
}
