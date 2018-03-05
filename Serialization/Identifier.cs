using System;
using System.Xml.Serialization;

namespace Serialization
{
    [XmlInclude(typeof(Consts))]
    [XmlInclude(typeof(Vars))]
    [XmlInclude(typeof(Classes))]
    [XmlInclude(typeof(Methods))]
    [Serializable]
    public abstract class Identifier: IComparable<Identifier>
    {
        public string name;
        public int hash => name.GetHashCode();

        public Types typeReturned = Types._void;
        public UseCase uses;

        protected Identifier(){ }
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
            return $"{typeReturned.ToString().Substring(1)} {name}";
        }
        public override int GetHashCode()
        {
            return hash;
        }
    }
}