using Client.Models;
using Client.Operations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.Pages
{
    /// <summary>
    /// Логика взаимодействия для TempPage.xaml
    /// </summary>
    public partial class TempPage : Page
    {
        UsersOperations ops = new UsersOperations();
        Temp temp = new Temp();
        public TempPage()
        {
            InitializeComponent();
        }
        private void RegisterUser()
        {
            User user = ops.RegisterUser(Global.LoggedInUser.Mail, Global.LoggedInUser.Password);
            if (user == null)
            {
                MessageBox.Show("Username already exists");
                return;
            }
            Global.LoggedInUser = null;
            //Global.LoggedInUser = user;

            MessageBox.Show("Register successful!");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            temp = ops.ChechPass(Global.LoggedInUser.Mail);
            MessageBox.Show("Message income!");
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (pbxPassword.Password == temp.Password)
            {
                ops.RemoveTemp(Global.LoggedInUser.Mail);
                RegisterUser();
                NavigationService.Navigate(new LoginPage());
            }
            else
            {
                MessageBox.Show("Password incorrect!");
                ops.RemoveTemp(Global.LoggedInUser.Mail);
                Global.LoggedInUser = null;
                NavigationService.Navigate(new LoginPage());
            }
        }
    }
}
