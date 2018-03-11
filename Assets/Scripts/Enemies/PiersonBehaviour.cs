using System;
using System.Collections.Generic;
using RogueGem.Items;
using RogueGem.Skills;
using UnityEngine;

namespace RogueGem.Enemies {
    public class PiersonBehaviour : EnemyBehaviour {

        public string creatureName = "Pierson";
        public int maxHealth = 5;
        public int atk = 2;
        public int def = 0;
        public int crit = 0;

        public override Vector2 GetDestination() {
            Vector2 destination = GetPathfinderFirstGrid();
            return destination - (Vector2)transform.position;
        }

        public override Skill GetSkill() {
            return new StickyWebSkill ("Sticky Web", 0, 4, 3);
        }

        public override int GetATK() {
            return atk;
        }

        public override int GetCRIT() {
            return crit;
        }

        public override int GetDEF() {
            return def;
        }

        public override IEnumerable<Item> GetItemLoot() {
            return null;
        }

        public override int GetMaxHP() {
            return maxHealth;
        }

        public override string GetName() {
            return creatureName;
        }

        public override int GetSightDistance() {
            return 0;
        }
    }
}
