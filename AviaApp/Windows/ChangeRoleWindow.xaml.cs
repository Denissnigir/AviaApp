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
    /// Логика взаимодействия для ChangeRoleWindow.xaml
    /// </summary>
    public partial class ChangeRoleWindow : Window
    {
        private Users curUser { get; set; }
        public ChangeRoleWindow(Users users)
        {
            InitializeComponent();
            curUser = users;
            UserDataGrid.DataContext = curUser;
            if(curUser.RoleID == 1)
            {
                AdminRB.IsChecked = true;
            }
            else
            {
                UserRB.IsChecked = true;
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Close();
        }

        private void ChangeRole(object sender, RoutedEventArgs e)
        {
            if(AdminRB.IsChecked == true)
            {
                curUser.RoleID = 1;
            } 
            else if(UserRB.IsChecked == true)
            {
                curUser.RoleID = 2;
            }
            Context._con.SaveChanges();
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Close();
        }
    }
}
