using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Enemies {
    public abstract class Enemy {
        private int currentHp;

        public Enemy() {
            currentHp = GetMaxHP();
        }

        public abstract string GetName();
        public abstract int GetMaxHP();        
        public abstract int GetATK();
        public abstract int GetDEF();
        public abstract int GetCRIT();
        public abstract GameObject GetPrefab();

        public int GetCurentHP() {
            return currentHp;
        }

        public void RecieveDamage(int damage) {
            int hpPrediction = currentHp - damage;
            if (hpPrediction < 1 && currentHp > 1) {
                currentHp = 1;
            } else {
                currentHp = hpPrediction;
            }
        }
    }
}
