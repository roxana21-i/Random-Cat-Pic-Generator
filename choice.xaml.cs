using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project
{
    /// <summary>
    /// Interaction logic for choice.xaml
    /// </summary>
    public partial class choice : Window
    {
        public choice()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void categoryChecked(object sender, RoutedEventArgs e)
        {
            if (Boxes.IsChecked == true)
                App.Current.Properties["Boxes"] = 5;
            if (Boxes.IsChecked == false)
                App.Current.Properties["Boxes"] = 0;
            if (Clothes.IsChecked == true)
                App.Current.Properties["Clothes"] = 15;
            if (Clothes.IsChecked == false)
                App.Current.Properties["Clothes"] = 0;
            if (Hats.IsChecked == true)
                App.Current.Properties["Hats"] = 1;
            if (Hats.IsChecked == false)
                App.Current.Properties["Hats"] = 0;
            if (Sinks.IsChecked == true)
                App.Current.Properties["Sinks"] = 14;
            if (Sinks.IsChecked == false)
                App.Current.Properties["Sinks"] = 0;
            if (Space.IsChecked == true)
                App.Current.Properties["Space"] = 2;
            if (Space.IsChecked == false)
                App.Current.Properties["Space"] = 0;
            if (Sunglasses.IsChecked == true)
                App.Current.Properties["Sunglasses"] = 4;
            if (Sunglasses.IsChecked == false)
                App.Current.Properties["Sunglasses"] = 0;
            if (Ties.IsChecked == true)
                App.Current.Properties["Ties"] = 7;
            if (Ties.IsChecked == false)
                App.Current.Properties["Ties"] = 0;

        }

        private void LetsGo_MouseDown(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
