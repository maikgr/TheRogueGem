using RogueGem.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RogueGem.Items {
    public class HealingItem : Item {

        public string itemName;
        public int itemAmount;
        public int healingAmount;

        public override int GetAmount() {
            return itemAmount;
        }

        public override ItemType GetItemType() {
            return ItemType.Healing;
        }

        public override string GetName() {
            return itemName;
        }

        public int GetHealAmount() {
            return healingAmount;
        }

        public override InventoryItem ToInventoryItem() {
            return new HealingInventoryItem(this);
        }
    }
}
