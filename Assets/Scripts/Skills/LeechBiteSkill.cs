using RogueGem.Player;
using UnityEngine;
using RogueGem.Utilities;
using RogueGem.Enemies;

namespace RogueGem.Skills {
    public class LeechBiteSkill : SingleSkill {

        public LeechBiteSkill(string name, int damage)
            : base(name, damage) { }
        
        public override void Use(CreatureBehaviour user, Vector2 direction, CreatureType targetType) {
            Vector2 targetPos = (Vector2)user.transform.position + direction;
            if (!WorldController.IsTileEmpty(targetPos)) {
                CreatureBehaviour creature = WorldController.GetGameObjectOnPos(targetPos).GetComponent<CreatureBehaviour>();
                if (creature != null && creature.GetCreatureType().Equals(targetType)) {
					MessagesController.DisplayMessage(Strings.useSkillOn(user.GetName(), GetName(), creature.GetName()));
                    creature.ReceiveDamage(skillDamage);
                    user.Heal(Mathf.FloorToInt(skillDamage / 2));
                }
            }
        }
    }
}
