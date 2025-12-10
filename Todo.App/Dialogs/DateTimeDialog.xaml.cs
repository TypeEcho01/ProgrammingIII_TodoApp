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

namespace Todo.App.Dialogs
{
    /// <summary>
    /// Interaction logic for DateTimeDialog.xaml
    /// </summary>
    public partial class DateTimeDialog : Window
    {
        public DateTime DateTime { get; private set; }

        public DateTimeDialog(string title, DateTime defaultDateTime)
        {
            InitializeComponent();

            this.Title = title;

            this.Date_Input.SelectedDate = defaultDateTime.Date;
            this.Date_Input.DisplayDateStart = defaultDateTime.Date;
            this.Date_Input.DisplayDateEnd = defaultDateTime.Date.AddDays((365 * 10));

            this.Hour_Input.Text = defaultDateTime.ToString("hh");
            this.Minute_Input.Text = defaultDateTime.ToString("mm");

            if ((defaultDateTime.Hour == 0) || (defaultDateTime.Hour < 12))
            {
                this.Meridiem_Button.Content = "AM";
            }
            else
            {
                this.Meridiem_Button.Content = "PM";
            }
        }

        private void Meridiem_Button_Click(object sender, RoutedEventArgs e)
        {
            if ((string)this.Meridiem_Button.Content == "PM")
            {
                this.Meridiem_Button.Content = "AM";
                return;
            }

            this.Meridiem_Button.Content = "PM";
        }

        private void Enter_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Date_Input.SelectedDate == null)
            {
                App.Error("Please select a date.");
                return;
            }

            if 
            (
                (!int.TryParse(this.Hour_Input.Text, out var hour)) || 
                (hour < 1) || 
                (hour > 12)
            )
            {
                App.Error("Please enter a valid hour between 1 and 12.");
                return;
            }

            if
            (
                (!int.TryParse(this.Minute_Input.Text, out var minute)) ||
                (minute < 0) ||
                (minute > 59)
            )
            {
                App.Error("Please enter a valid minute between 0 and 59.");
                return;
            }

            string meridiem = (this.Meridiem_Button.Content?.ToString()) ?? ("AM");

            if (meridiem == "AM")
            {
                if (hour == 12)
                    hour = 0; // 12AM => 0
            }
            else if (hour != 12)
            {
                hour += 12; 
            }

            var time = new TimeSpan(hour, minute, 59);

            this.DateTime = this.Date_Input.SelectedDate.Value + time;

            this.DialogResult = true; // closes the window
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; // closes the window
        }

        private void None_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DateTime = DateTime.MinValue;
            this.DialogResult = true; // closes the window
        }
    }
}
