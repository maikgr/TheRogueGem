using System;
using RogueGem.Player;
using UnityEngine;
using RogueGem.Utilities;
using RogueGem.Enemies;

namespace RogueGem.Skills {
    public class StickyWebSkill : LinearSkill {
        private int rootTurnsLength;

        public StickyWebSkill(string name, int damage, int distance, int rootTurnsLength)
            : base(name, damage, distance) {
            this.rootTurnsLength = rootTurnsLength;
        }

        public override void Use(PlayerBehaviour player, Vector2 direction) {
            Vector2 attackGridPos = player.transform.position;
            for (int i = 0; i < distance; ++i) {
                attackGridPos += direction;
                if (!WorldController.IsTileEmpty(attackGridPos)) {
                    EnemyBehaviour enemy = WorldController.GetGameObjectOnPos(attackGridPos).GetComponent<EnemyBehaviour>();
                    if (enemy != null) {
                        enemy.Root(rootTurnsLength);
                        break;
                    }
                }
            }
        }
    }
}
