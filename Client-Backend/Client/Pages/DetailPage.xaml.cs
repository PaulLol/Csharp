using Client.Models;
using Client.Operations;
using Microsoft.Win32;
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
    /// Логика взаимодействия для DetailPage.xaml
    /// </summary>
    public partial class DetailPage : Page
    {
        public DetailPage()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LogOut();
        }
        private void LogOut()
        {
            Global.LoggedInUser = null;
            btnLoadFileAdmin.Visibility = Visibility.Hidden;
            btnLoadFileUser.Visibility = Visibility.Hidden;
            NavigationService.Navigate(new LoginPage());
        }
        private void btnLoadFileAdmin_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            MusicOperations mo = new MusicOperations();
            MessageBox.Show("File should be in example format: 'Slayer - Repentless.mp3'\nPress 'Reset' after load music");
            if (openFileDialog.ShowDialog() == true)
            {
                mo.LoadFileAdmin(openFileDialog.FileName);
                MessageBox.Show("Music loaded!");
            }
        }
        private void btnLoadFileUser_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            MusicOperations mo = new MusicOperations();
            UsersOperations uo = new UsersOperations();
            MessageBox.Show("File should be in example format: 'Slayer - Repentless.mp3'\nPress 'Load personal music list' after load music");

            if (openFileDialog.ShowDialog() == true)
            {
                List<string> fileData = mo.LoadFileUser(openFileDialog.FileName, Global.LoggedInUser.Id);
                string idFile = fileData[0];
                string fileType = fileData[1];

                uo.LoadPrivateMusic(openFileDialog.SafeFileName, idFile, fileType);
                MessageBox.Show("Music loaded!");
            }

            //LogOut();
        }
        private void ShowUserInfo()
        {
            txbMail.Text = Global.LoggedInUser.Mail;
        }

        private void FetchUserDetails()
        {
            UsersOperations ops = new UsersOperations();
            User user = ops.GetUserDetails(Global.LoggedInUser);
            if (user == null)
            {
                MessageBox.Show("Session expired");
                NavigationService.Navigate(new LoginPage());
            }

            Global.LoggedInUser = user;
        }


        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            UsersOperations ops = new UsersOperations();
            string pass = pbxPassword.Password;
            ops.ChangePass(pass);
            MessageBox.Show("Password change!");
            LogOut();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FetchUserDetails();
            ShowUserInfo();


            if (Global.LoggedInUser.Mail == "paullol@ukr.net")
            {
                tbxRole.Text = "Admin:";
                btnLoadFileAdmin.Visibility = Visibility.Visible;
            }
            else
            {
                tbxRole.Text = "User:";
                btnLoadFileUser.Visibility = Visibility.Visible;
            }
        }
    }
}
