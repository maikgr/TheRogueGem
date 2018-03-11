using RogueGem.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RogueGem.Level {
    public class Node : IHeapIndex<Node> {
        public bool isFloor;
        public Vector2 position;
        public int gCost;
        public int hCost;
        public Node parentNode;

        private int heapIndex;

        public int HeapIndex {
            get { return heapIndex; }
            set { heapIndex = value; }
        }

        public int fCost { get { return gCost + hCost; } }

        public Node(bool isFloor, Vector2 position) {
            this.isFloor = isFloor;
            this.position = position;
        }

        public int CompareTo(Node other) {
            int compareResult = fCost.CompareTo(other.fCost);
            if (compareResult == 0) {
                compareResult = hCost.CompareTo(other.hCost);
            }
            return -compareResult;
        }
    }
}
