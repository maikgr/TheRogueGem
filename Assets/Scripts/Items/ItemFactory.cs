using UnityEngine;

namespace RogueGem.Items {
    public class ItemFactory {
        public static GameObject GetRandomItem() {
            GameObject[] items = Resources.LoadAll<GameObject>("Prefabs/Items");
            return items[Random.Range(0, items.Length)];
        }
    }
}
