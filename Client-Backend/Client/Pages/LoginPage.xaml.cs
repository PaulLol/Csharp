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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string mail = tbxMail.Text;
            string password = pbxPassword.Password;

            UsersOperations ops = new UsersOperations();
            User user = ops.AuthenticateUser(mail, password);

            //user.PersonalMusic = ops.GetListPersonalMusic(user.Id);
            //user.PersonalMusicId = ops.GetListPersonalMusicId(user.Id);

            if (user == null)
            {
                MessageBox.Show("Invalid username or password");
                return;
            }
            Global.LoggedInUser = user;

            Global.LoggedInUser.PersonalMusic = ops.GetListPersonalMusic(Global.LoggedInUser.Id);
            Global.LoggedInUser.PersonalMusicId = ops.GetListPersonalMusicId(Global.LoggedInUser.Id);
            Global.LoggedInUser.PersonalMusicFileType = ops.GetListPersonalMusicFileType(Global.LoggedInUser.Id);

            NavigationService.Navigate(new DetailPage());
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }
    }
}
