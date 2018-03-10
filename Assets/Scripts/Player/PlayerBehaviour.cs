using System.Collections.Generic;
using RogueGem.Enemies;
using RogueGem.Items;
using UnityEngine;
using RogueGem.Utilities;
using RogueGem.Skills;

namespace RogueGem.Player {
    public class PlayerBehaviour : CreatureBehaviour {
        public int atk;
        public int def;
        public int crit;
        public int maxHp;

        private InventoryBehaviour inventory;
        private UIBehaviour uiBehaviour;
        private bool canMove;
        private int targetedAttackDistance;
        private Skill defaultSkill;
        private Skill currentSkill;
        private Skill activeSkill;
        private Vector2 targetDirection;
        private ThrowingInventoryItem activeThrowingItem;
        private int activeItemIndex;

	    void Start () {
            inventory = GetComponent<InventoryBehaviour>();
            uiBehaviour = FindObjectOfType(typeof(UIBehaviour)) as UIBehaviour;            
            canMove = true;
            defaultSkill = new AbsorbSkill("Absorb", 1);
            currentSkill = defaultSkill;

            uiBehaviour.UpdateHealth(this);
            uiBehaviour.SetSkill(currentSkill.GetName());
        }
	
	    void Update () {
            if (canMove) {
                if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")) {
                    int horizontal = (int)(Input.GetAxisRaw("Horizontal"));
                    int vertical = Mathf.Abs(horizontal).Equals(1) ? 0 : (int)(Input.GetAxisRaw("Vertical"));
                    if (!TryMoveBy(horizontal, vertical)) {
                        EnemyBehaviour enemy;
                        if (TryInteract(horizontal, vertical, out enemy)) {
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
                } else if (Input.GetKeyDown(KeyCode.Z)) {
                    canMove = false;
                    activeSkill = currentSkill;
                    PrepareSkill(currentSkill);
                } else if (Input.GetKeyDown(KeyCode.X)) {
                    SetSkill(defaultSkill);
                }
            } else {
                if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")) {
                    int horizontal = (int)(Input.GetAxisRaw("Horizontal"));
                    int vertical = Mathf.Abs(horizontal).Equals(1) ? 0 : (int)(Input.GetAxisRaw("Vertical"));
                    targetDirection = new Vector2(horizontal, vertical);
                    switch (activeSkill.GetSkillArea()) {
                        case SkillArea.Linear:
                            uiBehaviour.LinearAttack(transform.position, targetDirection, targetedAttackDistance);
                            break;
                        case SkillArea.Single:
                            uiBehaviour.SingleAttack(transform.position, targetDirection);
                            break;
                    }                    
                } else if (Input.GetKeyDown(KeyCode.X)) {
                    uiBehaviour.CancelAttack();
                    canMove = true;
                } else if (Input.GetKeyDown(KeyCode.Z)) {
                    if (activeThrowingItem != null) {
                        activeThrowingItem.Use(this);
                        inventory.UpdateAmount(activeItemIndex, activeThrowingItem.GetAmount());
                        activeThrowingItem = null;
                    }
                    activeSkill.Use(this, targetDirection);
                    uiBehaviour.CancelAttack();                    
                    canMove = true;
                }
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
            InventoryItem item;
            index = index - 1;
            if(inventory.TryUseItem(index, out item)) {
                switch (item.GetItemType()) {
                    case ItemType.Throwing:
                        ThrowingInventoryItem throwingItem = item as ThrowingInventoryItem;                        
                        PrepareSkill(throwingItem.GetSkill());
                        activeSkill = throwingItem.GetSkill();
                        activeThrowingItem = throwingItem;
                        activeItemIndex = index;
                        break;
                    default:
                        item.Use(this);
                        break;
                }
                inventory.UpdateAmount(index, item.GetAmount());
            }
        }

        private void PrepareSkill(Skill skill) {
            canMove = false;
            targetDirection = new Vector2(1, 0);
            switch (skill.GetSkillArea()) {
                case SkillArea.Linear:
                    LinearSkill linearSkill = skill as LinearSkill;
                    targetedAttackDistance = linearSkill.GetDistance();
                    uiBehaviour.LinearAttack(transform.position, targetDirection, targetedAttackDistance);
                    break;
                case SkillArea.Single:
                    uiBehaviour.SingleAttack(transform.position, targetDirection);
                    break;
            }
        }

        public override string GetName() {
            return "Player";
        }

        public override int GetMaxHP() {
            return maxHp;
        }

        public override int GetATK() {
            int damage = Random.Range(0, 100) < crit ? Mathf.FloorToInt(atk * 1.5f) : atk;
            return damage;
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
            damage = Mathf.Max(0, damage - def);
            currentHp = currentHp - damage;
            Debug.Log("You received " + damage + " damage.");
            uiBehaviour.UpdateHealth(this);
        }

		public void Heal(int amount){
			currentHp = Mathf.Clamp(currentHp + amount, 0, maxHp);
            Debug.Log("You have recovered " + amount + " health points.");
            uiBehaviour.UpdateHealth(this);
        }

        public void SetSkill(Skill skill) {
            currentSkill = skill;
            uiBehaviour.SetSkill(currentSkill.GetName());
        }
    }
}
