﻿using System;
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

using TriTech.Common.Interface;
using TriTech.VisiCAD.DataService;

namespace HelloWorld
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<String> ComboBox_List;

        public MainWindow()
        {
            // 800 x 600 (653 x 486)
            // 800 x 240 (653 x 198)
            InitializeComponent();
        }

        public void AppendText(String Text, String Color)
        {
            BrushConverter BC = new BrushConverter();
            TextRange TR = new TextRange(RichTextBox.Document.ContentEnd, RichTextBox.Document.ContentEnd);
            TR.Text = Text + "\r";
            try
            {
                TR.ApplyPropertyValue(TextElement.ForegroundProperty, BC.ConvertFromString(Color));
            }
            catch (FormatException) { }
            RichTextBox.ScrollToEnd();
        }

        private void Write(String Text)
        {
            AppendText(Text,"Black");
        }

        private void Red(String Text)
        {
            AppendText(Text, "Red");
        }

        private void Green(String Text)
        {
            AppendText(Text, "Green");
        }

        private void Blue(String Text)
        {
            AppendText(Text, "Blue");
        }

        private void Hello()
        {
            Write("Hello " + Environment.UserName.ToUpper() + ".");
        }

        private void DEBUG(String Text)
        {
            Console.WriteLine("\nDEBUG_________________________________________________\n"+ Text + "\n");
        }

        private void ComboBox_Loaded(object Sender, RoutedEventArgs E)
        {
            ComboBox_List = new List<String>();

            var ComboBox = Sender as ComboBox;
            ComboBox.ItemsSource = ComboBox_List;
            ComboBox.SelectedIndex = -1;

            ComboBox.Focus();

            Hello();

            try
            {
                Wrapper.Initialize();
                Green("Wrapper class intialized.");
            }
            catch (Exception X)
            {
                Red("Wrapper class could not be intialized.");
                DEBUG(X.ToString());
            }
        }

        private void ComboBox_KeyDown(object Sender, KeyEventArgs E)
        {
            String Command;
            Boolean Command_Valid = false;

            var ComboBox = Sender as ComboBox;
            TextBox ComboBox_TextBox = ComboBox.Template.FindName("PART_EditableTextBox", ComboBox) as TextBox;

            switch (E.Key)
            {
                case Key.Enter:

                    Command = ComboBox_TextBox.Text.ToUpper();

                    ComboBox_TextBox.Text = "";
                    ComboBox.SelectedIndex = -1;

                    switch (Command)
                    {
                        case "HELP":
                            try
                            {
                                Wrapper.ExecuteCommand("clsAction|ActivateHelp||");
                                Command_Valid = true;
                            }
                            catch (Exception X)
                            {
                                Red("Unable to launch HELP module.");
                                DEBUG(X.ToString());
                            }
                            break;

                        case "QUIT":
                            try
                            {
                                Application.Current.Shutdown();
                            }
                            catch (Exception X)
                            {
                                DEBUG(X.ToString());
                            }
                            break;

                        case "RED":
                            Red("RED.");
                            Command_Valid = true;
                            break;

                        case "GREEN":
                            Green("GREEN.");
                            Command_Valid = true;
                            break;

                        case "BLUE":
                            Blue("BLUE.");
                            Command_Valid = true;
                            break;

                        case "HELLO":
                            Hello();
                            Command_Valid = true;
                            break;

                        default:
                            Red("Invalid command (" + Command + ").");
                            break;
                    }

                    if (Command_Valid)
                    {
                        if (!ComboBox.Items.Contains(Command))
                        {
                            ComboBox_List.Add(Command);
                            ComboBox_List.Sort();
                            ComboBox.Items.Refresh();
                        }
                    }
                    break;

                default:
                    break;;
            }
        }
    }
}
