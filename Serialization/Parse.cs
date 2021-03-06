﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Serialization
{
    public static class Parse
    {
        public static Identifier Identifier(string s)
        {
            Identifier result = null;
            if (s == string.Empty)
                return result;

            s = s.Replace("=  ", "= ").Replace("  ="," =").Replace(" ;", ";").Trim();


            if (s.Contains("const"))
            {
                if (s.Contains("string"))
                {
                    string p = s.Substring(s.IndexOf('\"') + 1, s.LastIndexOf('\"') - s.IndexOf('\"') - 1);
                    return result = new Consts(s.Split(' ')[2], Types._string, p);
                }

                if (s.Contains("char"))
                    return result = new Consts(s.Split(' ')[2], Types._char, s[s.IndexOf('\'') + 1]);

                var sArr1 = s.Split(' ').ToList();
                return result = new Consts(sArr1[2], Type(sArr1[1]), sArr1[4].TrimEnd(';').To(Type(sArr1[1])));
            }

            if (s.Contains("string") && s.IndexOf(')') != s.Length - 2)
            {
                if (s.Contains("="))
                {
                    string p = s.Substring(s.IndexOf('\"') + 1, s.LastIndexOf('\"') - s.IndexOf('\"') - 1);
                    return result = new Vars(s.Split(' ')[1].TrimEnd(';'), Types._string, p);
                }
                return result = new Vars(s.Split(' ')[1].TrimEnd(';'), Types._string);
            }

            if (s.Contains("char"))
            {
                if (s.Contains("="))
                    return result = new Vars(s.Split(' ')[1].TrimEnd(';'), Types._char, s[s.IndexOf('\'') + 1]);
                return result = new Vars(s.Split(' ')[1].TrimEnd(';'), Types._char);
            }

            var ss = (string)s.Clone();
            ss = ss.Replace("(", " ( ").Replace(")", " ) ").Replace("  ", " ").Trim();

            if (ss[ss.Length - 1] != ';')
                throw new FormatException();
            ss = ss.TrimEnd(';').Trim();
            var sArr = ss.Split(' ').ToList();

            if (ss.Contains('('))
                if (!ss.Contains(')'))
                    throw new FormatException();
                else
                {
                    result = new Methods(sArr[1], Type(sArr[0]));
                    var list = new List<Tuple>();
                    string sTemp = ss.Substring(ss.IndexOf('(') + 1, ss.LastIndexOf(')') - ss.IndexOf('(') - 1);
                    sTemp = sTemp.Trim('(', ')');

                    if (sTemp.Trim() == string.Empty)
                        return result;

                    var args = sTemp.Split(',');
                    foreach (string s1 in args)
                    {
                        string[] arg = s1.Trim().Split(' ');
                        if (arg.Length == 2)
                            list.Add(new Tuple(arg[1].Trim(), Type(arg[0]), Params._val));
                        else if (arg[0] == "out")
                            list.Add(new Tuple(arg[2].Trim(), Type(arg[1]), Params._out));
                        else if (arg[0] == "ref")
                            list.Add(new Tuple(arg[2].Trim(), Type(arg[1]), Params._ref));
                    }

                    ((Methods)result).args = list;
                    return result;
                }

            if (sArr[0] == "class")
                return result = new Classes(sArr[1]);

            if(ss.Contains("="))
                return result = new Consts(sArr[1], Type(sArr[0]), sArr[3].To(Type(sArr[0])));
            return result = new Vars(sArr[1], Type(sArr[0]));
        }

        public static Types Type(string s)
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
        public static object To(this string s, Types t)
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
    }
}
