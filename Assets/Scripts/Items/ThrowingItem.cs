using RogueGem.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RogueGem.Items {
    abstract class ThrowingItem : Item {
        public abstract int GetDamage();
        public abstract int ThrowDistance();
        public abstract void OnEnemyHit(EnemyBehaviour enemy);
    }
}
