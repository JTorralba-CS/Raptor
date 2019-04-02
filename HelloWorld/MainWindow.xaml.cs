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
            TextBox.AppendText("\n");
            TextBox.ScrollToEnd();
        }

        private void Write(String Text)
        {
            TextBox.AppendText(Text + "\n");
            TextBox.ScrollToEnd();
        }

        private void ComboBox_Loaded(object Sender, RoutedEventArgs E)
        {
            ComboBox_List = new List<String>();

            var ComboBox = Sender as ComboBox;
            ComboBox.ItemsSource = ComboBox_List;
            ComboBox.SelectedIndex = -1;

            Write("Hello user " + Environment.UserName.ToUpper() + ".");
            Write();

            ComboBox.Focus();
        }

        private void ComboBox_KeyDown(object Sender, KeyEventArgs E)
        {
            var ComboBox = Sender as ComboBox;
            TextBox ComboBox_TextBox = ComboBox.Template.FindName("PART_EditableTextBox", ComboBox) as TextBox;

            switch (E.Key)
            {
                case Key.Enter:
                    Write(ComboBox_TextBox.Text);
                    ComboBox_List.Add(ComboBox_TextBox.Text);
                    ComboBox.Items.Refresh();
                    ComboBox_TextBox.Text = "";
                    ComboBox.SelectedIndex = -1;
                    break;
                default:
                    break;
            }
        }
    }
}
