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
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        List<LoginHistory> loginHistories = new List<LoginHistory>();
        public UserWindow()
        {
            InitializeComponent();
            loginHistories = Context._con.LoginHistory.Where(p => p.UserId == LoginWindow.userData.UserID).ToList();
            HistoryDG.ItemsSource = loginHistories;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            LoginWindow.userLogin.LogoutDateTime = DateTime.Now;
            Context._con.SaveChanges();
            Properties.Settings.Default.Login = "";
            Properties.Settings.Default.Password = "";
            Properties.Settings.Default.Save();
            Application.Current.Shutdown();
            LoginWindow.userData = null;
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
