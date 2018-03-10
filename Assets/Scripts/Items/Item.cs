using UnityEngine;

namespace RogueGem.Items {
    public abstract class Item : MonoBehaviour {
        public abstract string GetName();
        public abstract int GetAmount();
        public abstract ItemType GetItemType();
        public abstract InventoryItem ToInventoryItem();
    }

    public enum ItemType {
        Healing,
        Throwing,
        Rune
    }
}
