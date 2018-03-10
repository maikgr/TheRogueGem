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
        private UIBehaviour uiBehaviour;
	    void Start () {
            inventory = GetComponent<InventoryBehaviour>();
            uiBehaviour = FindObjectOfType(typeof(UIBehaviour)) as UIBehaviour;
            uiBehaviour.UpdateHealth(this);
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
            } else if (Input.GetKeyDown(KeyCode.Alpha1)) {
                UseItem(1);
            } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                UseItem(2);
            } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
                UseItem(3);
            }
        }        

        public override void OnAnimationEnds() {
            Item item;
            if(IsItemOnGround(out item)) {
                uiBehaviour.UpdateGroundSlot(item);
            } else {
                uiBehaviour.RemoveItemOnGround();
            }
            EventBehaviour.TriggerEvent(GameEvent.MoveEnemy);
        }
        private bool IsItemOnGround(out Item item) {
            int layerMask = LayerMask.GetMask("Items");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 1f, layerMask);
            item = null;
            if (hit.transform != null) {
                item = hit.transform.GetComponent<Item>();
                return true;
            }
            return false;
        }

        private void GetItemOnGround() {
            Item item = null;
            if (IsItemOnGround(out item)) {
                inventory.PutInInventory(item);
            }
        }

        private void UseItem(int index) {
            InventoryItem item = inventory.GetItem(index - 1, this);
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

        public override void OnDead() {
            Debug.Log("Player is dead");
        }

        public override void ReceiveDamage(int damage) {
            currentHp = currentHp - (Mathf.Max(0, damage - def));
            uiBehaviour.UpdateHealth(this);
        }

		public void Heal(int amount){
			currentHp = Mathf.Clamp(currentHp + amount, 0, maxHp);
            uiBehaviour.UpdateHealth(this);
        }
    }
}
