namespace Serialization
{
    public static class IdentifierTree
    {
        public static BinTree<Identifier> Make(string str)
        {
            var tree = new BinTree<Identifier>();
            var strings = str.Split('\n');
            foreach (string s in strings)
                tree.Add(Parse.Identifier(s.Trim()));
            return tree;
        }
        public static BinTree<Identifier> Make(string[] strings)
        {
            var tree = new BinTree<Identifier>();
            foreach (string s in strings)
                tree.Add(Parse.Identifier(s));
            return tree;
        }
    }
}