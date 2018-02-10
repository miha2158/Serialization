using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        }

        private void ChooseSerializePath_Click(object sender, RoutedEventArgs e)
        {

        }

        private void XML_Seriallize_Click(object sender, RoutedEventArgs e)
        {

        }
        private void XML_Deseriallize_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BinaryFormatter_Serialize_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BinaryFormatter_Deserialize_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
