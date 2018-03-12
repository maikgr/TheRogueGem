using System;
using System.Collections.Generic;
using RogueGem.Items;
using RogueGem.Skills;
using UnityEngine;

namespace RogueGem.Enemies {
    public class BossMayhocBehaviour : EnemyBehaviour {

        public string creatureName;
        public int maxHealth;
        public int atk;
        public int def;
        public int crit;
        public int skillCooldown;
        public int skillDistance;
        public int moveEveryTurn;

        private int skillDelay;
        private bool isInLongRange;
        private int moveDelay;

        public override Vector2 GetDestination() {
            return GetPathfinderFirstGrid() - (Vector2)transform.position;
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
            if (skillDelay.Equals(0)) {
                int xDist = (int)Mathf.Abs(player.transform.position.x - transform.position.x);
                int yDist = (int)Mathf.Abs(player.transform.position.y - transform.position.y);

                isInLongRange = ((xDist.Equals(0) && yDist < skillDistance)
                                || (yDist.Equals(0) && xDist < skillDistance));
            }

            return isInLongRange || GetPathfinderFirstGrid() == (Vector2)player.transform.position;
        }

        public override Skill GetSkill() {
            if (isInLongRange) {
                int dice = UnityEngine.Random.Range(0, 2);
                switch (dice) {
                    case 0:
                        return new PierceSkill("Fire Breath", GetATK() * 2, skillDistance);
                    default:
                        return new ThrowingSkill("Fireball", GetATK(), skillDistance);
                }
            }
            return new HeavyPunchSkill("Dragon Claw", Mathf.FloorToInt(GetATK() * 1.5f));
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
            itemDropChance.Add(ItemFactory.REDBERRY, 75);
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
            return 0;
        }
    }
}
