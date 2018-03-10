using RogueGem.Player;
using UnityEngine;
using RogueGem.Utilities;
using RogueGem.Enemies;

namespace RogueGem.Skills {
    public class LeechBiteSkill : SingleSkill {

        public LeechBiteSkill(string name, int damage)
            : base(name, damage) { }
        
        public override void Use(PlayerBehaviour player, Vector2 direction) {
            Vector2 targetPos = (Vector2)player.transform.position + direction;
            if (!WorldController.IsTileEmpty(targetPos)) {
                EnemyBehaviour enemy = WorldController.GetGameObjectOnPos(targetPos).GetComponent<EnemyBehaviour>();
                if (enemy != null) {
                    enemy.ReceiveDamage(skillDamage);
                    player.Heal(Mathf.FloorToInt(skillDamage / 2));
                }
            }
        }
    }
}
