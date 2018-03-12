using System;
using System.Collections.Generic;
using RogueGem.Items;
using RogueGem.Skills;
using UnityEngine;

namespace RogueGem.Enemies {
    public class PorkyBehaviour : EnemyBehaviour {

        public string creatureName;
        public int maxHealth;
        public int atk;
        public int def;
        public int crit;
        public int sightDistance;
        public int skillCooldown;

        private int skillDelay;

        public override Vector2 GetDestination() {
            if (IsPlayerInSight()) {
                Vector2 toPlayerGrid = GetPathfinderFirstGrid() - (Vector2)transform.position;
                return toPlayerGrid;
            }
            return GetNextRandomGrid();
        }

        public override void AttackPlayer() {
            if (skillDelay.Equals(0)) {
                GetSkill().Use(this, GetPathfinderFirstGrid() - (Vector2)transform.position, CreatureType.Player);
                skillDelay = skillCooldown;
            } else {
                Attack(GetDestination());
                skillDelay = Mathf.Max(0, --skillDelay);
            }
        }

        public override bool IsInAttackRange() {
            return GetPathfinderFirstGrid() == (Vector2)player.transform.position;
        }

        public override Skill GetSkill() {
            return new LeechBiteSkill("Drain", GetATK());
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

        public override IDictionary<string, int> GetItemDropChance() {
            Dictionary<string, int> itemDropChance = new Dictionary<string, int>();
            itemDropChance.Add(ItemFactory.ROCK_SPIKY, 75);
            itemDropChance.Add(ItemFactory.BLUEBERRY, 25);
            return itemDropChance;
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
