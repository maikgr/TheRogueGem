﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RogueGem.Items {
    class RuneItem : Item {
        public string itemName;
        public int itemAmount;
        public int itemDamage;

        public override int GetAmount() {
            return itemAmount;
        }

        public override ItemType GetItemType() {
            return ItemType.Rune;
        }

        public override string GetName() {
            return name;
        }

        public override InventoryItem ToInventoryItem() {
            return new RuneInventoryItem(this);
        }

        public int GetDamage() {
            return itemDamage;
        }
    }
}
