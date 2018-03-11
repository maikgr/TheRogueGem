using RogueGem.Items;
using RogueGem.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace RogueGem.Utilities {
    public class UIBehaviour : MonoBehaviour {

        public Text health;
        public Text skill;
        public GameObject groundSlot;
        public GameObject[] inventorySlot;
        public Texture emptyImage;
        public GameObject attackGridPrefab;
        public GameObject pathGridPrefab;

        private RawImage[] inventorySlotImage;
        private Text[] inventorySlotText;
        private RawImage groundSlotImage;
        private Text groundSlotText;
        private GameObject parentFx;
        private GameObject parentPath;

        void Start() {
            inventorySlotImage = new RawImage[inventorySlot.Length];
            inventorySlotText = new Text[inventorySlot.Length];
            for (int i = 0; i < inventorySlot.Length; ++i) {
                inventorySlotImage[i] = inventorySlot[i].transform.Find("Content").GetComponent<RawImage>();
                inventorySlotText[i] = inventorySlot[i].transform.Find("Quantity").GetComponent<Text>();
            }

            groundSlotImage = groundSlot.transform.Find("Content").GetComponent<RawImage>();
            groundSlotText = groundSlot.transform.Find("Quantity").GetComponent<Text>();
        }

        public void SetSkill(string skillName) {
            skill.text = skillName;
        }

        public void UpdateInventory(int index, InventoryItem item) {
            inventorySlotImage[index].texture = item.GetImage();
            inventorySlotText[index].text = "x" + item.GetAmount().ToString();
        }

        public void EmptyInventory(int index) {
            inventorySlotImage[index].texture = emptyImage;
            inventorySlotText[index].text = string.Empty;
        }

        public void UpdateGroundSlot(Item item) {
            groundSlotImage.texture = item.gameObject.GetComponent<SpriteRenderer>().sprite.texture;
            groundSlotText.text = "x" + item.GetAmount().ToString();
        }

        public void RemoveItemOnGround() {
            if (groundSlotImage.texture != emptyImage) {
                groundSlotImage.texture = emptyImage;
                groundSlotText.text = string.Empty;
            }
        }

        public void UpdateHealth(PlayerBehaviour player) {
            health.text = player.GetCurentHP() + "/" + player.GetMaxHP();
        }

        public void LinearAttack(Vector2 attackerPos, Vector2 direction, int distance) {
            if (parentFx != null) {
                CancelAttack();
            }
            CreateAttackParent(attackerPos);
            Vector2 attackGridPos = attackerPos;
            for (int i = 0; i < distance; ++i) {
                attackGridPos += direction;
                Instantiate(attackGridPrefab, attackGridPos, Quaternion.identity, parentFx.transform);
            }
        }

        public void SingleAttack(Vector2 attackerPos, Vector2 direction) {
            if (parentFx != null) {
                CancelAttack();
            }
            CreateAttackParent(attackerPos);
            Vector2 attackGridPos = attackerPos + direction;
            Instantiate(attackGridPrefab, attackGridPos, Quaternion.identity, parentFx.transform);
        }

        public void CancelAttack() {
            Destroy(parentFx);
            parentFx = null;
        }

        private void CreateAttackParent(Vector2 pos) {
            parentFx = new GameObject("AttackArea");
            parentFx.transform.position = pos;
        }
    }
}
