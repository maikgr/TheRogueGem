using System;
using RogueGem.Enemies;
using RogueGem.Player;
using RogueGem.Utilities;
using UnityEngine;

namespace RogueGem.Skills {
    public class ThrowingSkill : Skill {
        private int distance;

        public ThrowingSkill(string name, int damage, int distance)
            : base(name, damage) {
            this.distance = distance;
        }
     
        public int GetDistance() {
            return distance;
        }

        public override SkillArea GetSkillArea() {
            return SkillArea.Linear;
        }

        public override void Use(PlayerBehaviour player, Vector2 direction) {
            Vector2 attackGridPos = player.transform.position;
            for (int i = 0; i < distance; ++i) {
                attackGridPos += direction;
                if (!WorldController.IsTileEmpty(attackGridPos)) {
                    EnemyBehaviour enemy = WorldController.GetGameObjectOnPos(attackGridPos).GetComponent<EnemyBehaviour>();
                    if (enemy != null) {
                        enemy.ReceiveDamage(skillDamage);
                        break;
                    }
                }
            }
        }
    }
}
