using System;
using RogueGem.Enemies;
using RogueGem.Player;
using RogueGem.Utilities;
using UnityEngine;

namespace RogueGem.Skills {
    public class PierceSkill : LinearSkill {

        public PierceSkill(string name, int damage, int distance)
            : base(name, damage, distance) { }

        public override void Use(PlayerBehaviour player, Vector2 direction) {
            Vector2 attackGridPos = player.transform.position;
            for (int i = 0; i < distance; ++i) {
                attackGridPos += direction;
                if (!WorldController.IsTileEmpty(attackGridPos)) {
                    EnemyBehaviour enemy = WorldController.GetGameObjectOnPos(attackGridPos).GetComponent<EnemyBehaviour>();
                    if (enemy != null) {
                        enemy.ReceiveDamage(skillDamage);
                    }
                }
            }
        }
    }
}
