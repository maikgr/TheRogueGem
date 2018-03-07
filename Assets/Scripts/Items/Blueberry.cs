using RogueGem.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RogueGem.Items {
    public class Blueberry : HealingItem {
        public string itemName = "Blueberry";
        public int amount = 1;
        public int healAmount = 5;

        public override int GetAmount() {
            return amount;
        }

        public override string GetName() {
            return itemName; 
        }

        public override int GetHealingAmount(out int turnsInEffect) {
            turnsInEffect = 1;
            return healAmount;
        }
    }
}
