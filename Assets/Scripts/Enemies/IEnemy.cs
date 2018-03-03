using UnityEngine;

namespace RogueGem.Enemies {
    public interface IEnemy {
        string GetName();
        int GetMaxHP();
        int GetCurentHP();
        int GetATK();
        int GetDEF();
        int GetCRIT();
        GameObject GetPrefab();
        void RecieveDamage(int damage);
    }
}
