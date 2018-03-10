using RogueGem.Enemies;
using RogueGem.Player;
using RogueGem.Skills;
using UnityEngine;

namespace RogueGem.Items {
    public class ThrowingInventoryItem : InventoryItem {
        private ThrowingSkill skill;
        
        public ThrowingInventoryItem(Item item) : base(item) {
            ThrowingItem throwingItem = item as ThrowingItem;
            skill = throwingItem.GetSkill();
        }

        public override void Use(PlayerBehaviour player) {
            base.Use(player);
        }

        public ThrowingSkill GetSkill() {
            return skill;
        }
    }
}
