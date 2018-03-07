using System.Collections.Generic;
using RogueGem.Enemies;
using RogueGem.Items;
using UnityEngine;
using RogueGem.Utilities;
using RogueGem.Controllers;

namespace RogueGem.Player {
    public class PlayerBehaviour : CreatureBehaviour {
        public int atk;
        public int def;
        public int crit;
        public int maxHp;

        private InventoryBehaviour inventory;

	    void Start () {
            atk = 5;
            def = 1;
            crit = 10;
            maxHp = 10;
            inventory = GetComponent<InventoryBehaviour>();
	    }
	
	    void Update () {
            if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")) {
                int horizontal = (int)(Input.GetAxisRaw("Horizontal"));
                int vertical = Mathf.Abs(horizontal).Equals(1) ? 0 : (int)(Input.GetAxisRaw("Vertical"));
                if (!TryMoveBy(horizontal, vertical)) {
                    EnemyBehaviour enemy;
                    if(TryInteract(horizontal, vertical, out enemy)) {
                        AttackController.Attack(this, enemy);
                        Attack(horizontal, vertical);
                    }
                }
            } else if (Input.GetKeyDown(KeyCode.E)) {
                GetItemOnGround();
            }
        }        

        public override void OnAnimationEnds() {
            Item item;
            if(IsItemOnGround(out item)) {                
                inventory.PutInGroundSlot(item);
            } else {
                inventory.RemoveFromGroundSlot();
            }
            EventBehaviour.TriggerEvent(GameEvent.MoveEnemy);
        }

        public override void OnDead() {
            Debug.Log("Player is dead");
        }

        private bool IsItemOnGround(out Item item) {
            int layerMask = LayerMask.GetMask("Item");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 1f, layerMask);
            item = null;
            if (hit.transform != null) {
                item = hit.transform.GetComponent<Item>();
                return true;
            }
            return false;
        }

        private void GetItemOnGround() {
            int layerMask = LayerMask.GetMask("Item");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 1f, layerMask);
            if (hit.transform != null) {
                Item item = hit.transform.GetComponent<Item>();
                inventory.PutInInventory(item);
            }
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

        public override IEnumerable<Item> GetItemLoot() {
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
