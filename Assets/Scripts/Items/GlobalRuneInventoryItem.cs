using RogueGem.Enemies;
using RogueGem.Player;
using UnityEngine;

namespace RogueGem.Items {
    public class GlobalRuneInventoryItem : InventoryItem {
        private int damage;
        
        public GlobalRuneInventoryItem(Item item) : base(item) {
            GlobalRuneItem rune = item as GlobalRuneItem;
            damage = rune.GetDamage();         
        }

        public override void Use(PlayerBehaviour player) {
            base.Use(player);
            player.ReceiveDamage(damage);
            Debug.Log("You used " + GetName() + ". You feel disoriented...");
            GameObject[] enemiesObject = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enemyObject in enemiesObject) {
                EnemyBehaviour enemy = enemyObject.GetComponent<EnemyBehaviour>();
                Debug.Log(GetName() + " inflicted " + damage + " to " + enemy.GetName());
                enemy.ReceiveDamage(damage);
            }
        }
    }
}
