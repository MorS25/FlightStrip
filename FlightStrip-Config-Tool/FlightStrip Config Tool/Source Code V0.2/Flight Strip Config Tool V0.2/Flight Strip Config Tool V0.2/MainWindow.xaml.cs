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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Xceed.Wpf.Toolkit;
using System.Diagnostics;


namespace Flight_Strip_Config_Tool_WPF_V0._1
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {   
        // [modus, bereich, index] -> 0: effekt, 1:parameter1, 2:parameter2, 3:parameter3, 4:parameter4
        string[,,] para = new string[20,20,5];
        int[,] standardPara = new int[,] {
                                            {16711680, 0, 0, 0},            //Const Color
                                            {16711680, 65280, 255, 200},    //Const Triple Color
                                            {16711680, 0, 0, 50},           //Chase Single Color
                                            {16711680, 65280, 255, 50},     //Chase Triple Color
                                            {16711680, 65280, 255, 50},     //Fill Triple COlor
                                            {16777215, 50, 150, 0},         //Blink Color
                                            {16777215, 0, 0, 0},            //Doubleflash
                                            {16711680, 65280, 0, 50},       //Fill Against
                                            {16747520, 0, 0, 50},           //Theater Chase
                                            {16711680, 0, 3, 70},           //Knightrider
                                            {16777215, 0, 0, 0},            //Thunderstorm
                                            {0, 0, 0, 0},                   //Police right
                                            {0, 0, 0, 0},                   //Police left
                                            {0, 0, 0, 5},                   //Rainbow
                                            {16711680, 65280, 5, 150},      //Blockswitch
                                            {0, 0, 0, 25}                   //Randomflash
                                         };

        string[] effects = new string[] {   "CONST_COLOR", "CONST_TRIPLE_COLOR", "CHASE_SINGLE_COLOR", "CHASE_TRIPLE_COLOR", "FILL_TRIPLE_COLOR",
                                            "BLINK_COLOR", "DOUBLEFLASH", "FILL_AGAINST", "THEATER_CHASE", "KNIGHTRIDER", "THUNDERSTORM",
                                            "POLICE_RIGHT", "POLICE_LEFT", "RAINBOW", "BLOCKSWITCH", "RANDOMFLASH"};

        bool userControl = false;

        String pfad;
        String datei;
        String name;
        String overpfad;
        String overordner;


        public MainWindow()
        {
            InitializeComponent();

            for (int mode = 0; mode < 20; mode++)  //Parameter und Effekte
            {
                for (int bereich = 0; bereich < 20; bereich++)
                {
                    para[mode, bereich, 0] = "-1";
                    for (int param = 1; param < 5; param++)
                    {
                        para[mode, bereich, param] = "0";
                    }
                }
            }
            userControl = true;
        }

        private void modeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            modeBox.Text = modeSlider.Value.ToString();
            modeSelectSlider.Maximum = modeSlider.Value;         
        }

        private void modeSelectSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            modeSelectBox.Text = modeSelectSlider.Value.ToString();

            try
            {
                userControl = false;
                effect1.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 0, 0]);
                effect2.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 1, 0]);
                effect3.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 2, 0]);
                effect4.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 3, 0]);
                effect5.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 4, 0]);
                effect6.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 5, 0]);
                effect7.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 6, 0]);
                effect8.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 7, 0]);
                effect9.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 8, 0]);
                effect10.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 9, 0]);
                effect11.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 10, 0]);
                effect12.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 11, 0]);
                effect13.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 12, 0]);
                effect14.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 13, 0]);
                effect15.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 14, 0]);
                effect16.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 15, 0]);
                effect17.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 16, 0]);
                effect18.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 17, 0]);
                effect19.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 18, 0]);
                effect20.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 19, 0]);
                userControl = true;
            }
            catch
            { }         
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void effect1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                para[(int)modeSelectSlider.Value - 1, 0, 0] = effect1.SelectedIndex.ToString();          
            if(userControl)
            {
                setStandardPara(0);
            }
        }

        private void effect2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                para[(int)modeSelectSlider.Value - 1, 1, 0] = effect2.SelectedIndex.ToString();
                if (userControl)
                {
                    setStandardPara(1);
                }
        }

        private void effect3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                para[(int)modeSelectSlider.Value - 1, 2, 0] = effect3.SelectedIndex.ToString();
                if (userControl)
                {
                    setStandardPara(2);
                }
        }

        private void effect4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                para[(int)modeSelectSlider.Value - 1, 3, 0] = effect4.SelectedIndex.ToString();
                if (userControl)
                {
                    setStandardPara(3);
                }
        }

        private void effect5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                para[(int)modeSelectSlider.Value - 1, 4, 0] = effect5.SelectedIndex.ToString();
                if (userControl)
                {
                    setStandardPara(4);
                }
        }

        private void effect6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                para[(int)modeSelectSlider.Value - 1, 5, 0] = effect6.SelectedIndex.ToString();
                if (userControl)
                {
                    setStandardPara(5);
                }
        }

        private void effect7_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                para[(int)modeSelectSlider.Value - 1, 6, 0] = effect7.SelectedIndex.ToString();
                if (userControl)
                {
                    setStandardPara(6);
                }
        }

        private void effect8_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                para[(int)modeSelectSlider.Value - 1, 7, 0] = effect8.SelectedIndex.ToString();
                if (userControl)
                {
                    setStandardPara(7);
                }
        }

        private void effect9_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            para[(int)modeSelectSlider.Value - 1, 8, 0] = effect9.SelectedIndex.ToString(); 
            if (userControl)
            {
                setStandardPara(8);
            }
        }

        private void effect10_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                para[(int)modeSelectSlider.Value - 1, 9, 0] = effect10.SelectedIndex.ToString();
                if (userControl)
                {
                    setStandardPara(9);
                }
        }

        private void effect11_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            para[(int)modeSelectSlider.Value - 1, 10, 0] = effect11.SelectedIndex.ToString();
            if (userControl)
            {
                setStandardPara(10);
            }
        }

        private void effect12_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            para[(int)modeSelectSlider.Value - 1, 11, 0] = effect12.SelectedIndex.ToString();
            if (userControl)
            {
                setStandardPara(11);
            }
        }

        private void effect13_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            para[(int)modeSelectSlider.Value - 1, 12, 0] = effect13.SelectedIndex.ToString();
            if (userControl)
            {
                setStandardPara(12);
            }
        }

        private void effect14_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            para[(int)modeSelectSlider.Value - 1, 13, 0] = effect14.SelectedIndex.ToString();
            if (userControl)
            {
                setStandardPara(13);
            }
        }

        private void effect15_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            para[(int)modeSelectSlider.Value - 1, 14, 0] = effect15.SelectedIndex.ToString();
            if (userControl)
            {
                setStandardPara(14);
            }
        }

        private void effect16_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            para[(int)modeSelectSlider.Value - 1, 15, 0] = effect16.SelectedIndex.ToString();
            if (userControl)
            {
                setStandardPara(15);
            }
        }

        private void effect17_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            para[(int)modeSelectSlider.Value - 1, 16, 0] = effect17.SelectedIndex.ToString();
            if (userControl)
            {
                setStandardPara(16);
            }
        }

        private void effect18_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            para[(int)modeSelectSlider.Value - 1, 17, 0] = effect18.SelectedIndex.ToString();
            if (userControl)
            {
                setStandardPara(17);
            }
        }

        private void effect19_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            para[(int)modeSelectSlider.Value - 1, 18, 0] = effect19.SelectedIndex.ToString();
            if (userControl)
            {
                setStandardPara(18);
            }
        }

        private void effect20_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            para[(int)modeSelectSlider.Value - 1, 19, 0] = effect20.SelectedIndex.ToString();
            if (userControl)
            {
                setStandardPara(19);
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.Filter = "FlightStrip Config File (*.fsc)|*.fsc";
            dlg.FilterIndex = 1;
            dlg.RestoreDirectory = true;
            dlg.DefaultExt = ".fsc";

            if (dlg.ShowDialog() == true)
            {
                StreamReader reader = new StreamReader(dlg.OpenFile());

                if (reader.ReadLine().Equals("V0.2"))
                {
                    modeSlider.Value = Convert.ToDouble(reader.ReadLine()); //Modeanzahl

                    rcmode1.IsChecked = Convert.ToBoolean(reader.ReadLine());//RC Parameter
                    rcmode2.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    rcmode3.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    rcmode4.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    updateTime.Text = reader.ReadLine();
                    nextSwitch.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    randomSwitch.IsChecked = Convert.ToBoolean(reader.ReadLine());

                    pin1.Text = reader.ReadLine();                          //Pins und Strip Aktivierung
                    pin2.Text = reader.ReadLine();
                    strip2act.IsChecked = Convert.ToBoolean(reader.ReadLine());

                    act1.IsChecked = Convert.ToBoolean(reader.ReadLine());  //Aktive Bereiche
                    act2.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    act3.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    act4.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    act5.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    act6.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    act7.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    act8.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    act9.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    act10.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    act11.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    act12.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    act13.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    act14.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    act15.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    act16.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    act17.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    act18.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    act19.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    act20.IsChecked = Convert.ToBoolean(reader.ReadLine());

                    rev1.IsChecked = Convert.ToBoolean(reader.ReadLine());  //Reverse
                    rev2.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    rev3.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    rev4.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    rev5.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    rev6.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    rev7.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    rev8.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    rev9.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    rev10.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    rev11.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    rev12.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    rev13.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    rev14.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    rev15.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    rev16.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    rev17.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    rev18.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    rev19.IsChecked = Convert.ToBoolean(reader.ReadLine());
                    rev20.IsChecked = Convert.ToBoolean(reader.ReadLine());

                    name1.Text = reader.ReadLine();                         //Namen
                    name2.Text = reader.ReadLine();
                    name3.Text = reader.ReadLine();
                    name4.Text = reader.ReadLine();
                    name5.Text = reader.ReadLine();
                    name6.Text = reader.ReadLine();
                    name7.Text = reader.ReadLine();
                    name8.Text = reader.ReadLine();
                    name9.Text = reader.ReadLine();
                    name10.Text = reader.ReadLine();
                    name11.Text = reader.ReadLine();
                    name12.Text = reader.ReadLine();
                    name13.Text = reader.ReadLine();
                    name14.Text = reader.ReadLine();
                    name15.Text = reader.ReadLine();
                    name16.Text = reader.ReadLine();
                    name17.Text = reader.ReadLine();
                    name18.Text = reader.ReadLine();
                    name19.Text = reader.ReadLine();
                    name20.Text = reader.ReadLine();

                    start1.Text = reader.ReadLine();                        //Start LED
                    start2.Text = reader.ReadLine();
                    start3.Text = reader.ReadLine();
                    start4.Text = reader.ReadLine();
                    start5.Text = reader.ReadLine();
                    start6.Text = reader.ReadLine();
                    start7.Text = reader.ReadLine();
                    start8.Text = reader.ReadLine();
                    start9.Text = reader.ReadLine();
                    start10.Text = reader.ReadLine();
                    start11.Text = reader.ReadLine();
                    start12.Text = reader.ReadLine();
                    start13.Text = reader.ReadLine();
                    start14.Text = reader.ReadLine();
                    start15.Text = reader.ReadLine();
                    start16.Text = reader.ReadLine();
                    start17.Text = reader.ReadLine();
                    start18.Text = reader.ReadLine();
                    start19.Text = reader.ReadLine();
                    start20.Text = reader.ReadLine();

                    end1.Text = reader.ReadLine();                          //End LED
                    end2.Text = reader.ReadLine();
                    end3.Text = reader.ReadLine();
                    end4.Text = reader.ReadLine();
                    end5.Text = reader.ReadLine();
                    end6.Text = reader.ReadLine();
                    end7.Text = reader.ReadLine();
                    end8.Text = reader.ReadLine();
                    end9.Text = reader.ReadLine();
                    end10.Text = reader.ReadLine();
                    end11.Text = reader.ReadLine();
                    end12.Text = reader.ReadLine();
                    end13.Text = reader.ReadLine();
                    end14.Text = reader.ReadLine();
                    end15.Text = reader.ReadLine();
                    end16.Text = reader.ReadLine();
                    end17.Text = reader.ReadLine();
                    end18.Text = reader.ReadLine();
                    end19.Text = reader.ReadLine();
                    end20.Text = reader.ReadLine();

                    for (int mode = 0; mode < 20; mode++)                   //Parameter und Effekte
                    {
                        for (int bereich = 0; bereich < 20; bereich++)
                        {
                            for (int param = 0; param < 5; param++)
                            {
                                para[mode, bereich, param] = reader.ReadLine();
                            }
                        }
                    }

                    try
                    {
                        userControl = false;
                        effect1.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 0, 0]);
                        effect2.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 1, 0]);
                        effect3.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 2, 0]);
                        effect4.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 3, 0]);
                        effect5.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 4, 0]);
                        effect6.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 5, 0]);
                        effect7.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 6, 0]);
                        effect8.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 7, 0]);
                        effect9.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 8, 0]);
                        effect10.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 9, 0]);
                        effect11.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 10, 0]);
                        effect12.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 11, 0]);
                        effect13.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 12, 0]);
                        effect14.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 13, 0]);
                        effect15.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 14, 0]);
                        effect16.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 15, 0]);
                        effect17.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 16, 0]);
                        effect18.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 17, 0]);
                        effect19.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 18, 0]);
                        effect20.SelectedIndex = Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, 19, 0]);
                        userControl = true;
                    }
                    catch
                    { }

                    System.Windows.MessageBox.Show("Die Konfiguration wurde erfolgreich geladen.", "Erfolgreich geladen");
                }
                else
                {
                    System.Windows.MessageBox.Show("Die geladene Konfiguration ist nicht kompatibel.", "Inkompatible Version");
                }
                reader.Dispose();
                reader.Close();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            dlg.Filter = "FlightStrip Config File (*.fsc)|*.fsc";
            dlg.FilterIndex = 1;
            dlg.RestoreDirectory = true;
            dlg.DefaultExt = ".fsc";

            if (dlg.ShowDialog() == true)
            {
                StreamWriter writer = new StreamWriter(dlg.OpenFile());

                writer.WriteLine("V0.2");           //Für Versionscheck

                writer.WriteLine(modeSlider.Value); //Modeanzahl


                writer.WriteLine(rcmode1.IsChecked);//RC Parameter
                writer.WriteLine(rcmode2.IsChecked);
                writer.WriteLine(rcmode3.IsChecked);
                writer.WriteLine(rcmode4.IsChecked);
                writer.WriteLine(updateTime.Text);  
                writer.WriteLine(nextSwitch.IsChecked);
                writer.WriteLine(randomSwitch.IsChecked);

                writer.WriteLine(pin1.Text);        //Pins und Strip Aktivierung
                writer.WriteLine(pin2.Text);
                writer.WriteLine(strip2act.IsChecked);

                writer.WriteLine(act1.IsChecked);   //Aktive Bereiche
                writer.WriteLine(act2.IsChecked);
                writer.WriteLine(act3.IsChecked);
                writer.WriteLine(act4.IsChecked);
                writer.WriteLine(act5.IsChecked);
                writer.WriteLine(act6.IsChecked);
                writer.WriteLine(act7.IsChecked);
                writer.WriteLine(act8.IsChecked);
                writer.WriteLine(act9.IsChecked);
                writer.WriteLine(act10.IsChecked);
                writer.WriteLine(act11.IsChecked);   
                writer.WriteLine(act12.IsChecked);
                writer.WriteLine(act13.IsChecked);
                writer.WriteLine(act14.IsChecked);
                writer.WriteLine(act15.IsChecked);
                writer.WriteLine(act16.IsChecked);
                writer.WriteLine(act17.IsChecked);
                writer.WriteLine(act18.IsChecked);
                writer.WriteLine(act19.IsChecked);
                writer.WriteLine(act20.IsChecked);

                writer.WriteLine(rev1.IsChecked);   //Reverse
                writer.WriteLine(rev2.IsChecked);
                writer.WriteLine(rev3.IsChecked);
                writer.WriteLine(rev4.IsChecked);
                writer.WriteLine(rev5.IsChecked);
                writer.WriteLine(rev6.IsChecked);
                writer.WriteLine(rev7.IsChecked);
                writer.WriteLine(rev8.IsChecked);
                writer.WriteLine(rev9.IsChecked);
                writer.WriteLine(rev10.IsChecked);
                writer.WriteLine(rev11.IsChecked); 
                writer.WriteLine(rev12.IsChecked);
                writer.WriteLine(rev13.IsChecked);
                writer.WriteLine(rev14.IsChecked);
                writer.WriteLine(rev15.IsChecked);
                writer.WriteLine(rev16.IsChecked);
                writer.WriteLine(rev17.IsChecked);
                writer.WriteLine(rev18.IsChecked);
                writer.WriteLine(rev19.IsChecked);
                writer.WriteLine(rev20.IsChecked);

                writer.WriteLine(name1.Text);       //Namen
                writer.WriteLine(name2.Text);
                writer.WriteLine(name3.Text);
                writer.WriteLine(name4.Text);
                writer.WriteLine(name5.Text);
                writer.WriteLine(name6.Text);
                writer.WriteLine(name7.Text);
                writer.WriteLine(name8.Text);
                writer.WriteLine(name9.Text);
                writer.WriteLine(name10.Text);
                writer.WriteLine(name11.Text); 
                writer.WriteLine(name12.Text);
                writer.WriteLine(name13.Text);
                writer.WriteLine(name14.Text);
                writer.WriteLine(name15.Text);
                writer.WriteLine(name16.Text);
                writer.WriteLine(name17.Text);
                writer.WriteLine(name18.Text);
                writer.WriteLine(name19.Text);
                writer.WriteLine(name20.Text);

                writer.WriteLine(start1.Text);      //Start LED
                writer.WriteLine(start2.Text);
                writer.WriteLine(start3.Text);
                writer.WriteLine(start4.Text);
                writer.WriteLine(start5.Text);
                writer.WriteLine(start6.Text);
                writer.WriteLine(start7.Text);
                writer.WriteLine(start8.Text);
                writer.WriteLine(start9.Text);
                writer.WriteLine(start10.Text);
                writer.WriteLine(start11.Text);
                writer.WriteLine(start12.Text);
                writer.WriteLine(start13.Text);
                writer.WriteLine(start14.Text);
                writer.WriteLine(start15.Text);
                writer.WriteLine(start16.Text);
                writer.WriteLine(start17.Text);
                writer.WriteLine(start18.Text);
                writer.WriteLine(start19.Text);
                writer.WriteLine(start20.Text);

                writer.WriteLine(end1.Text);        //End LED
                writer.WriteLine(end2.Text);
                writer.WriteLine(end3.Text);
                writer.WriteLine(end4.Text);
                writer.WriteLine(end5.Text);
                writer.WriteLine(end6.Text);
                writer.WriteLine(end7.Text);
                writer.WriteLine(end8.Text);
                writer.WriteLine(end9.Text);
                writer.WriteLine(end10.Text);
                writer.WriteLine(end11.Text);
                writer.WriteLine(end12.Text);
                writer.WriteLine(end13.Text);
                writer.WriteLine(end14.Text);
                writer.WriteLine(end15.Text);
                writer.WriteLine(end16.Text);
                writer.WriteLine(end17.Text);
                writer.WriteLine(end18.Text);
                writer.WriteLine(end19.Text);
                writer.WriteLine(end20.Text);

                for (int mode = 0; mode < 20; mode++ )  //Parameter und Effekte
                {
                    for (int bereich = 0; bereich < 20; bereich++)
                    {
                        for (int param = 0; param < 5; param++)
                        {
                            writer.WriteLine(para[mode,bereich,param]);
                        }
                    }
                }


                writer.Dispose();
                writer.Close();
            }
        }

        private void compile_Click(object sender, RoutedEventArgs e)
        {
            //Berechnung highLED
            int highLED1;
            if (act10.IsChecked == true)
                highLED1 = Convert.ToInt32(end10.Text) + 1;
            else if (act9.IsChecked == true)
                highLED1 = Convert.ToInt32(end9.Text) + 1;
            else if (act8.IsChecked == true)
                highLED1 = Convert.ToInt32(end8.Text) + 1;
            else if (act7.IsChecked == true)
                highLED1 = Convert.ToInt32(end7.Text) + 1;
            else if (act6.IsChecked == true)
                highLED1 = Convert.ToInt32(end6.Text) + 1;
            else if (act5.IsChecked == true)
                highLED1 = Convert.ToInt32(end5.Text) + 1;
            else if (act4.IsChecked == true)
                highLED1 = Convert.ToInt32(end4.Text) + 1;
            else if (act3.IsChecked == true)
                highLED1 = Convert.ToInt32(end3.Text) + 1;
            else if (act2.IsChecked == true)
                highLED1 = Convert.ToInt32(end2.Text) + 1;
            else
                highLED1 = Convert.ToInt32(end1.Text) + 1;

            int highLED2 = 0;
            if(strip2act.IsChecked == true)
            {  
                if (act20.IsChecked == true)
                    highLED2 = Convert.ToInt32(end20.Text) + 1;
                else if (act19.IsChecked == true)
                    highLED2 = Convert.ToInt32(end19.Text) + 1;
                else if (act18.IsChecked == true)
                    highLED2 = Convert.ToInt32(end18.Text) + 1;
                else if (act17.IsChecked == true)
                    highLED2 = Convert.ToInt32(end17.Text) + 1;
                else if (act16.IsChecked == true)
                    highLED2 = Convert.ToInt32(end16.Text) + 1;
                else if (act15.IsChecked == true)
                    highLED2 = Convert.ToInt32(end15.Text) + 1;
                else if (act14.IsChecked == true)
                    highLED2 = Convert.ToInt32(end14.Text) + 1;
                else if (act13.IsChecked == true)
                    highLED2 = Convert.ToInt32(end13.Text) + 1;
                else if (act12.IsChecked == true)
                    highLED2 = Convert.ToInt32(end12.Text) + 1;
                else
                    highLED2 = Convert.ToInt32(end11.Text) + 1;
            }

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            dlg.Filter = "Arduino File (*.ino)|*.ino";
            dlg.FilterIndex = 1;
            dlg.RestoreDirectory = true;
            dlg.DefaultExt = ".ino";

            if (dlg.ShowDialog() == true)
            {
                StreamWriter writer = new StreamWriter(dlg.OpenFile());

                writer.WriteLine("#include <Adafruit_NeoPixel.h>");
                writer.WriteLine("#include \"FlightStrip.h\"");
                writer.WriteLine();
                writer.WriteLine("#define SWITCH_PIN       6");
                writer.WriteLine("#define STRIP1_PIN       " + pin1.Text);
                writer.WriteLine("#define STRIP2_PIN       " + pin2.Text);
                writer.WriteLine("#define LED1_ANZAHL       "+ highLED1);
                if(strip2act.IsChecked == true)
                    writer.WriteLine("#define LED2_ANZAHL       " + highLED2);               
                if (act1.IsChecked == true)                                             //Bereichsgrenzen
                {
                    writer.WriteLine("#define BEREICH1_BOT     " + start1.Text);
                    writer.WriteLine("#define BEREICH1_UP      " + end1.Text);
                }
                if (act2.IsChecked == true)
                {
                    writer.WriteLine("#define BEREICH2_BOT     " + start2.Text);
                    writer.WriteLine("#define BEREICH2_UP      " + end2.Text);
                }
                if (act3.IsChecked == true)
                {
                    writer.WriteLine("#define BEREICH3_BOT     " + start3.Text);
                    writer.WriteLine("#define BEREICH3_UP      " + end3.Text);
                }
                if (act4.IsChecked == true)
                {
                    writer.WriteLine("#define BEREICH4_BOT     " + start4.Text);
                    writer.WriteLine("#define BEREICH4_UP      " + end4.Text);
                }
                if (act5.IsChecked == true)
                {
                    writer.WriteLine("#define BEREICH5_BOT     " + start5.Text);
                    writer.WriteLine("#define BEREICH5_UP      " + end5.Text);
                }
                if (act6.IsChecked == true)
                {
                    writer.WriteLine("#define BEREICH6_BOT     " + start6.Text);
                    writer.WriteLine("#define BEREICH6_UP      " + end6.Text);
                }
                if (act7.IsChecked == true)
                {
                    writer.WriteLine("#define BEREICH7_BOT     " + start7.Text);
                    writer.WriteLine("#define BEREICH7_UP      " + end7.Text);
                }
                if (act8.IsChecked == true)
                {
                    writer.WriteLine("#define BEREICH8_BOT     " + start8.Text);
                    writer.WriteLine("#define BEREICH8_UP      " + end8.Text);
                }
                if (act9.IsChecked == true)
                {
                    writer.WriteLine("#define BEREICH9_BOT     " + start9.Text);
                    writer.WriteLine("#define BEREICH9_UP      " + end9.Text);
                }
                if (act10.IsChecked == true)
                {
                    writer.WriteLine("#define BEREICH10_BOT    " + start10.Text);
                    writer.WriteLine("#define BEREICH10_UP     " + end10.Text);
                }
                if (act11.IsChecked == true)
                {
                    writer.WriteLine("#define BEREICH11_BOT     " + start11.Text);
                    writer.WriteLine("#define BEREICH11_UP      " + end11.Text);
                }
                if (act12.IsChecked == true)
                {
                    writer.WriteLine("#define BEREICH12_BOT     " + start12.Text);
                    writer.WriteLine("#define BEREICH12_UP      " + end12.Text);
                }
                if (act13.IsChecked == true)
                {
                    writer.WriteLine("#define BEREICH13_BOT     " + start13.Text);
                    writer.WriteLine("#define BEREICH13_UP      " + end13.Text);
                }
                if (act14.IsChecked == true)
                {
                    writer.WriteLine("#define BEREICH14_BOT     " + start14.Text);
                    writer.WriteLine("#define BEREICH14_UP      " + end14.Text);
                }
                if (act15.IsChecked == true)
                {
                    writer.WriteLine("#define BEREICH15_BOT     " + start15.Text);
                    writer.WriteLine("#define BEREICH15_UP      " + end15.Text);
                }
                if (act16.IsChecked == true)
                {
                    writer.WriteLine("#define BEREICH16_BOT     " + start16.Text);
                    writer.WriteLine("#define BEREICH16_UP      " + end16.Text);
                }
                if (act17.IsChecked == true)
                {
                    writer.WriteLine("#define BEREICH17_BOT     " + start17.Text);
                    writer.WriteLine("#define BEREICH17_UP      " + end17.Text);
                }
                if (act18.IsChecked == true)
                {
                    writer.WriteLine("#define BEREICH18_BOT     " + start18.Text);
                    writer.WriteLine("#define BEREICH18_UP      " + end18.Text);
                }
                if (act19.IsChecked == true)
                {
                    writer.WriteLine("#define BEREICH19_BOT     " + start19.Text);
                    writer.WriteLine("#define BEREICH19_UP      " + end19.Text);
                }
                if (act20.IsChecked == true)
                {
                    writer.WriteLine("#define BEREICH20_BOT    " + start20.Text);
                    writer.WriteLine("#define BEREICH20_UP     " + end20.Text);
                }

                writer.WriteLine("#define MODE_ANZAHL      " + (int)modeSlider.Value);
                writer.WriteLine("#define UPDATE           " + (Convert.ToInt32(updateTime.Text))*1000);
                if(rcmode3.IsChecked == true)
                    writer.WriteLine("#define INCTIME           500");
                writer.WriteLine();
                writer.WriteLine("Adafruit_NeoPixel strip = Adafruit_NeoPixel(LED1_ANZAHL, STRIP1_PIN, NEO_GRB + NEO_KHZ800);");        //Strips
                if(strip2act.IsChecked == true)
                    writer.WriteLine("Adafruit_NeoPixel strip2 = Adafruit_NeoPixel(LED2_ANZAHL, STRIP2_PIN, NEO_GRB + NEO_KHZ800);");
                
                if(act1.IsChecked == true)
                    writer.WriteLine("FlightStrip "+ name1.Text+ "  = FlightStrip(strip, BEREICH1_BOT, BEREICH1_UP);");                 //Bereichsobjekte
                if (act2.IsChecked == true)
                    writer.WriteLine("FlightStrip " + name2.Text + "  = FlightStrip(strip, BEREICH2_BOT, BEREICH2_UP);");
                if (act3.IsChecked == true)
                    writer.WriteLine("FlightStrip " + name3.Text + "  = FlightStrip(strip, BEREICH3_BOT, BEREICH3_UP);");
                if (act4.IsChecked == true)
                    writer.WriteLine("FlightStrip " + name4.Text + "  = FlightStrip(strip, BEREICH4_BOT, BEREICH4_UP);");
                if (act5.IsChecked == true)
                    writer.WriteLine("FlightStrip " + name5.Text + "  = FlightStrip(strip, BEREICH5_BOT, BEREICH5_UP);");
                if (act6.IsChecked == true)
                    writer.WriteLine("FlightStrip " + name6.Text + "  = FlightStrip(strip, BEREICH6_BOT, BEREICH6_UP);");
                if (act7.IsChecked == true)
                    writer.WriteLine("FlightStrip " + name7.Text + "  = FlightStrip(strip, BEREICH7_BOT, BEREICH7_UP);");
                if (act8.IsChecked == true)
                    writer.WriteLine("FlightStrip " + name8.Text + "  = FlightStrip(strip, BEREICH8_BOT, BEREICH8_UP);");
                if (act9.IsChecked == true)
                    writer.WriteLine("FlightStrip " + name9.Text + "  = FlightStrip(strip, BEREICH9_BOT, BEREICH9_UP);");
                if (act10.IsChecked == true)
                    writer.WriteLine("FlightStrip " + name10.Text + "  = FlightStrip(strip2, BEREICH10_BOT, BEREICH10_UP);");
                if (act11.IsChecked == true)
                    writer.WriteLine("FlightStrip " + name11.Text + "  = FlightStrip(strip2, BEREICH11_BOT, BEREICH11_UP);");
                if (act12.IsChecked == true)
                    writer.WriteLine("FlightStrip " + name12.Text + "  = FlightStrip(strip2, BEREICH12_BOT, BEREICH12_UP);");
                if (act13.IsChecked == true)
                    writer.WriteLine("FlightStrip " + name13.Text + "  = FlightStrip(strip2, BEREICH13_BOT, BEREICH13_UP);");
                if (act14.IsChecked == true)
                    writer.WriteLine("FlightStrip " + name14.Text + "  = FlightStrip(strip2, BEREICH14_BOT, BEREICH14_UP);");
                if (act15.IsChecked == true)
                    writer.WriteLine("FlightStrip " + name15.Text + "  = FlightStrip(strip2, BEREICH15_BOT, BEREICH15_UP);");
                if (act16.IsChecked == true)
                    writer.WriteLine("FlightStrip " + name16.Text + "  = FlightStrip(strip2, BEREICH16_BOT, BEREICH16_UP);");
                if (act17.IsChecked == true)
                    writer.WriteLine("FlightStrip " + name17.Text + "  = FlightStrip(strip2, BEREICH17_BOT, BEREICH17_UP);");
                if (act18.IsChecked == true)
                    writer.WriteLine("FlightStrip " + name18.Text + "  = FlightStrip(strip2, BEREICH18_BOT, BEREICH18_UP);");
                if (act19.IsChecked == true)
                    writer.WriteLine("FlightStrip " + name19.Text + "  = FlightStrip(strip2, BEREICH19_BOT, BEREICH19_UP);");
                if (act20.IsChecked == true)
                    writer.WriteLine("FlightStrip " + name20.Text + "  = FlightStrip(strip2, BEREICH20_BOT, BEREICH20_UP);");
                writer.WriteLine();

                writer.WriteLine("uint32_t farben[] =  {strip.Color(255,0,0),strip.Color(0,255,0),strip.Color(0,0,255),strip.Color(255,0,255),strip.Color(0,255,255),strip.Color(255,255,255),strip.Color(255,255,0),strip.Color(255,140,0),strip.Color(0,127,127)};");
                writer.WriteLine("uint32_t lastShow = 0;");
                writer.WriteLine("uint32_t lastSwitch = 0;");
                writer.WriteLine("uint32_t aendern = 0;");
                writer.WriteLine("uint8_t modus = 0;");
                if (rcmode2.IsChecked == true)
                    writer.WriteLine("uint8_t lastModus = 1;");
                if(rcmode3.IsChecked == true)
                    writer.WriteLine("uint32_t lastInc = 0;");
                writer.WriteLine();

                writer.WriteLine("void setup()");                                           //Setup
                writer.WriteLine("{");
                writer.WriteLine("  Serial.begin(115200); //For Debugging");
                writer.WriteLine("  delay(50);");
                writer.WriteLine("  pinMode(SWITCH_PIN, OUTPUT);");
                writer.WriteLine("  digitalWrite(SWITCH_PIN, LOW);");
                if(rcmode4.IsChecked == false)
                    writer.WriteLine("  initializeRC();");
                writer.WriteLine("  randomSeed(analogRead(A0));");
                writer.WriteLine("  strip.begin(); //Strip(s) initialisieren");
                writer.WriteLine("  strip.show();  //Initialize all pixels to 'off'");
                if(strip2act.IsChecked == true)
                {
                    writer.WriteLine("  strip2.begin(); //Strip(s) initialisieren");
                    writer.WriteLine("  strip2.show();  //Initialize all pixels to 'off'");
                }
                writer.WriteLine();
                for (int mode = 0; mode < (int)modeSlider.Value; mode++)                    //Modi einlernen
                {
                    if (act1.IsChecked == true)
                        writer.WriteLine("  " + name1.Text + ".learnMode(" + mode + ", "+ effects[Convert.ToInt16(para[mode, 0, 0])]+ ");");
                    if (act2.IsChecked == true)
                        writer.WriteLine("  " + name2.Text + ".learnMode(" + mode + ", " + effects[Convert.ToInt16(para[mode, 1, 0])] + ");");
                    if (act3.IsChecked == true)
                        writer.WriteLine("  " + name3.Text + ".learnMode(" + mode + ", " + effects[Convert.ToInt16(para[mode, 2, 0])] + ");");
                    if (act4.IsChecked == true)
                        writer.WriteLine("  " + name4.Text + ".learnMode(" + mode + ", " + effects[Convert.ToInt16(para[mode, 3, 0])] + ");");
                    if (act5.IsChecked == true)
                        writer.WriteLine("  " + name5.Text + ".learnMode(" + mode + ", " + effects[Convert.ToInt16(para[mode, 4, 0])] + ");");
                    if (act6.IsChecked == true)
                        writer.WriteLine("  " + name6.Text + ".learnMode(" + mode + ", " + effects[Convert.ToInt16(para[mode, 5, 0])] + ");");
                    if (act7.IsChecked == true)
                        writer.WriteLine("  " + name7.Text + ".learnMode(" + mode + ", " + effects[Convert.ToInt16(para[mode, 6, 0])] + ");");
                    if (act8.IsChecked == true)
                        writer.WriteLine("  " + name8.Text + ".learnMode(" + mode + ", " + effects[Convert.ToInt16(para[mode, 7, 0])] + ");");
                    if (act9.IsChecked == true)
                        writer.WriteLine("  " + name9.Text + ".learnMode(" + mode + ", " + effects[Convert.ToInt16(para[mode, 8, 0])] + ");");
                    if (act10.IsChecked == true)
                        writer.WriteLine("  " + name10.Text + ".learnMode(" + mode + ", " + effects[Convert.ToInt16(para[mode, 9, 0])] + ");");
                    if (act11.IsChecked == true)
                        writer.WriteLine("  " + name11.Text + ".learnMode(" + mode + ", " + effects[Convert.ToInt16(para[mode, 10, 0])] + ");");
                    if (act12.IsChecked == true)
                        writer.WriteLine("  " + name12.Text + ".learnMode(" + mode + ", " + effects[Convert.ToInt16(para[mode, 11, 0])] + ");");
                    if (act13.IsChecked == true)
                        writer.WriteLine("  " + name13.Text + ".learnMode(" + mode + ", " + effects[Convert.ToInt16(para[mode, 12, 0])] + ");");
                    if (act14.IsChecked == true)
                        writer.WriteLine("  " + name14.Text + ".learnMode(" + mode + ", " + effects[Convert.ToInt16(para[mode, 13, 0])] + ");");
                    if (act15.IsChecked == true)
                        writer.WriteLine("  " + name15.Text + ".learnMode(" + mode + ", " + effects[Convert.ToInt16(para[mode, 14, 0])] + ");");
                    if (act16.IsChecked == true)
                        writer.WriteLine("  " + name16.Text + ".learnMode(" + mode + ", " + effects[Convert.ToInt16(para[mode, 15, 0])] + ");");
                    if (act17.IsChecked == true)
                        writer.WriteLine("  " + name17.Text + ".learnMode(" + mode + ", " + effects[Convert.ToInt16(para[mode, 16, 0])] + ");");
                    if (act18.IsChecked == true)
                        writer.WriteLine("  " + name18.Text + ".learnMode(" + mode + ", " + effects[Convert.ToInt16(para[mode, 17, 0])] + ");");
                    if (act19.IsChecked == true)
                        writer.WriteLine("  " + name19.Text + ".learnMode(" + mode + ", " + effects[Convert.ToInt16(para[mode, 18, 0])] + ");");
                    if (act20.IsChecked == true)
                        writer.WriteLine("  " + name20.Text + ".learnMode(" + mode + ", " + effects[Convert.ToInt16(para[mode, 19, 0])] + ");");
                    writer.WriteLine();
                }
                if (rev1.IsChecked == true)
                    writer.WriteLine("  " + name1.Text + ".reverse();");
                if (rev2.IsChecked == true)
                    writer.WriteLine("  " + name2.Text + ".reverse();");
                if (rev3.IsChecked == true)
                    writer.WriteLine("  " + name3.Text + ".reverse();");
                if (rev4.IsChecked == true)
                    writer.WriteLine("  " + name4.Text + ".reverse();");
                if (rev5.IsChecked == true)
                    writer.WriteLine("  " + name5.Text + ".reverse();");
                if (rev6.IsChecked == true)
                    writer.WriteLine("  " + name6.Text + ".reverse();");
                if (rev7.IsChecked == true)
                    writer.WriteLine("  " + name7.Text + ".reverse();");
                if (rev8.IsChecked == true)
                    writer.WriteLine("  " + name8.Text + ".reverse();");
                if (rev9.IsChecked == true)
                    writer.WriteLine("  " + name9.Text + ".reverse();");
                if (rev10.IsChecked == true)
                    writer.WriteLine("  " + name10.Text + ".reverse();");
                if (rev11.IsChecked == true)
                    writer.WriteLine("  " + name11.Text + ".reverse();");
                if (rev12.IsChecked == true)
                    writer.WriteLine("  " + name12.Text + ".reverse();");
                if (rev13.IsChecked == true)
                    writer.WriteLine("  " + name13.Text + ".reverse();");
                if (rev14.IsChecked == true)
                    writer.WriteLine("  " + name14.Text + ".reverse();");
                if (rev15.IsChecked == true)
                    writer.WriteLine("  " + name15.Text + ".reverse();");
                if (rev16.IsChecked == true)
                    writer.WriteLine("  " + name16.Text + ".reverse();");
                if (rev17.IsChecked == true)
                    writer.WriteLine("  " + name17.Text + ".reverse();");
                if (rev18.IsChecked == true)
                    writer.WriteLine("  " + name18.Text + ".reverse();");
                if (rev19.IsChecked == true)
                    writer.WriteLine("  " + name19.Text + ".reverse();");
                if (rev20.IsChecked == true)
                    writer.WriteLine("  " + name20.Text + ".reverse();");
                writer.WriteLine();
                writer.WriteLine("  delay(100);");
                writer.WriteLine("  parameter(modus);");
                writer.WriteLine("}");
                writer.WriteLine();

                writer.WriteLine("void loop()");                                          //Loop
                writer.WriteLine("{");
                if(rcmode4.IsChecked == false)
                    writer.WriteLine("  int16_t value = getPercent();");

                if (rcmode1.IsChecked == true)                                            //Zeit Modus
                {
                    writer.WriteLine("  if(value >= 25)             //Automatic Mode");
                    writer.WriteLine("  {");
                    writer.WriteLine("    digitalWrite(SWITCH_PIN, HIGH);  //Positionslicht einschalten");
                    if (act1.IsChecked == true)
                        writer.WriteLine("    " + name1.Text + ".update();                  //Strips updaten");
                    if (act2.IsChecked == true)
                        writer.WriteLine("    " + name2.Text + ".update();");
                    if (act3.IsChecked == true)
                        writer.WriteLine("    " + name3.Text + ".update();");
                    if (act4.IsChecked == true)
                        writer.WriteLine("    " + name4.Text + ".update();");
                    if (act5.IsChecked == true)
                        writer.WriteLine("    " + name5.Text + ".update();");
                    if (act6.IsChecked == true)
                        writer.WriteLine("    " + name6.Text + ".update();");
                    if (act7.IsChecked == true)
                        writer.WriteLine("    " + name7.Text + ".update();");
                    if (act8.IsChecked == true)
                        writer.WriteLine("    " + name8.Text + ".update();");
                    if (act9.IsChecked == true)
                        writer.WriteLine("    " + name9.Text + ".update();");
                    if (act10.IsChecked == true)
                        writer.WriteLine("    " + name10.Text + ".update();");
                    if (act11.IsChecked == true)
                        writer.WriteLine("    " + name11.Text + ".update();");
                    if (act12.IsChecked == true)
                        writer.WriteLine("    " + name12.Text + ".update();");
                    if (act13.IsChecked == true)
                        writer.WriteLine("    " + name13.Text + ".update();");
                    if (act14.IsChecked == true)
                        writer.WriteLine("    " + name14.Text + ".update();");
                    if (act15.IsChecked == true)
                        writer.WriteLine("    " + name15.Text + ".update();");
                    if (act16.IsChecked == true)
                        writer.WriteLine("    " + name16.Text + ".update();");
                    if (act17.IsChecked == true)
                        writer.WriteLine("    " + name17.Text + ".update();");
                    if (act18.IsChecked == true)
                        writer.WriteLine("    " + name18.Text + ".update();");
                    if (act19.IsChecked == true)
                        writer.WriteLine("    " + name19.Text + ".update();");
                    if (act20.IsChecked == true)
                        writer.WriteLine("    " + name20.Text + ".update();");
                    writer.WriteLine("    showStrips();                    //Update anzeigen");
                    writer.WriteLine("    if((millis() - lastSwitch) > UPDATE)  //Nach Update Zeit in den nächsten Modus schalten");
                    writer.WriteLine("    {");
                    writer.WriteLine("      lastSwitch = millis();");
                    if (nextSwitch.IsChecked == true)
                    {
                        writer.WriteLine("      if(modus < (MODE_ANZAHL - 1))");
                        writer.WriteLine("        modus++;");
                        writer.WriteLine("      else");
                        writer.WriteLine("        modus = 0;");
                    }
                    else
                    {
                        writer.WriteLine("      modus = random(0, " + (int)modeSlider.Value + ");");
                    }
                    writer.WriteLine("      parameter(modus);");
                    if (act1.IsChecked == true)
                        writer.WriteLine("      " + name1.Text + ".setMode(modus);");
                    if (act2.IsChecked == true)
                        writer.WriteLine("      " + name2.Text + ".setMode(modus);");
                    if (act3.IsChecked == true)
                        writer.WriteLine("      " + name3.Text + ".setMode(modus);");
                    if (act4.IsChecked == true)
                        writer.WriteLine("      " + name4.Text + ".setMode(modus);");
                    if (act5.IsChecked == true)
                        writer.WriteLine("      " + name5.Text + ".setMode(modus);");
                    if (act6.IsChecked == true)
                        writer.WriteLine("      " + name6.Text + ".setMode(modus);");
                    if (act7.IsChecked == true)
                        writer.WriteLine("      " + name7.Text + ".setMode(modus);");
                    if (act8.IsChecked == true)
                        writer.WriteLine("      " + name8.Text + ".setMode(modus);");
                    if (act9.IsChecked == true)
                        writer.WriteLine("      " + name9.Text + ".setMode(modus);");
                    if (act10.IsChecked == true)
                        writer.WriteLine("      " + name10.Text + ".setMode(modus);");
                    if (act11.IsChecked == true)
                        writer.WriteLine("      " + name11.Text + ".setMode(modus);");
                    if (act12.IsChecked == true)
                        writer.WriteLine("      " + name12.Text + ".setMode(modus);");
                    if (act13.IsChecked == true)
                        writer.WriteLine("      " + name13.Text + ".setMode(modus);");
                    if (act14.IsChecked == true)
                        writer.WriteLine("      " + name14.Text + ".setMode(modus);");
                    if (act15.IsChecked == true)
                        writer.WriteLine("      " + name15.Text + ".setMode(modus);");
                    if (act16.IsChecked == true)
                        writer.WriteLine("      " + name16.Text + ".setMode(modus);");
                    if (act17.IsChecked == true)
                        writer.WriteLine("      " + name17.Text + ".setMode(modus);");
                    if (act18.IsChecked == true)
                        writer.WriteLine("      " + name18.Text + ".setMode(modus);");
                    if (act19.IsChecked == true)
                        writer.WriteLine("      " + name19.Text + ".setMode(modus);");
                    if (act20.IsChecked == true)
                        writer.WriteLine("      " + name20.Text + ".setMode(modus);");
                    writer.WriteLine("    }");
                    writer.WriteLine("  }");
                    writer.WriteLine("  else if(value >= -25)       //Start Mode");
                    writer.WriteLine("  {");
                    writer.WriteLine("    digitalWrite(SWITCH_PIN, HIGH);  //Positionslicht einschalten");
                    writer.WriteLine("    clearStrips();                   //Strips ausschalten");
                    writer.WriteLine("  }");
                    writer.WriteLine("  else ");
                    writer.WriteLine("  {");
                    writer.WriteLine("    digitalWrite(SWITCH_PIN, LOW);   //Positionslicht ausschalten");
                    writer.WriteLine("    clearStrips();                   //Strips ausschalten");
                    writer.WriteLine("  }");
                }
                else if(rcmode2.IsChecked == true)      //Poti Mode
                {
                    Int16 grenze = (Int16)((-100) + (200/((int)modeSlider.Value + 1)));
                    writer.WriteLine("  if(value >= " + grenze + ")");
                    writer.WriteLine("  {");
                    writer.WriteLine("    digitalWrite(SWITCH_PIN, HIGH);");
                    if (act1.IsChecked == true)
                        writer.WriteLine("    " + name1.Text + ".update();                  //Strips updaten");
                    if (act2.IsChecked == true)
                        writer.WriteLine("    " + name2.Text + ".update();");
                    if (act3.IsChecked == true)
                        writer.WriteLine("    " + name3.Text + ".update();");
                    if (act4.IsChecked == true)
                        writer.WriteLine("    " + name4.Text + ".update();");
                    if (act5.IsChecked == true)
                        writer.WriteLine("    " + name5.Text + ".update();");
                    if (act6.IsChecked == true)
                        writer.WriteLine("    " + name6.Text + ".update();");
                    if (act7.IsChecked == true)
                        writer.WriteLine("    " + name7.Text + ".update();");
                    if (act8.IsChecked == true)
                        writer.WriteLine("    " + name8.Text + ".update();");
                    if (act9.IsChecked == true)
                        writer.WriteLine("    " + name9.Text + ".update();");
                    if (act10.IsChecked == true)
                        writer.WriteLine("    " + name10.Text + ".update();");
                    if (act11.IsChecked == true)
                        writer.WriteLine("    " + name11.Text + ".update();");
                    if (act12.IsChecked == true)
                        writer.WriteLine("    " + name12.Text + ".update();");
                    if (act13.IsChecked == true)
                        writer.WriteLine("    " + name13.Text + ".update();");
                    if (act14.IsChecked == true)
                        writer.WriteLine("    " + name14.Text + ".update();");
                    if (act15.IsChecked == true)
                        writer.WriteLine("    " + name15.Text + ".update();");
                    if (act16.IsChecked == true)
                        writer.WriteLine("    " + name16.Text + ".update();");
                    if (act17.IsChecked == true)
                        writer.WriteLine("    " + name17.Text + ".update();");
                    if (act18.IsChecked == true)
                        writer.WriteLine("    " + name18.Text + ".update();");
                    if (act19.IsChecked == true)
                        writer.WriteLine("    " + name19.Text + ".update();");
                    if (act20.IsChecked == true)
                        writer.WriteLine("    " + name20.Text + ".update();");
                    writer.WriteLine("    showStrips();                    //Update anzeigen");
                    writer.WriteLine("  }");
                    writer.WriteLine("  else");
                    writer.WriteLine("  {");
                    writer.WriteLine("    digitalWrite(SWITCH_PIN, LOW);");
                    writer.WriteLine("    clearStrips();");
                    writer.WriteLine("  }");
                    writer.WriteLine();
                    grenze = (Int16)((-100) + ((int)modeSlider.Value*200 / ((int)modeSlider.Value + 1)));
                    writer.WriteLine("  if(value >= " + grenze + ")");
                    writer.WriteLine("  {");
                    writer.WriteLine("    modus = " + ((int)modeSlider.Value - 1) + ";");
                    writer.WriteLine("  }");

                    for (int modeCounter = ((int)modeSlider.Value - 2); modeCounter >= 0; modeCounter--)
                    {
                        grenze = (Int16)((-100) + ((modeCounter+1) * 200 / ((int)modeSlider.Value + 1)));
                        writer.WriteLine("  else if(value >= " + grenze + ")");
                        writer.WriteLine("  {");
                        writer.WriteLine("    modus = " + modeCounter + ";");
                        writer.WriteLine("  }");
                    }

                    writer.WriteLine();

                    writer.WriteLine("  if(lastModus != modus)");
                    writer.WriteLine("  {");
                    writer.WriteLine("    parameter(modus);");
                    if (act1.IsChecked == true)
                        writer.WriteLine("    " + name1.Text + ".setMode(modus);");
                    if (act2.IsChecked == true)
                        writer.WriteLine("    " + name2.Text + ".setMode(modus);");
                    if (act3.IsChecked == true)
                        writer.WriteLine("    " + name3.Text + ".setMode(modus);");
                    if (act4.IsChecked == true)
                        writer.WriteLine("    " + name4.Text + ".setMode(modus);");
                    if (act5.IsChecked == true)
                        writer.WriteLine("    " + name5.Text + ".setMode(modus);");
                    if (act6.IsChecked == true)
                        writer.WriteLine("    " + name6.Text + ".setMode(modus);");
                    if (act7.IsChecked == true)
                        writer.WriteLine("    " + name7.Text + ".setMode(modus);");
                    if (act8.IsChecked == true)
                        writer.WriteLine("    " + name8.Text + ".setMode(modus);");
                    if (act9.IsChecked == true)
                        writer.WriteLine("    " + name9.Text + ".setMode(modus);");
                    if (act10.IsChecked == true)
                        writer.WriteLine("    " + name10.Text + ".setMode(modus);");
                    if (act11.IsChecked == true)
                        writer.WriteLine("    " + name11.Text + ".setMode(modus);");
                    if (act12.IsChecked == true)
                        writer.WriteLine("    " + name12.Text + ".setMode(modus);");
                    if (act13.IsChecked == true)
                        writer.WriteLine("    " + name13.Text + ".setMode(modus);");
                    if (act14.IsChecked == true)
                        writer.WriteLine("    " + name14.Text + ".setMode(modus);");
                    if (act15.IsChecked == true)
                        writer.WriteLine("    " + name15.Text + ".setMode(modus);");
                    if (act16.IsChecked == true)
                        writer.WriteLine("    " + name16.Text + ".setMode(modus);");
                    if (act17.IsChecked == true)
                        writer.WriteLine("    " + name17.Text + ".setMode(modus);");
                    if (act18.IsChecked == true)
                        writer.WriteLine("    " + name18.Text + ".setMode(modus);");
                    if (act19.IsChecked == true)
                        writer.WriteLine("    " + name19.Text + ".setMode(modus);");
                    if (act20.IsChecked == true)
                        writer.WriteLine("    " + name20.Text + ".setMode(modus);");
                    writer.WriteLine("  lastModus = modus;");
                    writer.WriteLine("  }");
                }
                else if(rcmode3.IsChecked == true)      //Manuell Mode
                {
                    writer.WriteLine("  if(value >= 25 && millis()-lastInc >= INCTIME)             //Inc Modus");
                    writer.WriteLine("  {");
                    writer.WriteLine("    lastInc = millis();");
                    writer.WriteLine("    if(modus < (MODE_ANZAHL - 1))");
                    writer.WriteLine("      modus++;");
                    writer.WriteLine("    else");
                    writer.WriteLine("      modus = 0;");
                    writer.WriteLine("    parameter(modus);");
                    if (act1.IsChecked == true)
                        writer.WriteLine("    " + name1.Text + ".setMode(modus);");
                    if (act2.IsChecked == true)
                        writer.WriteLine("    " + name2.Text + ".setMode(modus);");
                    if (act3.IsChecked == true)
                        writer.WriteLine("    " + name3.Text + ".setMode(modus);");
                    if (act4.IsChecked == true)
                        writer.WriteLine("    " + name4.Text + ".setMode(modus);");
                    if (act5.IsChecked == true)
                        writer.WriteLine("    " + name5.Text + ".setMode(modus);");
                    if (act6.IsChecked == true)
                        writer.WriteLine("    " + name6.Text + ".setMode(modus);");
                    if (act7.IsChecked == true)
                        writer.WriteLine("    " + name7.Text + ".setMode(modus);");
                    if (act8.IsChecked == true)
                        writer.WriteLine("    " + name8.Text + ".setMode(modus);");
                    if (act9.IsChecked == true)
                        writer.WriteLine("    " + name9.Text + ".setMode(modus);");
                    if (act10.IsChecked == true)
                        writer.WriteLine("    " + name10.Text + ".setMode(modus);");
                    if (act11.IsChecked == true)
                        writer.WriteLine("    " + name11.Text + ".setMode(modus);");
                    if (act12.IsChecked == true)
                        writer.WriteLine("    " + name12.Text + ".setMode(modus);");
                    if (act13.IsChecked == true)
                        writer.WriteLine("    " + name13.Text + ".setMode(modus);");
                    if (act14.IsChecked == true)
                        writer.WriteLine("    " + name14.Text + ".setMode(modus);");
                    if (act15.IsChecked == true)
                        writer.WriteLine("    " + name15.Text + ".setMode(modus);");
                    if (act16.IsChecked == true)
                        writer.WriteLine("    " + name16.Text + ".setMode(modus);");
                    if (act17.IsChecked == true)
                        writer.WriteLine("    " + name17.Text + ".setMode(modus);");
                    if (act18.IsChecked == true)
                        writer.WriteLine("    " + name18.Text + ".setMode(modus);");
                    if (act19.IsChecked == true)
                        writer.WriteLine("    " + name19.Text + ".setMode(modus);");
                    if (act20.IsChecked == true)
                        writer.WriteLine("    " + name20.Text + ".setMode(modus);");
                    writer.WriteLine("  }");
                    writer.WriteLine("  else if(value >= -25)       //On");
                    writer.WriteLine("  {");
                    writer.WriteLine("    digitalWrite(SWITCH_PIN, HIGH);  //Positionslicht einschalten");
                    if (act1.IsChecked == true)
                        writer.WriteLine("    " + name1.Text + ".update();                  //Strips updaten");
                    if (act2.IsChecked == true)
                        writer.WriteLine("    " + name2.Text + ".update();");
                    if (act3.IsChecked == true)
                        writer.WriteLine("    " + name3.Text + ".update();");
                    if (act4.IsChecked == true)
                        writer.WriteLine("    " + name4.Text + ".update();");
                    if (act5.IsChecked == true)
                        writer.WriteLine("    " + name5.Text + ".update();");
                    if (act6.IsChecked == true)
                        writer.WriteLine("    " + name6.Text + ".update();");
                    if (act7.IsChecked == true)
                        writer.WriteLine("    " + name7.Text + ".update();");
                    if (act8.IsChecked == true)
                        writer.WriteLine("    " + name8.Text + ".update();");
                    if (act9.IsChecked == true)
                        writer.WriteLine("    " + name9.Text + ".update();");
                    if (act10.IsChecked == true)
                        writer.WriteLine("    " + name10.Text + ".update();");
                    if (act11.IsChecked == true)
                        writer.WriteLine("    " + name11.Text + ".update();");
                    if (act12.IsChecked == true)
                        writer.WriteLine("    " + name12.Text + ".update();");
                    if (act13.IsChecked == true)
                        writer.WriteLine("    " + name13.Text + ".update();");
                    if (act14.IsChecked == true)
                        writer.WriteLine("    " + name14.Text + ".update();");
                    if (act15.IsChecked == true)
                        writer.WriteLine("    " + name15.Text + ".update();");
                    if (act16.IsChecked == true)
                        writer.WriteLine("    " + name16.Text + ".update();");
                    if (act17.IsChecked == true)
                        writer.WriteLine("    " + name17.Text + ".update();");
                    if (act18.IsChecked == true)
                        writer.WriteLine("    " + name18.Text + ".update();");
                    if (act19.IsChecked == true)
                        writer.WriteLine("    " + name19.Text + ".update();");
                    if (act20.IsChecked == true)
                        writer.WriteLine("    " + name20.Text + ".update();");
                    writer.WriteLine("    showStrips();");
                    writer.WriteLine("  }");
                    writer.WriteLine("  else ");
                    writer.WriteLine("  {");
                    writer.WriteLine("    digitalWrite(SWITCH_PIN, LOW);   //Positionslicht ausschalten");
                    writer.WriteLine("    clearStrips();                   //Strips ausschalten");
                    writer.WriteLine("  }");
                }
                else if(rcmode4.IsChecked == true)      //Ohne RC
                {
                    writer.WriteLine("  digitalWrite(SWITCH_PIN, HIGH);  //Positionslicht einschalten");
                    if (act1.IsChecked == true)
                        writer.WriteLine("  " + name1.Text + ".update();                  //Strips updaten");
                    if (act2.IsChecked == true)
                        writer.WriteLine("  " + name2.Text + ".update();");
                    if (act3.IsChecked == true)
                        writer.WriteLine("  " + name3.Text + ".update();");
                    if (act4.IsChecked == true)
                        writer.WriteLine("  " + name4.Text + ".update();");
                    if (act5.IsChecked == true)
                        writer.WriteLine("  " + name5.Text + ".update();");
                    if (act6.IsChecked == true)
                        writer.WriteLine("  " + name6.Text + ".update();");
                    if (act7.IsChecked == true)
                        writer.WriteLine("  " + name7.Text + ".update();");
                    if (act8.IsChecked == true)
                        writer.WriteLine("  " + name8.Text + ".update();");
                    if (act9.IsChecked == true)
                        writer.WriteLine("  " + name9.Text + ".update();");
                    if (act10.IsChecked == true)
                        writer.WriteLine("  " + name10.Text + ".update();");
                    if (act11.IsChecked == true)
                        writer.WriteLine("  " + name11.Text + ".update();");
                    if (act12.IsChecked == true)
                        writer.WriteLine("  " + name12.Text + ".update();");
                    if (act13.IsChecked == true)
                        writer.WriteLine("  " + name13.Text + ".update();");
                    if (act14.IsChecked == true)
                        writer.WriteLine("  " + name14.Text + ".update();");
                    if (act15.IsChecked == true)
                        writer.WriteLine("  " + name15.Text + ".update();");
                    if (act16.IsChecked == true)
                        writer.WriteLine("  " + name16.Text + ".update();");
                    if (act17.IsChecked == true)
                        writer.WriteLine("  " + name17.Text + ".update();");
                    if (act18.IsChecked == true)
                        writer.WriteLine("  " + name18.Text + ".update();");
                    if (act19.IsChecked == true)
                        writer.WriteLine("  " + name19.Text + ".update();");
                    if (act20.IsChecked == true)
                        writer.WriteLine("  " + name20.Text + ".update();");
                    writer.WriteLine("  showStrips();                    //Update anzeigen");
                    writer.WriteLine("  if((millis() - lastSwitch) > UPDATE)  //Nach Update Zeit in den nächsten Modus schalten");
                    writer.WriteLine("  {");
                    writer.WriteLine("    lastSwitch = millis();");
                    if (nextSwitch.IsChecked == true)
                    {
                        writer.WriteLine("    if(modus < (MODE_ANZAHL - 1))");
                        writer.WriteLine("      modus++;");
                        writer.WriteLine("    else");
                        writer.WriteLine("      modus = 0;");
                    }
                    else
                    {
                        writer.WriteLine("    modus = random(0, " + (int)modeSlider.Value + ");");
                    }
                    writer.WriteLine("    parameter(modus);");
                    if (act1.IsChecked == true)
                        writer.WriteLine("    " + name1.Text + ".setMode(modus);");
                    if (act2.IsChecked == true)
                        writer.WriteLine("    " + name2.Text + ".setMode(modus);");
                    if (act3.IsChecked == true)
                        writer.WriteLine("    " + name3.Text + ".setMode(modus);");
                    if (act4.IsChecked == true)
                        writer.WriteLine("    " + name4.Text + ".setMode(modus);");
                    if (act5.IsChecked == true)
                        writer.WriteLine("    " + name5.Text + ".setMode(modus);");
                    if (act6.IsChecked == true)
                        writer.WriteLine("    " + name6.Text + ".setMode(modus);");
                    if (act7.IsChecked == true)
                        writer.WriteLine("    " + name7.Text + ".setMode(modus);");
                    if (act8.IsChecked == true)
                        writer.WriteLine("    " + name8.Text + ".setMode(modus);");
                    if (act9.IsChecked == true)
                        writer.WriteLine("    " + name9.Text + ".setMode(modus);");
                    if (act10.IsChecked == true)
                        writer.WriteLine("    " + name10.Text + ".setMode(modus);");
                    if (act11.IsChecked == true)
                        writer.WriteLine("    " + name11.Text + ".setMode(modus);");
                    if (act12.IsChecked == true)
                        writer.WriteLine("    " + name12.Text + ".setMode(modus);");
                    if (act13.IsChecked == true)
                        writer.WriteLine("    " + name13.Text + ".setMode(modus);");
                    if (act14.IsChecked == true)
                        writer.WriteLine("    " + name14.Text + ".setMode(modus);");
                    if (act15.IsChecked == true)
                        writer.WriteLine("    " + name15.Text + ".setMode(modus);");
                    if (act16.IsChecked == true)
                        writer.WriteLine("    " + name16.Text + ".setMode(modus);");
                    if (act17.IsChecked == true)
                        writer.WriteLine("    " + name17.Text + ".setMode(modus);");
                    if (act18.IsChecked == true)
                        writer.WriteLine("    " + name18.Text + ".setMode(modus);");
                    if (act19.IsChecked == true)
                        writer.WriteLine("    " + name19.Text + ".setMode(modus);");
                    if (act20.IsChecked == true)
                        writer.WriteLine("    " + name20.Text + ".setMode(modus);");
                    writer.WriteLine("  }");
                }
                writer.WriteLine("}");
                writer.WriteLine();


                writer.WriteLine("void showStrips()");
                writer.WriteLine("{");
                writer.WriteLine("  if((micros() - lastShow) >= 25000)");
                writer.WriteLine("  {");
                writer.WriteLine("    strip.show();");
                if(strip2act.IsChecked == true)
                    writer.WriteLine("    strip2.show();");
                writer.WriteLine("    lastShow = micros();");
                writer.WriteLine("  }");
                writer.WriteLine("}");
                writer.WriteLine();
                writer.WriteLine("void clearStrips()");
                writer.WriteLine("{");
                writer.WriteLine("  for(uint8_t a = 0 ; a < LED1_ANZAHL ; a++)");
                writer.WriteLine("  {");
                writer.WriteLine("    strip.setPixelColor(a, 0);");
                writer.WriteLine("  }");
                if (strip2act.IsChecked == true)
                {
                    writer.WriteLine("  for(uint8_t a = 0 ; a < LED2_ANZAHL ; a++)");
                    writer.WriteLine("  {");
                    writer.WriteLine("    strip2.setPixelColor(a, 0);");
                    writer.WriteLine("  }");
                }
                writer.WriteLine("    showStrips();");
                writer.WriteLine("}");
                writer.WriteLine();
                writer.WriteLine("void parameter(uint8_t mode)");
                writer.WriteLine("{");
                writer.WriteLine("  switch(mode)");
                writer.WriteLine("  {");
                for (int modus = 0; modus < (int)modeSlider.Value; modus++)
                {
                    writer.WriteLine("    case " + modus + ":");
                    if (act1.IsChecked == true)
                        writer.WriteLine("      " + name1.Text + ".updateParameter(" + effects[Convert.ToInt16(para[modus, 0, 0])] + ", " + para[modus, 0, 1] + ", " + para[modus, 0, 2] + ", " + para[modus, 0, 3] + ", " + para[modus, 0, 4] + ");");
                    if (act2.IsChecked == true)
                        writer.WriteLine("      " + name2.Text + ".updateParameter(" + effects[Convert.ToInt16(para[modus, 1, 0])] + ", " + para[modus, 1, 1] + ", " + para[modus, 1, 2] + ", " + para[modus, 1, 3] + ", " + para[modus, 1, 4] + ");");
                    if (act3.IsChecked == true)
                        writer.WriteLine("      " + name3.Text + ".updateParameter(" + effects[Convert.ToInt16(para[modus, 2, 0])] + ", " + para[modus, 2, 1] + ", " + para[modus, 2, 2] + ", " + para[modus, 2, 3] + ", " + para[modus, 2, 4] + ");");
                    if (act4.IsChecked == true)
                        writer.WriteLine("      " + name4.Text + ".updateParameter(" + effects[Convert.ToInt16(para[modus, 3, 0])] + ", " + para[modus, 3, 1] + ", " + para[modus, 3, 2] + ", " + para[modus, 3, 3] + ", " + para[modus, 3, 4] + ");");
                    if (act5.IsChecked == true)
                        writer.WriteLine("      " + name5.Text + ".updateParameter(" + effects[Convert.ToInt16(para[modus, 4, 0])] + ", " + para[modus, 4, 1] + ", " + para[modus, 4, 2] + ", " + para[modus, 4, 3] + ", " + para[modus, 4, 4] + ");");
                    if (act6.IsChecked == true)
                        writer.WriteLine("      " + name6.Text + ".updateParameter(" + effects[Convert.ToInt16(para[modus, 5, 0])] + ", " + para[modus, 5, 1] + ", " + para[modus, 5, 2] + ", " + para[modus, 5, 3] + ", " + para[modus, 5, 4] + ");");
                    if (act7.IsChecked == true)
                        writer.WriteLine("      " + name7.Text + ".updateParameter(" + effects[Convert.ToInt16(para[modus, 6, 0])] + ", " + para[modus, 6, 1] + ", " + para[modus, 6, 2] + ", " + para[modus, 6, 3] + ", " + para[modus, 6, 4] + ");");
                    if (act8.IsChecked == true)
                        writer.WriteLine("      " + name8.Text + ".updateParameter(" + effects[Convert.ToInt16(para[modus, 7, 0])] + ", " + para[modus, 7, 1] + ", " + para[modus, 7, 2] + ", " + para[modus, 7, 3] + ", " + para[modus, 7, 4] + ");");
                    if (act9.IsChecked == true)
                        writer.WriteLine("      " + name9.Text + ".updateParameter(" + effects[Convert.ToInt16(para[modus, 8, 0])] + ", " + para[modus, 8, 1] + ", " + para[modus, 8, 2] + ", " + para[modus, 8, 3] + ", " + para[modus, 8, 4] + ");");
                    if (act10.IsChecked == true)
                        writer.WriteLine("      " + name10.Text + ".updateParameter(" + effects[Convert.ToInt16(para[modus, 9, 0])] + ", " + para[modus, 9, 1] + ", " + para[modus, 9, 2] + ", " + para[modus, 9, 3] + ", " + para[modus, 9, 4] + ");");
                    if (act11.IsChecked == true)
                        writer.WriteLine("      " + name11.Text + ".updateParameter(" + effects[Convert.ToInt16(para[modus, 10, 0])] + ", " + para[modus, 10, 1] + ", " + para[modus, 10, 2] + ", " + para[modus, 10, 3] + ", " + para[modus, 10, 4] + ");");
                    if (act12.IsChecked == true)
                        writer.WriteLine("      " + name12.Text + ".updateParameter(" + effects[Convert.ToInt16(para[modus, 11, 0])] + ", " + para[modus, 11, 1] + ", " + para[modus, 11, 2] + ", " + para[modus, 11, 3] + ", " + para[modus, 11, 4] + ");");
                    if (act13.IsChecked == true)
                        writer.WriteLine("      " + name13.Text + ".updateParameter(" + effects[Convert.ToInt16(para[modus, 12, 0])] + ", " + para[modus, 12, 1] + ", " + para[modus, 12, 2] + ", " + para[modus, 12, 3] + ", " + para[modus, 12, 4] + ");");
                    if (act14.IsChecked == true)
                        writer.WriteLine("      " + name14.Text + ".updateParameter(" + effects[Convert.ToInt16(para[modus, 13, 0])] + ", " + para[modus, 13, 1] + ", " + para[modus, 13, 2] + ", " + para[modus, 13, 3] + ", " + para[modus, 13, 4] + ");");
                    if (act15.IsChecked == true)
                        writer.WriteLine("      " + name15.Text + ".updateParameter(" + effects[Convert.ToInt16(para[modus, 14, 0])] + ", " + para[modus, 14, 1] + ", " + para[modus, 14, 2] + ", " + para[modus, 14, 3] + ", " + para[modus, 14, 4] + ");");
                    if (act16.IsChecked == true)
                        writer.WriteLine("      " + name16.Text + ".updateParameter(" + effects[Convert.ToInt16(para[modus, 15, 0])] + ", " + para[modus, 15, 1] + ", " + para[modus, 15, 2] + ", " + para[modus, 15, 3] + ", " + para[modus, 15, 4] + ");");
                    if (act17.IsChecked == true)
                        writer.WriteLine("      " + name17.Text + ".updateParameter(" + effects[Convert.ToInt16(para[modus, 16, 0])] + ", " + para[modus, 16, 1] + ", " + para[modus, 16, 2] + ", " + para[modus, 16, 3] + ", " + para[modus, 16, 4] + ");");
                    if (act18.IsChecked == true)
                        writer.WriteLine("      " + name18.Text + ".updateParameter(" + effects[Convert.ToInt16(para[modus, 17, 0])] + ", " + para[modus, 17, 1] + ", " + para[modus, 17, 2] + ", " + para[modus, 17, 3] + ", " + para[modus, 17, 4] + ");");
                    if (act19.IsChecked == true)
                        writer.WriteLine("      " + name19.Text + ".updateParameter(" + effects[Convert.ToInt16(para[modus, 18, 0])] + ", " + para[modus, 18, 1] + ", " + para[modus, 18, 2] + ", " + para[modus, 18, 3] + ", " + para[modus, 18, 4] + ");");
                    if (act20.IsChecked == true)
                        writer.WriteLine("      " + name20.Text + ".updateParameter(" + effects[Convert.ToInt16(para[modus, 19, 0])] + ", " + para[modus, 19, 1] + ", " + para[modus, 19, 2] + ", " + para[modus, 19, 3] + ", " + para[modus, 19, 4] + ");");
                    writer.WriteLine("    break;");
                }
                writer.WriteLine("  }");
                writer.WriteLine("}");

                pfad = System.IO.Path.GetDirectoryName(dlg.FileName);
                datei = dlg.FileName;
                name = dlg.SafeFileName;
                
                writer.Dispose();
                writer.Close();
            }

            overpfad = System.IO.Path.GetDirectoryName(pfad);

            overordner = pfad.Replace(overpfad, "");
            overordner = overordner.Replace(@"\", "");

            if (overordner != name.Replace(".ino", ""))
            {
                System.IO.Directory.CreateDirectory(pfad + @"\" + name.Replace(".ino", ""));
                System.IO.File.Move(datei, pfad + @"\" + name.Replace(".ino", "") + @"\" + name);

                string exeLocation = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                string exeDir = System.IO.Path.GetDirectoryName(exeLocation);
                exeDir = exeDir.Replace(@"file:\", "");

                try
                {
                    System.IO.File.Copy(exeDir + @"\data\FlightStrip.h", pfad + @"\" + name.Replace(".ino", "") + @"\FlightStrip.h", true);
                    System.IO.File.Copy(exeDir + @"\data\FlightStrip.cpp", pfad + @"\" + name.Replace(".ino", "") + @"\FlightStrip.cpp", true);
                    System.IO.File.Copy(exeDir + @"\data\RC.ino", pfad + @"\" + name.Replace(".ino", "") + @"\RC.ino", true);
                }
                catch
                {
                    System.Windows.MessageBox.Show("FlightStrip.h, FlightStrip.cpp oder RC.ino konnte nicht gefunden werden.", "Fehler");
                }
            }
        }

        private void openPara(int bereich)
        {
            try
            {
                switch (effects[Convert.ToInt16(para[(int)modeSelectSlider.Value - 1, bereich, 0])])
                {
                    case "CONST_COLOR":
                    case "DOUBLEFLASH":
                    case "THUNDERSTORM":
                        OneColorOnly win = new OneColorOnly();
                        win.effectText.Text = effects[Convert.ToInt16(para[(int)modeSelectSlider.Value - 1, bereich, 0])];
                        win.color1.R = Convert.ToByte(Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 1]) >> 16);
                        win.color1.G = Convert.ToByte((UInt16)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 1]) >> 8);
                        win.color1.B = (byte)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 1]);
                        win.ShowDialog();
                        if (win.DialogResult.HasValue && win.DialogResult.Value)
                        {
                            UInt32 r = win.color1.R;
                            UInt32 g = win.color1.G;
                            UInt32 b = win.color1.B;
                            UInt32 color = (((UInt32)r << 16) | ((UInt32)g << 8) | b);
                            para[(int)modeSelectSlider.Value - 1, bereich, 1] = color.ToString();

                            //int red = Convert.ToInt16(color >> 16);
                            //int gre = Convert.ToInt16((UInt16)color >> 8);
                            //int blu = Convert.ToInt16((byte)color);

                            //System.Windows.MessageBox.Show(para[(int)modeSelectSlider.Value - 1, 0, 1] + "  Red : " + red + "  Green : " + gre + "  Blue : " + blu);

                        }
                        break;
                    case "CONST_TRIPLE_COLOR":
                    case "CHASE_TRIPLE_COLOR":
                    case "FILL_TRIPLE_COLOR":
                        ThreeColorPlusTime win2 = new ThreeColorPlusTime();
                        win2.effectText.Text = effects[Convert.ToInt16(para[(int)modeSelectSlider.Value - 1, bereich, 0])];
                        win2.color1.R = Convert.ToByte(Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 1]) >> 16);
                        win2.color1.G = Convert.ToByte((UInt16)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 1]) >> 8);
                        win2.color1.B = Convert.ToByte((byte)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 1]));
                        win2.color2.R = Convert.ToByte(Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 2]) >> 16);
                        win2.color2.G = Convert.ToByte((UInt16)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 2]) >> 8);
                        win2.color2.B = Convert.ToByte((byte)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 2]));
                        win2.color3.R = Convert.ToByte(Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 3]) >> 16);
                        win2.color3.G = Convert.ToByte((UInt16)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 3]) >> 8);
                        win2.color3.B = Convert.ToByte((byte)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 3]));
                        win2.time.Value = Convert.ToInt16(para[(int)modeSelectSlider.Value - 1, bereich, 4]);
                        win2.ShowDialog();
                        if (win2.DialogResult.HasValue && win2.DialogResult.Value)
                        {
                            UInt32 r = win2.color1.R;
                            UInt32 g = win2.color1.G;
                            UInt32 b = win2.color1.B;
                            UInt32 color = (((UInt32)r << 16) | ((UInt32)g << 8) | b);
                            para[(int)modeSelectSlider.Value - 1, bereich, 1] = color.ToString();

                            r = win2.color2.R;
                            g = win2.color2.G;
                            b = win2.color2.B;
                            color = (((UInt32)r << 16) | ((UInt32)g << 8) | b);
                            para[(int)modeSelectSlider.Value - 1, bereich, 2] = color.ToString();

                            r = win2.color3.R;
                            g = win2.color3.G;
                            b = win2.color3.B;
                            color = (((UInt32)r << 16) | ((UInt32)g << 8) | b);
                            para[(int)modeSelectSlider.Value - 1, bereich, 3] = color.ToString();

                            para[(int)modeSelectSlider.Value - 1, bereich, 4] = win2.time.Value.ToString();
                        }
                        break;
                    case "CHASE_SINGLE_COLOR":
                    case "THEATER_CHASE":
                        OneColorPlusTime win3 = new OneColorPlusTime();
                        win3.effectText.Text = effects[Convert.ToInt16(para[(int)modeSelectSlider.Value - 1, bereich, 0])];
                        win3.color1.R = Convert.ToByte(Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 1]) >> 16);
                        win3.color1.G = Convert.ToByte((UInt16)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 1]) >> 8);
                        win3.color1.B = Convert.ToByte((byte)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 1]));
                        win3.time.Value = Convert.ToInt16(para[(int)modeSelectSlider.Value - 1, bereich, 4]);
                        win3.ShowDialog();
                        if (win3.DialogResult.HasValue && win3.DialogResult.Value)
                        {
                            UInt32 r = win3.color1.R;
                            UInt32 g = win3.color1.G;
                            UInt32 b = win3.color1.B;
                            UInt32 color = (((UInt32)r << 16) | ((UInt32)g << 8) | b);
                            para[(int)modeSelectSlider.Value - 1, bereich, 1] = color.ToString();

                            para[(int)modeSelectSlider.Value - 1, bereich, 4] = win3.time.Value.ToString();
                        }
                        break;
                    case "BLINK_COLOR":
                        ColorTwoTimes win4 = new ColorTwoTimes();
                        win4.effectText.Text = effects[Convert.ToInt16(para[(int)modeSelectSlider.Value - 1, bereich, 0])];
                        win4.color1.R = Convert.ToByte(Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 1]) >> 16);
                        win4.color1.G = Convert.ToByte((UInt16)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 1]) >> 8);
                        win4.color1.B = Convert.ToByte((byte)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 1]));
                        win4.time1.Value = Convert.ToInt16(para[(int)modeSelectSlider.Value - 1, bereich, 2]);
                        win4.time2.Value = Convert.ToInt16(para[(int)modeSelectSlider.Value - 1, bereich, 3]);
                        win4.ShowDialog();
                        if (win4.DialogResult.HasValue && win4.DialogResult.Value)
                        {
                            UInt32 r = win4.color1.R;
                            UInt32 g = win4.color1.G;
                            UInt32 b = win4.color1.B;
                            UInt32 color = (((UInt32)r << 16) | ((UInt32)g << 8) | b);
                            para[(int)modeSelectSlider.Value - 1, bereich, 1] = color.ToString();

                            para[(int)modeSelectSlider.Value - 1, bereich, 2] = win4.time1.Value.ToString();
                            para[(int)modeSelectSlider.Value - 1, bereich, 3] = win4.time2.Value.ToString();
                        }
                        break;
                    case "FILL_AGAINST":
                        TwoColorPlusTime win5 = new TwoColorPlusTime();
                        win5.effectText.Text = effects[Convert.ToInt16(para[(int)modeSelectSlider.Value - 1, bereich, 0])];
                        win5.color1.R = Convert.ToByte(Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 1]) >> 16);
                        win5.color1.G = Convert.ToByte((UInt16)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 1]) >> 8);
                        win5.color1.B = Convert.ToByte((byte)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 1]));
                        win5.color2.R = Convert.ToByte(Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 2]) >> 16);
                        win5.color2.G = Convert.ToByte((UInt16)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 2]) >> 8);
                        win5.color2.B = Convert.ToByte((byte)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 2]));
                        win5.time.Value = Convert.ToInt16(para[(int)modeSelectSlider.Value - 1, bereich, 4]);
                        win5.ShowDialog();
                        if (win5.DialogResult.HasValue && win5.DialogResult.Value)
                        {
                            UInt32 r = win5.color1.R;
                            UInt32 g = win5.color1.G;
                            UInt32 b = win5.color1.B;
                            UInt32 color = (((UInt32)r << 16) | ((UInt32)g << 8) | b);
                            para[(int)modeSelectSlider.Value - 1, bereich, 1] = color.ToString();

                            r = win5.color2.R;
                            g = win5.color2.G;
                            b = win5.color2.B;
                            color = (((UInt32)r << 16) | ((UInt32)g << 8) | b);
                            para[(int)modeSelectSlider.Value - 1, bereich, 2] = color.ToString();

                            para[(int)modeSelectSlider.Value - 1, bereich, 4] = win5.time.Value.ToString();
                        }
                        break;
                    case "KNIGHTRIDER":
                        Knightrider win6 = new Knightrider();
                        win6.effectText.Text = effects[Convert.ToInt16(para[(int)modeSelectSlider.Value - 1, bereich, 0])];
                        win6.color1.R = Convert.ToByte(Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 1]) >> 16);
                        win6.color1.G = Convert.ToByte((UInt16)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 1]) >> 8);
                        win6.color1.B = Convert.ToByte((byte)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 1]));
                        win6.color2.R = Convert.ToByte(Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 2]) >> 16);
                        win6.color2.G = Convert.ToByte((UInt16)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 2]) >> 8);
                        win6.color2.B = Convert.ToByte((byte)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 2]));
                        win6.breite.Value = Convert.ToInt16(para[(int)modeSelectSlider.Value - 1, bereich, 3]);
                        win6.time.Value = Convert.ToInt16(para[(int)modeSelectSlider.Value - 1, bereich, 4]);
                        win6.ShowDialog();
                        if (win6.DialogResult.HasValue && win6.DialogResult.Value)
                        {
                            UInt32 r = win6.color1.R;
                            UInt32 g = win6.color1.G;
                            UInt32 b = win6.color1.B;
                            UInt32 color = (((UInt32)r << 16) | ((UInt32)g << 8) | b);
                            para[(int)modeSelectSlider.Value - 1, bereich, 1] = color.ToString();

                            r = win6.color2.R;
                            g = win6.color2.G;
                            b = win6.color2.B;
                            color = (((UInt32)r << 16) | ((UInt32)g << 8) | b);
                            para[(int)modeSelectSlider.Value - 1, bereich, 2] = color.ToString();

                            para[(int)modeSelectSlider.Value - 1, bereich, 3] = win6.breite.Value.ToString();

                            para[(int)modeSelectSlider.Value - 1, bereich, 4] = win6.time.Value.ToString();
                        }
                        break;
                    case "RAINBOW":
                    case "RANDOMFLASH":
                        TimeOnly win7 = new TimeOnly();
                        win7.effectText.Text = effects[Convert.ToInt16(para[(int)modeSelectSlider.Value - 1, bereich, 0])];
                        win7.time.Value = Convert.ToInt16(para[(int)modeSelectSlider.Value - 1, bereich, 4]);
                        win7.ShowDialog();
                        if (win7.DialogResult.HasValue && win7.DialogResult.Value)
                        {
                            para[(int)modeSelectSlider.Value - 1, bereich, 4] = win7.time.Value.ToString();
                        }
                        break;
                    case "BLOCKSWITCH":
                        Blockswitch win8 = new Blockswitch();
                        win8.effectText.Text = effects[Convert.ToInt16(para[(int)modeSelectSlider.Value - 1, bereich, 0])];
                        win8.color1.R = Convert.ToByte(Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 1]) >> 16);
                        win8.color1.G = Convert.ToByte((UInt16)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 1]) >> 8);
                        win8.color1.B = Convert.ToByte((byte)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 1]));
                        win8.color2.R = Convert.ToByte(Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 2]) >> 16);
                        win8.color2.G = Convert.ToByte((UInt16)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 2]) >> 8);
                        win8.color2.B = Convert.ToByte((byte)Convert.ToInt32(para[(int)modeSelectSlider.Value - 1, bereich, 2]));
                        win8.breite.Value = Convert.ToInt16(para[(int)modeSelectSlider.Value - 1, bereich, 3]);
                        win8.time.Value = Convert.ToInt16(para[(int)modeSelectSlider.Value - 1, bereich, 4]);
                        win8.ShowDialog();
                        if (win8.DialogResult.HasValue && win8.DialogResult.Value)
                        {
                            UInt32 r = win8.color1.R;
                            UInt32 g = win8.color1.G;
                            UInt32 b = win8.color1.B;
                            UInt32 color = (((UInt32)r << 16) | ((UInt32)g << 8) | b);
                            para[(int)modeSelectSlider.Value - 1, bereich, 1] = color.ToString();

                            r = win8.color2.R;
                            g = win8.color2.G;
                            b = win8.color2.B;
                            color = (((UInt32)r << 16) | ((UInt32)g << 8) | b);
                            para[(int)modeSelectSlider.Value - 1, bereich, 2] = color.ToString();

                            para[(int)modeSelectSlider.Value - 1, bereich, 3] = win8.breite.Value.ToString();

                            para[(int)modeSelectSlider.Value - 1, bereich, 4] = win8.time.Value.ToString();
                        }
                        break;
                    case "POLICE_RIGHT":
                    case "POLICE_LEFT":
                        System.Windows.MessageBox.Show("Hier gibt es leider nichts einzustellen :(", "Nö");
                        break;
                    default:
                        System.Windows.MessageBox.Show("Ungültiger Modus", "Fehler");
                        break;
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Bitte wähle erst einen Effekt!", "Fehler");
            }
        }

        private void edit1_Click(object sender, RoutedEventArgs e)
        {
            openPara(0);
        }

        private void edit2_Click(object sender, RoutedEventArgs e)
        {
            openPara(1);
        }

        private void edit3_Click(object sender, RoutedEventArgs e)
        {
            openPara(2);
        }

        private void edit4_Click(object sender, RoutedEventArgs e)
        {
            openPara(3);
        }

        private void edit5_Click(object sender, RoutedEventArgs e)
        {
            openPara(4);
        }

        private void edit6_Click(object sender, RoutedEventArgs e)
        {
            openPara(5);
        }

        private void edit7_Click(object sender, RoutedEventArgs e)
        {
            openPara(6);
        }

        private void edit8_Click(object sender, RoutedEventArgs e)
        {
            openPara(7);
        }

        private void edit9_Click(object sender, RoutedEventArgs e)
        {
            openPara(8);
        }

        private void edit10_Click(object sender, RoutedEventArgs e)
        {
            openPara(9);
        }

        private void edit11_Click(object sender, RoutedEventArgs e)
        {
            openPara(10);
        }

        private void edit12_Click(object sender, RoutedEventArgs e)
        {
            openPara(11);
        }

        private void edit13_Click(object sender, RoutedEventArgs e)
        {
            openPara(12);
        }

        private void edit14_Click(object sender, RoutedEventArgs e)
        {
            openPara(13);
        }

        private void edit15_Click(object sender, RoutedEventArgs e)
        {
            openPara(14);
        }

        private void edit16_Click(object sender, RoutedEventArgs e)
        {
            openPara(15);
        }

        private void edit17_Click(object sender, RoutedEventArgs e)
        {
            openPara(16);
        }

        private void edit18_Click(object sender, RoutedEventArgs e)
        {
            openPara(17);
        }

        private void edit19_Click(object sender, RoutedEventArgs e)
        {
            openPara(18);
        }

        private void edit20_Click(object sender, RoutedEventArgs e)
        {
            openPara(19);
        }

        private void setStandardPara(int bereich)
        {
            for (int parameter = 1; parameter < 5; parameter++)
            {
                para[(int)modeSelectSlider.Value - 1, bereich, parameter] = Convert.ToString(standardPara[Convert.ToInt16(para[(int)modeSelectSlider.Value - 1, bereich, 0]), parameter - 1]);
            }
        }

        private void strip2act_Checked(object sender, RoutedEventArgs e)
        {
            act11.IsEnabled = true;
            strip2label.IsEnabled = true;
            pin2.IsEnabled = true;
            pin2StripLabel.IsEnabled = true;
        }

        private void act11_Checked(object sender, RoutedEventArgs e)
        {
            rev11.IsEnabled = true;
            name11.IsEnabled = true;
            start11.IsEnabled = true;
            end11.IsEnabled = true;
            effect11.IsEnabled = true;
            edit11.IsEnabled = true;
            act12.IsEnabled = true;
        }

        private void act12_Checked(object sender, RoutedEventArgs e)
        {
            rev12.IsEnabled = true;
            name12.IsEnabled = true;
            start12.IsEnabled = true;
            end12.IsEnabled = true;
            effect12.IsEnabled = true;
            edit12.IsEnabled = true;
            act13.IsEnabled = true;
        }

        private void act13_Checked(object sender, RoutedEventArgs e)
        {
            rev13.IsEnabled = true;
            name13.IsEnabled = true;
            start13.IsEnabled = true;
            end13.IsEnabled = true;
            effect13.IsEnabled = true;
            edit13.IsEnabled = true;
            act14.IsEnabled = true;
        }

        private void act14_Checked(object sender, RoutedEventArgs e)
        {
            rev14.IsEnabled = true;
            name14.IsEnabled = true;
            start14.IsEnabled = true;
            end14.IsEnabled = true;
            effect14.IsEnabled = true;
            edit14.IsEnabled = true;
            act15.IsEnabled = true;
        }

        private void act15_Checked(object sender, RoutedEventArgs e)
        {
            rev15.IsEnabled = true;
            name15.IsEnabled = true;
            start15.IsEnabled = true;
            end15.IsEnabled = true;
            effect15.IsEnabled = true;
            edit15.IsEnabled = true;
            act16.IsEnabled = true;
        }

        private void act16_Checked(object sender, RoutedEventArgs e)
        {
            rev16.IsEnabled = true;
            name16.IsEnabled = true;
            start16.IsEnabled = true;
            end16.IsEnabled = true;
            effect16.IsEnabled = true;
            edit16.IsEnabled = true;
            act17.IsEnabled = true;
        }

        private void act17_Checked(object sender, RoutedEventArgs e)
        {
            rev17.IsEnabled = true;
            name17.IsEnabled = true;
            start17.IsEnabled = true;
            end17.IsEnabled = true;
            effect17.IsEnabled = true;
            edit17.IsEnabled = true;
            act18.IsEnabled = true;
        }

        private void act18_Checked(object sender, RoutedEventArgs e)
        {
            rev18.IsEnabled = true;
            name18.IsEnabled = true;
            start18.IsEnabled = true;
            end18.IsEnabled = true;
            effect18.IsEnabled = true;
            edit18.IsEnabled = true;
            act19.IsEnabled = true;
        }

        private void act19_Checked(object sender, RoutedEventArgs e)
        {
            rev19.IsEnabled = true;
            name19.IsEnabled = true;
            start19.IsEnabled = true;
            end19.IsEnabled = true;
            effect19.IsEnabled = true;
            edit19.IsEnabled = true;
            act20.IsEnabled = true;
        }

        private void act20_Checked(object sender, RoutedEventArgs e)
        {
            rev20.IsEnabled = true;
            name20.IsEnabled = true;
            start20.IsEnabled = true;
            end20.IsEnabled = true;
            effect20.IsEnabled = true;
            edit20.IsEnabled = true;
        }

        private void act2_Checked(object sender, RoutedEventArgs e)
        {
            rev2.IsEnabled = true;
            name2.IsEnabled = true;
            start2.IsEnabled = true;
            end2.IsEnabled = true;
            effect2.IsEnabled = true;
            edit2.IsEnabled = true;
            act3.IsEnabled = true;
        }

        private void act3_Checked(object sender, RoutedEventArgs e)
        {
            rev3.IsEnabled = true;
            name3.IsEnabled = true;
            start3.IsEnabled = true;
            end3.IsEnabled = true;
            effect3.IsEnabled = true;
            edit3.IsEnabled = true;
            act4.IsEnabled = true;
        }

        private void act4_Checked(object sender, RoutedEventArgs e)
        {
            rev4.IsEnabled = true;
            name4.IsEnabled = true;
            start4.IsEnabled = true;
            end4.IsEnabled = true;
            effect4.IsEnabled = true;
            edit4.IsEnabled = true;
            act5.IsEnabled = true;
        }

        private void act5_Checked(object sender, RoutedEventArgs e)
        {
            rev5.IsEnabled = true;
            name5.IsEnabled = true;
            start5.IsEnabled = true;
            end5.IsEnabled = true;
            effect5.IsEnabled = true;
            edit5.IsEnabled = true;
            act6.IsEnabled = true;
        }

        private void act6_Checked(object sender, RoutedEventArgs e)
        {
            rev6.IsEnabled = true;
            name6.IsEnabled = true;
            start6.IsEnabled = true;
            end6.IsEnabled = true;
            effect6.IsEnabled = true;
            edit6.IsEnabled = true;
            act7.IsEnabled = true;
        }

        private void act7_Checked(object sender, RoutedEventArgs e)
        {
            rev7.IsEnabled = true;
            name7.IsEnabled = true;
            start7.IsEnabled = true;
            end7.IsEnabled = true;
            effect7.IsEnabled = true;
            edit7.IsEnabled = true;
            act8.IsEnabled = true;
        }

        private void act8_Checked(object sender, RoutedEventArgs e)
        {
            rev8.IsEnabled = true;
            name8.IsEnabled = true;
            start8.IsEnabled = true;
            end8.IsEnabled = true;
            effect8.IsEnabled = true;
            edit8.IsEnabled = true;
            act9.IsEnabled = true;
        }

        private void act9_Checked(object sender, RoutedEventArgs e)
        {
            rev9.IsEnabled = true;
            name9.IsEnabled = true;
            start9.IsEnabled = true;
            end9.IsEnabled = true;
            effect9.IsEnabled = true;
            edit9.IsEnabled = true;
            act10.IsEnabled = true;
        }

        private void act10_Checked(object sender, RoutedEventArgs e)
        {
            rev10.IsEnabled = true;
            name10.IsEnabled = true;
            start10.IsEnabled = true;
            end10.IsEnabled = true;
            effect10.IsEnabled = true;
            edit10.IsEnabled = true;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string tabItem = ((sender as TabControl).SelectedItem as TabItem).Header as string;

                switch (tabItem)
                {
                    case "Effekte":
                        nameBereich1.Content = name1.Text;
                        nameBereich2.Content = name2.Text;
                        nameBereich3.Content = name3.Text;
                        nameBereich4.Content = name4.Text;
                        nameBereich5.Content = name5.Text;
                        nameBereich6.Content = name6.Text;
                        nameBereich7.Content = name7.Text;
                        nameBereich8.Content = name8.Text;
                        nameBereich9.Content = name9.Text;
                        nameBereich10.Content = name10.Text;
                        nameBereich11.Content = name11.Text;
                        nameBereich12.Content = name12.Text;
                        nameBereich13.Content = name13.Text;
                        nameBereich14.Content = name14.Text;
                        nameBereich15.Content = name15.Text;
                        nameBereich16.Content = name16.Text;
                        nameBereich17.Content = name17.Text;
                        nameBereich18.Content = name18.Text;
                        nameBereich19.Content = name19.Text;
                        nameBereich20.Content = name20.Text;
                        break;

                    default:
                        return;
                }
            }
            catch { }
            
        }
        


    }
}
