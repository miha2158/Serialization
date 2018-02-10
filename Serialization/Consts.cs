using System;
using System.Collections.Generic;

namespace Identificators
{
    using static Extenisons;

    [Serializable]
    public class Consts: Identifier
    {
        public readonly object value;

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
    }
}