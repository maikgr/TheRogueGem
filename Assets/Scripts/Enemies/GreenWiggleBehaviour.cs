using System;
using System.Collections.Generic;
using RogueGem.Items;
using RogueGem.Skills;
using UnityEngine;

namespace RogueGem.Enemies {
    public class GreenWiggleBehaviour : EnemyBehaviour {

        public string creatureName;
        public int maxHealth;
        public int atk;
        public int def;
        public int crit;
        public int skillCooldown;
        public int skillDistance;
        public int rootTurnsLength;

        private int skillDelay;

        public override Vector2 GetDestination() {
            Vector2 destination = GetPathfinderFirstGrid();
            return destination - (Vector2)transform.position;
        }

        public override Skill GetSkill() {
            return new StickyWebSkill("Sticky Slime", GetATK(), skillDistance, rootTurnsLength);
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
            itemDropChance.Add(ItemFactory.BLUEBERRY, 25);
            itemDropChance.Add(ItemFactory.BLUEBERRY, 75);
            return itemDropChance;
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

        public override void AttackPlayer() {
            if (skillDelay.Equals(0)) {
                GetSkill().Use(this, GetPathfinderFirstGrid() - (Vector2)transform.position, CreatureType.Player);
                skillDelay = skillCooldown;
            } else {
                Attack(GetDestination());
            }
        }

        public override bool IsInAttackRange() {
            if (skillDelay.Equals(0)) {
                int xDist = (int)Mathf.Abs(player.transform.position.x - transform.position.x);
                int yDist = (int)Mathf.Abs(player.transform.position.y - transform.position.y);

                return ((xDist.Equals(0) && yDist < skillDistance)
                        || (yDist.Equals(0) && xDist < skillDistance));
            } else {
                skillDelay = Mathf.Max(0, --skillDelay);
            }
            return GetPathfinderFirstGrid() == (Vector2)player.transform.position;
        }
    }
}
