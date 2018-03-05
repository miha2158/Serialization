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
    public partial class MainWindow : Window
    {
        Task1 Page1;
        Task2 Page2;

        public double MinHeight1 = 5, MinWidth1 = 5;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.Manual;
        }
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Page1 = new Task1(this);
            Page2 = new Task2(this);

            ToPage1();
        }

        public void ToPage1()
        {
            WindowFrame.NavigationService.Navigate(Page1);
        }
        public void ToPage2()
        {
            WindowFrame.NavigationService.Navigate(Page2);
        }
    }
}
