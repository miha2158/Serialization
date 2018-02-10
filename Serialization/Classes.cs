using System;

namespace Identificators
{
    [Serializable]
    public class Classes: Identifier
    {
        public Classes(string name): base(name)
        {
            typeReturned = Types._class;
            uses = UseCase.Classes;
        }
    }
}