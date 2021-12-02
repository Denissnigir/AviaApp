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
using AviaApp.Model;

namespace AviaApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private List<Users> users = new List<Users>();
        private List<Offices> offices = new List<Offices>();
        public AdminWindow()
        {
            InitializeComponent();
            offices = Context._con.Offices.ToList();
            offices.Insert(0, new Offices { Title = "Выберите офис" });
            OfficeCB.ItemsSource = offices;
            Filter();
        }

        private void Filter()
        {
            users = Context._con.Users.ToList();
            if (OfficeCB.SelectedItem is Offices offices)
            {
                users = users.Where(p => p.OfficeID == offices.ID).ToList();
            }
            UserDG.ItemsSource = users;

        }

        private void OfficeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void ChangeRole(object sender, RoutedEventArgs e)
        {
            ChangeRoleWindow changeRoleWindow = new ChangeRoleWindow(UserDG.SelectedItem as Users);
            changeRoleWindow.Show();
            this.Close();
        }

        private void EnableDisableLogin(object sender, RoutedEventArgs e)
        {
            if (UserDG.SelectedItem is Users user)
            {
                if (user.Active == true)
                {
                    user.Active = false;
                }
                else
                {
                    user.Active = true;
                }
                Context._con.SaveChanges();
                Filter();
            }
        }

        private void ExitClick(object sender, RoutedEventArgs e)
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

        private void AddUser(object sender, RoutedEventArgs e)
        {

        }
    }
}
