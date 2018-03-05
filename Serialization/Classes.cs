using System;
using System.Xml.Serialization;

namespace Serialization
{
    [XmlInclude(typeof(Classes))]
    [Serializable]
    public class Classes: Identifier
    {
        public Classes(): base() { }
        public Classes(string name): base(name)
        {
            typeReturned = Types._class;
            uses = UseCase.Classes;
        }

        public override string ToString()
        {
            return base.ToString() + ";";
        }
    }
}