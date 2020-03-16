using Client.Models;
using Client.Other;
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
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string mail = tbxMail.Text;
            string password = pbxPassword.Password;

            if (Valid.IsValidEmail(mail)) { }
            else
            {
                MessageBox.Show($"Invalid: {mail}");
                return;
            }
            if (password.Length < 8)
            {
                MessageBox.Show($"Invalid: {password} \nYour password must have at least 8 characters ");
                return;
            }
            User user = new User() { Mail = mail, Password = password };
            Global.LoggedInUser = user;

            NavigationService.Navigate(new TempPage());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
