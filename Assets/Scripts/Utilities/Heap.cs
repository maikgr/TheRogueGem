using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RogueGem.Utilities {
    public class Heap<T> where T : IHeapIndex<T> {

        T[] items;
        int currentItemCount;

        public Heap(int maxSize) {
            items = new T[maxSize];
        }

        public int Count { get { return currentItemCount; } }

        public void Add(T item) {
            item.HeapIndex = currentItemCount;
            items[currentItemCount] = item;
            SortUp(item);
            ++currentItemCount;
        }

        public T RemoveFirst() {
            T firstItem = items[0];
            --currentItemCount;
            items[0] = items[currentItemCount];
            items[0].HeapIndex = 0;
            SortDown(items[0]);
            return firstItem;
        }

        public void UpdateItem(T item) {
            SortUp(item);
        }

        public bool Contains(T item) {
            return Equals(items[item.HeapIndex], item);
        }

        private void SortUp(T item) {
            int parentIndex;
            while (true) {
                parentIndex = (item.HeapIndex - 1) / 2;
                T parentItem = items[parentIndex];

                if (item.CompareTo(parentItem) > 0) {
                    Swap(item, parentItem);
                } else {
                    break;
                }
            }
        }

        private void SortDown(T item) {
            int childIndexLeft;
            int childIndexRight;
            int indexToBeSwapped;

            while (true) {
                childIndexLeft = item.HeapIndex * 2 + 1;
                childIndexRight = item.HeapIndex * 2 + 2;
                indexToBeSwapped = 0;

                if (childIndexLeft < currentItemCount) {
                    indexToBeSwapped = childIndexLeft;

                    if (childIndexRight < currentItemCount
                        && items[childIndexLeft].CompareTo(items[childIndexRight]) < 0) {
                        indexToBeSwapped = childIndexRight;
                    }

                    if (item.CompareTo(items[indexToBeSwapped]) < 0) {
                        Swap(item, items[indexToBeSwapped]);
                    } else {
                        return;
                    }
                } else {
                    return;
                }
            }
        }

        private void Swap(T firstItem, T secondItem) {
            int firstHeapIndex = firstItem.HeapIndex;
            items[firstItem.HeapIndex] = secondItem;
            items[secondItem.HeapIndex] = firstItem;
            firstItem.HeapIndex = secondItem.HeapIndex;
            secondItem.HeapIndex = firstHeapIndex;
        }
    }

    public interface IHeapIndex<T> : IComparable<T> {
        int HeapIndex { get; set; }
    }
}
