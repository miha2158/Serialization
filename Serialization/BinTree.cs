using System;

namespace Identificators
{
    [Serializable]
    public class BinTree <T> where T:IComparable<T>
    {
        public BinTree<T> right { get; private set; } = null;
        public BinTree<T> left { get; private set; } = null;

        public T data;

        public BinTree() { }
        public BinTree (T data)
        {
            this.data = data;
        }
        public BinTree(T data, BinTree<T> right, BinTree<T> left): this(data)
        {
            this.right = right;
            this.left = left;
        }
        public BinTree(BinTree<T> left, T data): this(data, null, left) { }
        public BinTree(T data, BinTree<T> right): this(data, right, null) { }
        
        public void Add(T data)
        {
            if (this.data == null || this.data.Equals(default(T)))
            {
                this.data = data;
                return;
            }
            if (data.CompareTo(this.data) < 0)
                if (right == null)
                    NewRight(data);
                else
                    right.Add(data);
            else if (left == null)
                NewLeft(data);
            else
                left.Add(data);
        }

        public void NewRight(T data)
        {
            right = new BinTree<T>(data);
        }
        public void NewLeft(T data)
        {
            left = new BinTree<T>(data);
        }
    }
}