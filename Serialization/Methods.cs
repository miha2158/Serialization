using System;
using System.Collections.Generic;

namespace Identificators
{
    [Serializable]
    public class Methods: Identifier
    {
        public Methods(string name): base(name)
        {
            uses = UseCase.Methods;
        }
        public Methods(string name, Types typeReturned): base(name, typeReturned)
        {
            uses = UseCase.Methods;
        }
        public Methods(string name, LinkedList<Tuple<string, Types, Params>> args, Types typeReturned): base(name,
            typeReturned)
        {
            this.args = args;
            uses = UseCase.Methods;
        }

        public LinkedList<Tuple<string, Types, Params>> args = new LinkedList<Tuple<string, Types, Params>>();

    }
}