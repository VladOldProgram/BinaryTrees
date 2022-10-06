using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTrees
{
    public class BinaryTree<T> : IEnumerable<T> 
        where T : IComparable
    {
        private BinaryTree<T> left;
        private BinaryTree<T> right;
        private T value;
        private int count = 0;
        private bool isInitialized = false;

        public BinaryTree(T value)
        {
            this.value = value;
            count++;
            isInitialized = true;
        }

        public BinaryTree()
        {
            isInitialized = false;
        }

        public void Add(T value)
        {
            if (!isInitialized)
            {
                this.value = value;
                count++;
                isInitialized = true;
                return;
            }

            BinaryTree<T> subtree = this;
            while (true)
            {
                subtree.count++;
                if (subtree.value.CompareTo(value) > 0)
                {
                    if (subtree.left != null)
                        subtree = subtree.left;
                    else
                    {
                        subtree.left = new BinaryTree<T>(value);
                        break;
                    }
                }
                else
                {
                    if (subtree.right != null)
                        subtree = subtree.right;
                    else
                    {
                        subtree.right = new BinaryTree<T>(value);
                        break;
                    }
                }
            }
        }

        public bool Contains(T value)
        {
            if (!isInitialized) 
                return false;

            BinaryTree<T> subtree = this;
            while (true)
            {
                int comparisonResult = subtree.value.CompareTo(value);
                if (comparisonResult == 0)
                    return true;
                if (comparisonResult > 0)
                {
                    if (subtree.left != null)
                        subtree = subtree.left;
                    else
                        return false;
                }
                else
                {
                    if (subtree.right != null)
                        subtree = subtree.right;
                    else
                        return false;
                }
            }
        }

        public T this[int i]
        {
            get
            {
                BinaryTree<T> subtree = this;
                int count = 0;
                while (true)
                {
                    int currentNodeIndex = 0;
                    if (subtree.left == null)
                        currentNodeIndex += count;
                    else currentNodeIndex += subtree.left.count + count;

                    if (i == currentNodeIndex)
                        return subtree.value;

                    if (i < currentNodeIndex)
                        subtree = subtree.left;
                    else
                    {
                        subtree = subtree.right;
                        count = currentNodeIndex + 1;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetEnumeratorForNode(this);
        }

        IEnumerator<T> GetEnumeratorForNode(BinaryTree<T> subtree)
        {
            if (subtree == null || !subtree.isInitialized)
                yield break;

            var enumerator = GetEnumeratorForNode(subtree.left);
            while (enumerator.MoveNext())
                yield return enumerator.Current;

            yield return subtree.value;

            enumerator = GetEnumeratorForNode(subtree.right);
            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }
    }
}
