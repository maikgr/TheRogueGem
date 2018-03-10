using RogueGem.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RogueGem.Items {
    public class ThrowingItem : Item {
        public string itemName;
        public int itemAmount;
        public int damage;
        public int distance;

        public override int GetAmount() {
            return itemAmount;
        }

        public override ItemType GetItemType() {
            return ItemType.Throwing;
        }

        public override string GetName() {
            return itemName;
        }

        public ThrowingSkill GetSkill() {
            return new ThrowingSkill(itemName, damage, distance);
        }

        public override InventoryItem ToInventoryItem() {
            return new ThrowingInventoryItem(this);
        }
    }
}
