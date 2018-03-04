using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RogueGem.Items {
    public interface IItem {
        string GetName();
        string GetPrefab();
        void UseEffect(GameObject entity);
        void UseEffect(List<GameObject> entity);
        int GetAmount();
    }
}
