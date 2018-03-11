using System.Collections.Generic;
using RogueGem.Items;
using RogueGem.Skills;
using UnityEngine;

namespace RogueGem.Enemies {
    public class SkullyBehaviour : EnemyBehaviour {

        public string creatureName;
        public int maxHealth;
        public int atk;
        public int def;
        public int crit;
        public int sightDistance;

        public override int GetSightDistance() {
            return sightDistance;
        }

        public override void AttackPlayer() {
            Attack(GetDestination());
        }

        public override bool IsInAttackRange() {
            return GetPathfinderFirstGrid() == (Vector2)player.transform.position;
        }

        public override Vector2 GetDestination() {
            if (IsPlayerInSight()) {
                Vector2 destination = GetPathfinderFirstGrid();
                return destination - (Vector2)transform.position;
            } else {
                return GetNextRandomGrid();
            }
        }

        public override Skill GetSkill() {
            return new ThrowingSkill("Rock Throw", 2, 3);
        }

        public override int GetATK() {
            int damage = UnityEngine.Random.Range(0, 100) < GetCRIT() ? Mathf.FloorToInt(atk * 1.5f) : atk;
            return damage;
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
