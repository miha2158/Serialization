using System;
using System.Xml.Serialization;
using static Serialization.Extenisons;

namespace Serialization
{
    [XmlInclude(typeof(Consts))]
    [Serializable]
    public class Consts: Identifier
    {
        public object value;

        public Consts(): base() { }
        public Consts(string name): base(name)
        {
            uses = UseCase.Consts;
        }
        public Consts(string name, Types type): base(name, type)
        {
            uses = UseCase.Consts;
        }
        public Consts(string name, Types type, dynamic value): base(name, type)
        {
            if (ParseType(value.GetType().ToString()) == typeReturned)
                this.value = value;
            else throw new TypeInitializationException("Введён  аргумент не правильного типа",new Exception());
            uses = UseCase.Consts;
        }

        public override bool Equals(object obj)
        {
            var o = obj as Consts;
            return base.Equals(obj) && value.Equals(o.value);
        }

        public override string ToString()
        {
            string result = $"const {base.ToString()} = ";

            switch (typeReturned)
            {
                case Types._string:
                    result += $"\"{value}\"";
                    break;
                case Types._char:
                    result += $"'{value}'";
                    break;
                case Types._int:
                    result += $"{int.Parse(value.ToString())}";
                    break;
                case Types._float:
                    result += $"{float.Parse(value.ToString())}";
                    break;
                case Types._bool:
                    result += $"{bool.Parse(value.ToString())}";
                    break;
            }

            result += ";";

            return result;
        }
    }
}