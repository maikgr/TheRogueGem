using System.Collections.Generic;
using System.Linq;
using System.Text;
using RogueGem.Items;
using UnityEngine;

namespace RogueGem.Enemies {
    public class Skully : ICreature {
        public override int GetATK() {
            return 2;
        }

        public override int GetCRIT() {
            return 0;
        }

        public override int GetDEF() {
            return 0;
        }

        public override IEnumerable<IItem> GetItemLoot() {
            return null;
        }

        public override int GetMaxHP() {
            return 5;
        }

        public override string GetName() {
            return "Skully";
        }

        public override GameObject GetPrefab() {
            return Resources.Load("Prefabs/Skully") as GameObject;
        }

        public override Vector2 GetDestination() {
            int xMovement = Random.Range(-1, 2);
            int yMovement = xMovement.Equals(0) ? Random.Range(-1, 2) : 0;
            return new Vector2(xMovement, yMovement);
        }
    }
}
