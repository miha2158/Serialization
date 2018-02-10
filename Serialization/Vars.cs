using System;

namespace Identificators
{
    using static Extenisons;

    [Serializable]
    public class Vars: Identifier
    {
        public readonly object value;

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
            if (ParseType(value.GetType().ToString()) == typeReturned)
                this.value = value;
            else
                throw new TypeInitializationException("Введён  аргумент не правильного типа", new Exception());
            uses = UseCase.Vars;
        }
    }
}