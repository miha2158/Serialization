using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Serialization
{
    [XmlInclude(typeof(Tuple))]
    [Serializable]
    public class Tuple
    {
        public string Item1;
        public Types Item2;
        public Params Item3;

        public Tuple()
        {
            Item1 = string.Empty;
        }
        public Tuple(string Item1, Types Item2, Params Item3)
        {
            this.Item1 = Item1;
            this.Item2 = Item2;
            this.Item3 = Item3;
        }
    }

    [XmlInclude(typeof(Methods))]
    [Serializable]
    public class Methods: Identifier
    {
        public Methods(): base() { }
        public Methods(string name): base(name)
        {
            uses = UseCase.Methods;
        }
        public Methods(string name, Types typeReturned): base(name, typeReturned)
        {
            uses = UseCase.Methods;
        }
        public Methods(string name, List<Tuple> args, Types typeReturned): base(name,
            typeReturned)
        {
            this.args = args;
            uses = UseCase.Methods;
        }

        public List<Tuple> args = new List<Tuple>(0);

        public override string ToString()
        {
            var result = base.ToString() + " ( ";

            foreach (var t in args)
            {
                if (t.Item3 != Params._val)
                    result += $"{t.Item3.ToString().Substring(1)}";
                result += $" {t.Item2.ToString().Substring(1)} {t.Item1},";
            }

            result = result.Substring(0, result.Length - 1) + ");";

            return result;
        }
    }
}