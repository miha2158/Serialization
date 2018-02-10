using System;

namespace Identificators {
    [Serializable]
    public abstract class Identifier: IComparable<Identifier>
    {
        public string name;
        public int hash => name.GetHashCode();

        public Types typeReturned = Types._void;
        public UseCase uses;
        
        protected Identifier(string name)
        {
            this.name = name;
        }
        protected Identifier(string name,Types typeReturned): this(name)
        {
            this.typeReturned = typeReturned;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Identifier))
                return false;
            var o = (Identifier) obj;
            return name.Equals(o.name) && typeReturned == o.typeReturned && uses.Equals(o.uses);
        }
        public int CompareTo(Identifier other)
        {
            return Math.Sign(hash - other.hash);
        }
        public override string ToString()
        {
            return name;
        }
        public override int GetHashCode()
        {
            return hash;
        }
    }
}