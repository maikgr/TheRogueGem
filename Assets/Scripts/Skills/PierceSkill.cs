using System;
using RogueGem.Enemies;
using RogueGem.Player;
using RogueGem.Utilities;
using UnityEngine;

namespace RogueGem.Skills {
    public class PierceSkill : LinearSkill {

        public PierceSkill(string name, int damage, int distance)
            : base(name, damage, distance) { }

        public override void Use(CreatureBehaviour user, Vector2 direction, CreatureType targetType) {
            Vector2 attackGridPos = user.transform.position;
            for (int i = 0; i < distance; ++i) {
                attackGridPos += direction;
                if (!WorldController.IsTileEmpty(attackGridPos)) {
                    CreatureBehaviour creature = WorldController.GetGameObjectOnPos(attackGridPos).GetComponent<CreatureBehaviour>();
                    if (creature != null && creature.GetCreatureType().Equals(targetType)) {
						MessagesController.DisplayMessage(Strings.useSkillOn(user.GetName(), GetName(), creature.GetName()));
                        creature.ReceiveDamage(skillDamage);                        
                    }
                }
            }
        }
    }
}
