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
    /// Interaktionslogik für Blockswitch.xaml
    /// </summary>
    public partial class Blockswitch : Window
    {
        int[,] standard = new int[,] {
                                        {16711680, 65280, 5, 150}   //Blockswitch
                                     };


        public Blockswitch()
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

        private void load_Click(object sender, RoutedEventArgs e)
        {
            switch(effectText.Text)
            {
                case "BLOCKSWITCH":
                    color1.R = Convert.ToByte(standard[0, 0] >> 16);
                    color1.G = Convert.ToByte((UInt16)standard[0, 0] >> 8);
                    color1.B = Convert.ToByte((byte)standard[0, 0]);

                    color2.R = Convert.ToByte(standard[0, 1] >> 16);
                    color2.G = Convert.ToByte((UInt16)standard[0, 1] >> 8);
                    color2.B = Convert.ToByte((byte)standard[0, 1]);

                    breite.Value = standard[0, 2];

                    time.Value = standard[0, 3];
                    break;

            }
        }
    }
}
