﻿using System;
using System.Collections.Generic;
using RogueGem.Items;
using RogueGem.Skills;
using UnityEngine;

namespace RogueGem.Enemies {
    public class SlimeBehaviour : EnemyBehaviour {

        public string creatureName;
        public int maxHealth;
        public int atk;
        public int def;
        public int crit;
        public int sightDistance;

        public override Vector2 GetDestination() {
            if (IsPlayerInSight()) {
                Vector2 toPlayerGrid = GetPathfinderFirstGrid() - (Vector2)transform.position;
                return toPlayerGrid;
            }
            return GetNextRandomGrid();
        }

        public override void AttackPlayer() {
            Attack(GetDestination());
        }

        public override bool IsInAttackRange() {
            return GetPathfinderFirstGrid() == (Vector2)player.transform.position;
        }

        public override Skill GetSkill() {
            return new AbsorbSkill("Absorb", 0);
        }

        public override int GetATK() {
            int damage = player.GetDEF() + 1;
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
            return sightDistance;
        }
    }
}
