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

        private void Write()
        {
            RichTextBox.AppendText("\r");
            RichTextBox.ScrollToEnd();
        }

        private void Write(String Text)
        {
            RichTextBox.AppendText(Text + "\r");
            RichTextBox.ScrollToEnd();
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

            Write("Hello " + Environment.UserName.ToUpper() + ".");

            ComboBox.Focus();

            try
            {
                Wrapper.Initialize();
                Write("Initialized wrapper class.");
            }
            catch (Exception X)
            {
                Write("Unable to initialize wrapper class.");
                DEBUG(X.ToString());
            }
        }

        private void ComboBox_KeyDown(object Sender, KeyEventArgs E)
        {
            String Command;

            var ComboBox = Sender as ComboBox;
            TextBox ComboBox_TextBox = ComboBox.Template.FindName("PART_EditableTextBox", ComboBox) as TextBox;

            switch (E.Key)
            {
                case Key.Enter:

                    Command = ComboBox_TextBox.Text.ToUpper();

                    ComboBox_List.Add(Command);
                    ComboBox.Items.Refresh();
                    ComboBox_TextBox.Text = "";
                    ComboBox.SelectedIndex = -1;

                    switch (Command)
                    {
                        case "HELP":
                            try
                            {
                                Wrapper.ExecuteCommand("clsAction|ActivateHelp||");
                                Write("Executed command --> " + Command + ".");
                            }
                            catch (Exception X)
                            {
                                Write("Unable to execute command --> " + Command + ".");
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
                                Write("Unable to execute command --> " + Command + ".");
                                Console.WriteLine(X.ToString());
                            }
                            break;

                        default:
                            Write("Invalid command --> " + Command + ".");
                            break;
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
