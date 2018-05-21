using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Xml.XPath;
using System.IO.Compression;
using System.Xml;

namespace Serialization
{
    public partial class Task2: Page
    {
        private new MainWindow Parent;

        public Task2()
        {
            InitializeComponent();
        }
        public Task2(MainWindow window) : this()
        {
            Parent = window;
        }
        private void Task2_OnLoaded(object sender, RoutedEventArgs e)
        {
            //ReSharper disable once AssignNullToNotNullAttribute
            //UnzipPath.Text = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            UnzipPath.Text =
                @"C:\Users\Михаил\Downloads\Documents\Конструирование ПО\Лабораторная работа №5. Работа с файлами и сериализация\Current.zip";
        }

        private void ChooseUnzipPath_Click(object sender, RoutedEventArgs e)
        {
            var fBrowser = new OpenFileDialog()
            {
                FileName = "",
                DefaultExt = ".zip",
                AddExtension = true,
                Multiselect = false
            };

            var b = fBrowser.ShowDialog();
            if (b == DialogResult.Cancel || b == DialogResult.Abort || b == DialogResult.Ignore || b == DialogResult.None)
                return;
            UnzipPath.Text = fBrowser.FileName;
        }

        private void Unzip_Click(object sender, RoutedEventArgs e)
        {
            if(UnzipPath.Text.Length == 0)
                return;
            string path = UnzipPath.Text.Substring(0, UnzipPath.Text.LastIndexOf("."));

            var p = new DirectoryInfo(path);
            if(!p.Exists)
                ZipFile.ExtractToDirectory(UnzipPath.Text, path);

            var dir = new DirectoryInfo(UnzipPath.Text.Substring(0,UnzipPath.Text.LastIndexOf('.')));
            var files = dir.GetFiles().Where(x => x.FullName.Substring(UnzipPath.Text.LastIndexOf('.')).Contains("xml"));

            foreach (FileInfo file in files)
            {
                var result = new Things();

                var doc = new XmlDocument();
                doc.Load(file.FullName);
                XmlElement root = doc.DocumentElement;

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("n1", "http://zakupki.gov.ru/oos/types/1");
                nsmgr.AddNamespace("n2", "http://zakupki.gov.ru/oos/export/1");
                nsmgr.AddNamespace("n3", "http://zakupki.gov.ru/oos/printform/1");
                nsmgr.AddNamespace("n4", "http://zakupki.gov.ru/oos/control99/1");
                
                result.INN = root?.SelectNodes("//n1:customerInfo/n1:INN", nsmgr)?[0].InnerText;
                
                result.KPP = root?.SelectSingleNode("//n1:customerInfo/n1:KPP", nsmgr)?.InnerText;
                
                result.Name = root?.SelectSingleNode("//n1:customerInfo/n1:fullName", nsmgr)?.InnerText;
                
                var nodeList = root?.SelectNodes("//n1:positions//n1:position", nsmgr);
                result.PositionsCount = nodeList?.Count ?? 0;

                foreach (XmlNode node in nodeList)
                {
                    var child = node.SelectSingleNode(@"//n1:OKPD2/n1:code", nsmgr);
                    if (child != null && child.InnerText.StartsWith("63"))
                    {
                        double.TryParse(node?.SelectSingleNode(@"//n1:financeInfo/n1:planPayments/n1:total", nsmgr)?.InnerText, out double d);
                        result.Sum += d;
                        result.Count++;
                    }
                }

                ItemsList.Add(result);
            }

            ItemsView.ItemsSource = ItemsList.Distinct();
        }

        public List<Things> ItemsList { get; set; } = new List<Things>();

        private void ChangeTask_Click(object sender, RoutedEventArgs e)
        {
            Parent.ToPage1();
        }
    }

    public class Things
    {
        public string Name { get; set; } = string.Empty;
        public string INN { get; set; } = string.Empty;
        public string KPP { get; set; } = string.Empty;
        public long PositionsCount { get; set; } = 0;
        public double Sum { get; set; } = 0;
        public int Count { get; set; } = 0;
        public double Avg => Sum / (Count == 0? 1: Count);

        public Things() { }

        public override bool Equals(object obj) { return base.Equals(obj); }

        protected bool Equals(Things other)
        {
            return string.Equals(Name, other.Name) && string.Equals(INN, other.INN) && string.Equals(KPP, other.KPP) && PositionsCount == other.PositionsCount && Sum == other.Sum;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (INN != null ? INN.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (KPP != null ? KPP.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ PositionsCount.GetHashCode();
                hashCode = (hashCode * 397) ^ Sum.GetHashCode();
                return hashCode;
            }
        }
    } 
}
