using UnityEngine;

namespace RogueGem.Items {
    public class ItemFactory {
        public static readonly string BLUEBERRY = "Blueberry";
        public static readonly string REDBERRY = "Redberry";
        public static readonly string ROCK = "Rock";
        public static readonly string ROCK_SPIKY = "Spiky Rock";
        public static readonly string RUNE_THUNDER = "Rune Thunder";        

        public static GameObject GetRandomItem() {
            GameObject[] items = Resources.LoadAll<GameObject>("Prefabs/Items");
            return items[Random.Range(0, items.Length)];
        }

        public static GameObject GetItem(string itemName) {
            return Resources.Load<GameObject>("Prefabs/Items/" + itemName);
        }
    }
}
