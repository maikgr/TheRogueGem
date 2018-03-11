using System;
using System.Collections.Generic;
using RogueGem.Items;
using RogueGem.Skills;
using UnityEngine;

namespace RogueGem.Enemies {
    public class BubuggBehaviour : EnemyBehaviour {

        public string creatureName;
        public int maxHealth;
        public int atk;
        public int def;
        public int crit;
        public int sightDistance;
        public int turnToMove;

        private int turnLastMoved;
        public override Vector2 GetDestination() {
            if (IsPlayerInSight()) {
                Vector2 toPlayerGrid = GetPathfinderFirstGrid() - (Vector2)transform.position;
                return new Vector2(toPlayerGrid.x * -1, toPlayerGrid.y * -1);
            } else if (turnPassed - turnLastMoved > turnToMove) {
                turnLastMoved = turnPassed;
                return GetNextRandomGrid();
            }

            return Vector2.zero;
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

        public override int GetSightDistance() {
            return sightDistance;
        }
    }
}
