using AviaApp.Classes;
using AviaApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AviaApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public static Users userData { get; set; }
        public static LoginHistory userLogin { get; set; }
        private int counter = 1;
        private DispatcherTimer dispatcherTimer;
        private TimeSpan timerCounter = new TimeSpan(0, 0, 10);

        public LoginWindow()
        {
            InitializeComponent();
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
        }

        

        private void Exit(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Login = "";
            Properties.Settings.Default.Password = "";
            Properties.Settings.Default.Save();
            Application.Current.Shutdown();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            userData = Context._con.Users.ToList().Where(p => p.Email == LoginTB.Text && p.Password == Hash.HashPassword(PasswordTB.Text, "ебучие тайминги")).FirstOrDefault();
            if(userData != null)
            {
                if (userData.Active is true)
                {
                    if (userData.RoleID == 1)
                    {
                        if(LoginTB.Text == Properties.Settings.Default.Login)
                        {
                            LoginHistory login = Context._con.LoginHistory.Where(p => p.UserId == userData.UserID).Last();
                            UnsucceessfulLogoutReasonWindow unsucceessfulLogoutReasonWindow = new UnsucceessfulLogoutReasonWindow(login);
                            unsucceessfulLogoutReasonWindow.Show();
                        }
                        else
                        {
                            AdminWindow adminWindow = new AdminWindow();
                            adminWindow.Show();
                        }
                    }
                    else if (userData.RoleID == 2)
                    {
                        if (LoginTB.Text == Properties.Settings.Default.Login)
                        {
                            LoginHistory login = Context._con.LoginHistory.ToList().Where(p => p.UserId == userData.UserID).LastOrDefault();
                            UnsucceessfulLogoutReasonWindow unsucceessfulLogoutReasonWindow = new UnsucceessfulLogoutReasonWindow(login);
                            unsucceessfulLogoutReasonWindow.Show();
                        }
                        else
                        {
                            UserWindow userWindow = new UserWindow();
                            userWindow.Show();
                        }
                    }

                    userLogin = new LoginHistory();
                    userLogin.UserId = userData.UserID;
                    userLogin.LoginDateTime = DateTime.Now;
                    Context._con.LoginHistory.Add(userLogin);
                    Context._con.SaveChanges();
                    Properties.Settings.Default.Login = LoginTB.Text;
                    Properties.Settings.Default.Password = PasswordTB.Text;
                    Properties.Settings.Default.Save();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Вы были заблокированы!");
                }
                
            }
            else
            {
                new Thread(() => { MessageBox.Show("Неудачная попытка входа!"); }).Start();
                if(counter == 3)
                {
                    new Thread(() => { MessageBox.Show("Вы были заблокированы на 10 секунд"); }).Start();
                    dispatcherTimer.Start();
                    LoginBtn.IsEnabled = false;
                    counter = 1;
                }
                counter++;
            }
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            TimeTB.Text = timerCounter.ToString();
            timerCounter -= TimeSpan.FromSeconds(1);
            if(timerCounter <= TimeSpan.FromSeconds(1))
            {
                MessageBox.Show("Теперь вы можете войти!");
                LoginBtn.IsEnabled = true;
                TimeTB.Text = "";
                dispatcherTimer.Stop();
            }
        }
    }
}
