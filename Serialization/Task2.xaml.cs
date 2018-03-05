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
            if(p.Exists)
                p.Delete(true);

            ZipFile.ExtractToDirectory(UnzipPath.Text, path);

            var dir = new DirectoryInfo(UnzipPath.Text.Substring(0,UnzipPath.Text.LastIndexOf('.')));
            var files = dir.GetFiles().Where(x => x.FullName.Substring(UnzipPath.Text.LastIndexOf('.')).Contains("xml"));

            foreach (FileInfo file in files)
            {
                var doc = new XmlDocument();
                doc.Load(file.FullName);
                XmlElement root = doc.DocumentElement;
                XmlNodeList children = root.SelectNodes("*");
                foreach (XmlNode child in children)
                {
                    Test.Text += $"{child.InnerText}{Environment.NewLine}";
                }
            }
        }

        private void ChangeTask_Click(object sender, RoutedEventArgs e)
        {
            Parent.ToPage1();
        }
    }
}
