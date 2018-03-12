using RogueGem.Utilities;
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
            GameObject[] enemiesObject = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enemyObject in enemiesObject) {
                EnemyBehaviour enemy = enemyObject.GetComponent<EnemyBehaviour>();
				MessagesController.DisplayMessage(Strings.inflictDamage(GetName(), damage, enemy.GetName()));
                enemy.ReceiveDamage(damage);
            }
            MessagesController.DisplayMessage(Strings.dizzy(GetName()));
        }
    }
}
