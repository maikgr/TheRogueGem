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

        public override void Use(CreatureBehaviour user, Vector2 direction, CreatureType targetType) {
            Vector2 attackGridPos = user.transform.position;
            for (int i = 0; i < distance; ++i) {
                attackGridPos += direction;
                if (!WorldController.IsTileEmpty(attackGridPos)) {
                    CreatureBehaviour creature = WorldController.GetGameObjectOnPos(attackGridPos).GetComponent<CreatureBehaviour>();
                    if (creature != null && creature.GetCreatureType().Equals(targetType)) {
                        Debug.Log(user.GetName() + " used " + GetName() + " to " + creature.GetName());
                        creature.Root(rootTurnsLength);
                        break;
                    }
                }
            }
        }
    }
}
