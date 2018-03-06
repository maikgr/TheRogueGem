using System.Collections.Generic;
using RogueGem.Enemies;
using RogueGem.Items;
using UnityEngine;
using RogueGem.Utilities;
using System;

namespace RogueGem.Controllers {
    public class PlayerBehaviour : CreatureBehaviour {

        private int atk;
        private int def;
        private int crit;
        private int maxHp;
	    void Start () {
            atk = 5;
            def = 1;
            crit = 10;
            maxHp = 10;
	    }
	
	    void Update () {
            if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")) {
                int horizontal = (int)(Input.GetAxisRaw("Horizontal"));
                int vertical = (int)(Input.GetAxisRaw("Vertical"));
                if (!TryMoveBy(horizontal, vertical)) {
                    EnemyBehaviour enemy = null;
                    if(TryInteract(horizontal, vertical, out enemy)) {
                        AttackController.Attack(this, enemy);
                        Attack(horizontal, vertical);
                    }
                }
            }
        }

        public override void OnAnimationEnds() {
            EventBehaviour.TriggerEvent(GameEvent.MoveEnemy);
        }

        public override void OnDead() {
            Debug.Log("Player is dead");
        }

        public override string GetName() {
            return "Player";
        }

        public override int GetMaxHP() {
            return maxHp;
        }

        public override int GetATK() {
            return atk;
        }

        public override int GetDEF() {
            return def;
        }

        public override int GetCRIT() {
            return crit;
        }

        public override IEnumerable<IItem> GetItemLoot() {
            return null;
        }

        public override Vector2 GetDestination() {
            return Vector2.zero;
        }

        public override void ReceiveDamage(int damage) {
            currentHp = currentHp - (Mathf.Max(0, damage - GetDEF()));
        }
    }
}
