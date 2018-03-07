using RogueGem.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RogueGem.Items {
    abstract class RuneItem : Item {
        public abstract IEnumerable<Vector2> GetArea();
        public abstract int GetDamage();
    }
}
