using System;
using System.Collections.Generic;
using RogueGem.Enemies;
using RogueGem.Items;
using UnityEngine;
using UnityEngine.Events;
using RogueGem.Utilities;

namespace RogueGem.Controllers {
    public class PlayerBehaviour : CreatureBehaviour {

        private MovementController movementControl;
        private int atk;
        private int def;
        private int crit;
        private int maxHp;
	    void Start () {
            movementControl = new MovementController(this);
            atk = 5;
            def = 1;
            crit = 10;
            maxHp = 10;
	    }
	
	    void Update () {
            if (EventBehaviour.instance.isPlayerTurn) {
                if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")) {
                    int horizontal = (int)(Input.GetAxisRaw("Horizontal"));
                    int vertical = (int)(Input.GetAxisRaw("Vertical"));
                    movementControl.MoveByTile(gameObject, horizontal, vertical);
                }
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

        public override IEnumerable<IItem> GetItemLoot() {
            return null;
        }

        public override Vector2 GetDestination() {
            throw new NotImplementedException();
        }

        public override void ReceiveDamage(int damage) {
            currentHp = currentHp - (Mathf.Max(0, damage - GetDEF()));
        }
    }
}
