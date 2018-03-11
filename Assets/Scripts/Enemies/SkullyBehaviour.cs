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
        public int skillCooldown;
        public int skillDistance;
        private int skillDelay;

        public override int GetSightDistance() {
            return sightDistance;
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

        public override Vector2 GetDestination() {
            if (IsPlayerInSight()) {
                Vector2 destination = GetPathfinderFirstGrid();
                return destination - (Vector2)transform.position;
            } else {
                return GetNextRandomGrid();
            }
        }

        public override Skill GetSkill() {
            return new ThrowingSkill("Rock Throw", GetATK(), skillDistance);
        }

        public override int GetATK() {
            atk = player.GetDEF() + 1;
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
