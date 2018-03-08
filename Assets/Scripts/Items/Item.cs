using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RogueGem.Items {
    public abstract class Item : MonoBehaviour {
        public abstract string GetName();
        public abstract int GetAmount();
        public abstract void SetAmount(int amount);
    }
}
