using System;
using System.Xml.Serialization;
using static Serialization.Parse;

namespace Serialization
{
    [XmlInclude(typeof(Vars))]
    [Serializable]
    public class Vars: Identifier
    {
        public readonly object value;

        public Vars() : base() { }
        public Vars(string name): base(name)
        {
            uses = UseCase.Vars;
        }
        public Vars(string name, Types type): base(name, type)
        {
            uses = UseCase.Vars;
        }
        public Vars(string name, Types type, dynamic value): base(name, type)
        {
            if (Type(value.GetType().ToString()) == typeReturned)
                this.value = value;
            else
                throw new TypeInitializationException("Введён  аргумент не правильного типа", new Exception());
            uses = UseCase.Vars;
        }

        public override string ToString()
        {
            string result = base.ToString();

            if (value == null)
                return result + ";";

            result += " = ";
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