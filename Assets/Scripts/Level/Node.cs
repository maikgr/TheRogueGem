using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RogueGem.Level {
    public class Node {
        public bool isFloor;
        public Vector2 position;
        public int gCost;
        public int hCost;        

        public Node(bool isFloor, Vector2 position) {
            this.isFloor = isFloor;
            this.position = position;
        }

        public int GetFCost() {
            return gCost + hCost;
        }
    }
}
