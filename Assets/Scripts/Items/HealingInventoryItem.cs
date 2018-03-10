using RogueGem.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RogueGem.Items {
    public class HealingInventoryItem : InventoryItem {
        private int healAmount;
        
        public HealingInventoryItem(Item item) : base(item) {
            HealingItem healingItem = item as HealingItem;
            healAmount = healingItem.GetHealAmount();
        }

        public override void Use(PlayerBehaviour player) {
            base.Use(player);
            player.Heal(healAmount);
        }
    }
}
