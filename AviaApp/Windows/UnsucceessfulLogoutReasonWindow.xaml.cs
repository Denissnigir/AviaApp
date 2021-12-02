using AviaApp.Model;
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

namespace AviaApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для UnsucceessfulLogoutReasonWindow.xaml
    /// </summary>
    public partial class UnsucceessfulLogoutReasonWindow : Window
    {
        private LoginHistory login { get; set; }
        public UnsucceessfulLogoutReasonWindow(LoginHistory loginHistory)
        {
            InitializeComponent();
            login = loginHistory;
            ReaseonTB.Text = $"No logout detected for your last login on {login.LoginDateTime.Day}.{login.LoginDateTime.Month}.{login.LoginDateTime.Year} at {login.LoginDateTime.Hour}:{login.LoginDateTime.Minute}";
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            login.UnsuccessgulLogoutReason = ReasonTB.Text;
            Context._con.SaveChanges();
            if (LoginWindow.userData.RoleID == 1)
            {

                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();
                Properties.Settings.Default.Login = "";
                Properties.Settings.Default.Password = "";
            }
            else if (LoginWindow.userData.RoleID == 2)
            {

                UserWindow userWindow = new UserWindow();
                userWindow.Show();
                Properties.Settings.Default.Login = "";
                Properties.Settings.Default.Password = "";
            }
        }
    }
}
