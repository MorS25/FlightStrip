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

namespace Flight_Strip_Config_Tool_WPF_V0._1
{
    /// <summary>
    /// Interaktionslogik für ColorTwoTimes.xaml
    /// </summary>
    public partial class ColorTwoTimes : Window
    {
        int[,] standard = new int[,] {
                                        {16777215, 50, 150, 0}   //Blink Color
                                     };

        public ColorTwoTimes()
        {
            InitializeComponent();
        }

        private void savebtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

    }
}
