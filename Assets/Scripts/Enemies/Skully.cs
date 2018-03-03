using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RogueGem.Enemies {
    public class Skully : IEnemy {
        
        private int currentHP;

        public Skully() {
            currentHP = GetMaxHP();
        }

        public string GetName() {
            return "Skully";
        }

        public GameObject GetPrefab() {
            return Resources.Load("Prefabs/Skully") as GameObject;
        }

        public int GetMaxHP() {
            return 3;
        }

        public int GetCurentHP() {
            return currentHP;
        }

        public int GetATK() {
            return 1;
        }

        public int GetDEF() {
            return 0;
        }

        public int GetCRIT() {
            return 0;
        }

        public void RecieveDamage(int damage) {
            int hpLeft = currentHP - damage;
            if (hpLeft < 0 && currentHP > 1) {
                currentHP = 1;
            } else {
                currentHP = 0;
            }
        }
    }
}
