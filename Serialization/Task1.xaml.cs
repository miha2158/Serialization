using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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

using System.Xml.Serialization;
using static Serialization.Parse;
using FileStream = System.IO.FileStream;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Serialization
{
    public partial class Task1: Page
    {
        private new MainWindow Parent;

        public Task1()
        {
            InitializeComponent();
        }
        public Task1(MainWindow window) : this()
        {
            Parent = window;
        }
        private void MainGrid_OnLoaded(object sender, RoutedEventArgs e)
        {
            //ReSharper disable once AssignNullToNotNullAttribute
            SerializePath.Text = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            StringToParse.Text = @"string method1(int x1, ref char x2, out float x3, out float x3);
int al;
char papapa;
const string Wo = ""jojojo"";
const int c1 = 10;
class MyClass;
const float c = 10,67;
int fact(int a);
const char googas = '4';
bool fa ();
bool az = true;";
        }

        private BinTree<Identifier> mainTree;

        private void ChooseSerializePath_Click(object sender, RoutedEventArgs e)
        {
            var fBrowser = new FolderBrowserDialog
            {
                ShowNewFolderButton = true,
                SelectedPath = SerializePath.Text,
                Description = @"Выберите папку для сериалзирования/десериалзирования файлов"
            };

            fBrowser.ShowDialog();
            SerializePath.Text = fBrowser.SelectedPath;
        }
        private void MakeTree()
        {
            try
            {
                mainTree = StringToParse.Text.Make();
                Console.WriteLine("tree made");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}{Environment.NewLine}{ex.StackTrace}");
                MessageBox.Show($@"Неправильный формат идентификатора
{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        
        private void XML_Seriallize_Click(object sender, RoutedEventArgs e)
        {
            MakeTree();

            //var file = new OpenFileDialog()
            //{
            //    AddExtension = true,
            //    DefaultExt = ".xml",
            //    FileName = "file",
            //    Multiselect = false
            //};

            //file.ShowDialog();

            using (var fs = new FileStream(SerializePath.Text + @"\xml", FileMode.Create))
            {
                var xs = new XmlSerializer(typeof(BinTree<Identifier>));
                xs.Serialize(fs,mainTree);
            }
            Console.WriteLine("xml serialized");
        }
        private void XML_Deseriallize_Click(object sender, RoutedEventArgs e)
        {
            //var file = new OpenFileDialog()
            //{
            //    AddExtension = true,
            //    DefaultExt = ".xml",
            //    FileName = "file",
            //    Multiselect = false
            //};

            //file.ShowDialog();

            using (var fs = new FileStream(SerializePath.Text + @"\xml", FileMode.Open))
            {
                var xs = new XmlSerializer(typeof(BinTree<Identifier>));
                mainTree = (BinTree<Identifier>)xs.Deserialize(fs);
            }
            Console.WriteLine("xml deserialized");

            StringToParse.Text = mainTree.ToString();
        }

        private void BinaryFormatter_Serialize_Click(object sender, RoutedEventArgs e)
        {
            MakeTree();

            //var file = new OpenFileDialog()
            //{
            //    AddExtension = true,
            //    DefaultExt = ".bin",
            //    FileName = "file",
            //    Multiselect = false
            //};

            //file.ShowDialog();


            using (var fs = new FileStream(SerializePath.Text + @"\bin", FileMode.Create))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(fs, mainTree);
            }
            Console.WriteLine("binary serialized");
        }
        private void BinaryFormatter_Deserialize_Click(object sender, RoutedEventArgs e)
        {
            //var file = new OpenFileDialog()
            //{
            //    AddExtension = true,
            //    DefaultExt = ".bin",
            //    FileName = "file",
            //    Multiselect = false
            //};

            //file.ShowDialog();
            
            using (var fs = new FileStream(SerializePath.Text + @"\bin", FileMode.Open))
            {
                var bf = new BinaryFormatter();
                mainTree = (BinTree<Identifier>)bf.Deserialize(fs);
            }
            Console.WriteLine("binary deserialized");

            StringToParse.Text = mainTree.ToString();
        }

        private void ChangeTask_Click(object sender, RoutedEventArgs e)
        {
            Parent.ToPage2();
        }
    }
}
