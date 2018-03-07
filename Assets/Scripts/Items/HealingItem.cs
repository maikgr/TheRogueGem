using RogueGem.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RogueGem.Items {
    public abstract class HealingItem : Item {
        public abstract int GetHealingAmount(out int turnsInEffect);
    }
}
