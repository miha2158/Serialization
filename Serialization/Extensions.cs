using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identificators
{
    public static class Extenisons
    {
        static void MakeTree(string[] strings)
        {
            var tree = new BinTree<Identifier>();
            foreach (string s in strings)
                tree.Add(ParseIdentifier(s));
        }

        public static Types ParseType(string s)
        {
            s = s.Trim();
            switch (s)
            {
                case "int":
                case "System.Int32":
                    return Types._int;
                case "System.Single":
                case "float":
                    return Types._float;
                case "System.Boolean":
                case "bool":
                    return Types._bool;
                case "System.Char":
                case "char":
                    return Types._char;
                case "System.String":
                case "string":
                    return Types._string;
                case "class":
                    return Types._class;
                case "void":
                    return Types._void;
                default:
                    return Types._;
            }
        }
        public static object ParseTo(this string s, Types t)
        {
            switch (t)
            {
                case Types._int:
                    return int.Parse(s);
                case Types._float:
                    return float.Parse(s);
                case Types._bool:
                    return bool.Parse(s);
                case Types._char:
                    return char.Parse(s);
                case Types._string:
                    return s;
                default:
                    return null;
            }
        }

        public static Identifier ParseIdentifier(string s)
        {
            Identifier result = null;
            if (s == string.Empty)
                return result;

            if (s.Contains("string") && !s.Contains('('))
            {
                string p = s.Substring(s.IndexOf('\"'), s.LastIndexOf('\"') - s.IndexOf('\"'));
                if (s.Contains("consts"))
                    return new Consts(s.Split(' ')[2], Types._string, p);
                return new Vars(s.Split(' ')[1], Types._string, p);
            }

            if (s.Contains("char"))
            {
                if (s.Contains("consts"))
                    return new Consts(s.Split(' ')[2], Types._char, s[s.IndexOf('\'') + 1]);
                return new Vars(s.Split(' ')[1], Types._char, s[s.IndexOf('\'') + 1]);
            }

            s = s.Replace("(", " ( ").Replace(")", " ) ").Replace("  ", " ");
            s = s.Trim();
            if (s == string.Empty)
                return result;

            if (s[s.Length - 1] != ';')
                throw new FormatException();
            s = s.TrimEnd(';').Trim();
            var sArr = s.Split(' ').ToList();

            if (sArr[0] == "const")
                return new Consts(sArr[2], ParseType(sArr[1]), sArr[4].ParseTo(ParseType(sArr[1])));

            if (s.Contains('('))
                if (!s.Contains(')'))
                    throw new FormatException();
                else
                {
                    result = new Methods(sArr[1], ParseType(sArr[0]));
                    var list = new LinkedList<Tuple<string, Types, Params>>();
                    string sTemp = s.Substring(s.IndexOf('('), s.LastIndexOf(')') - s.IndexOf('('));
                    sTemp = sTemp.Trim('(', ')');

                    var args = sTemp.Split(',');
                    foreach (string s1 in args)
                    {
                        string[] arg = s1.Split(' ');
                        if (arg.Length == 2)
                            list.AddLast(new Tuple<string, Types, Params>(arg[1].Trim(), ParseType(arg[0]), Params._val));
                        else if (arg[0] == "out")
                            list.AddLast(new Tuple<string, Types, Params>(arg[1].Trim(), ParseType(arg[0]), Params._out));
                        else if (arg[0] == "ref")
                            list.AddLast(new Tuple<string, Types, Params>(arg[1].Trim(), ParseType(arg[0]), Params._ref));
                    }

                    ((Methods)result).args = list;
                    return result;
                }

            if (sArr[0] == "class")
                return new Classes(sArr[1]);

            return new Vars(sArr[1], ParseType(sArr[0]));
        }
    }
}
