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

namespace Todo.App
{
    /// <summary>
    /// Interaction logic for InputDialog.xaml
    /// </summary>
    public partial class InputDialog : Window
    {
        public string Input { get; private set; }

        public InputDialog(string title, string prompt, string defaultText)
        {
            InitializeComponent();

            this.Title = title;
            this.Prompt_Label.Content = prompt;
            this.Input_TextBox.Text = defaultText;
        }

        private void Enter_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Input = this.Input_TextBox.Text;
            this.DialogResult = true; // closes the window
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; // closes the window
        }
    }
}
