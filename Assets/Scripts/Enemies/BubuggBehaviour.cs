using System.Collections.Generic;
using RogueGem.Items;
using RogueGem.Skills;
using UnityEngine;

namespace RogueGem.Enemies {
    public class BubuggBehaviour : EnemyBehaviour {

        public string creatureName = "Bubugg";
        public int maxHealth = 5;
        public int atk = 3;
        public int def = 1;
        public int crit = 10;

        public override Vector2 GetDestination() {
            int xMovement = 0;
            int yMovement = 0;
            while (xMovement.Equals(0) && yMovement.Equals(0)) {
                xMovement = Random.Range(-1, 2);
                yMovement = xMovement.Equals(0) ? Random.Range(-1, 2) : 0;
            }
            return new Vector2(xMovement, yMovement);
        }

        public override Skill GetSkill() {
            return new LeechBiteSkill("Leech Bite", 3);
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
    }
}
