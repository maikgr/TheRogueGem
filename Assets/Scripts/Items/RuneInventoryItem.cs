using RogueGem.Enemies;
using RogueGem.Player;
using UnityEngine;

namespace RogueGem.Items {
    public class RuneInventoryItem : InventoryItem {
        private int damage;
        
        public RuneInventoryItem(Item item) : base(item) {
            RuneItem rune = item as RuneItem;
            damage = rune.GetDamage();         
        }

        public override void Use(PlayerBehaviour player) {
            base.Use(player);
            player.ReceiveDamage(damage);
            GameObject[] enemiesObject = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enemy in enemiesObject) {
                enemy.GetComponent<EnemyBehaviour>().ReceiveDamage(damage);
            }
        }
    }
}
