using Client.Models;
using Client.Operations;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void timerTick();
        DispatcherTimer ticks = new DispatcherTimer();
        timerTick tick;

        public string startupPath = Directory.GetCurrentDirectory() + "/Music/";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HiddenElements();
            LoadGroupBox();
        }
        private void HiddenElements()
        {
            timelineSlider.Visibility = Visibility.Hidden;
            volSlider.Visibility = Visibility.Hidden;
            btnPause.Visibility = Visibility.Hidden;
            btnPlay.Visibility = Visibility.Hidden;
            btnStop.Visibility = Visibility.Hidden;
            tbxNameSong.Visibility = Visibility.Hidden;
            tbxVol.Visibility = Visibility.Hidden;
            tbxTimer.Visibility = Visibility.Hidden;
            lbPrivate.Visibility = Visibility.Hidden;
            tbxPersonalList.Visibility = Visibility.Hidden;
        }
        private void VisibleElements()
        {
            //txbResult.Visibility = Visibility.Visible;
            timelineSlider.Visibility = Visibility.Visible;
            volSlider.Visibility = Visibility.Visible;
            btnPause.Visibility = Visibility.Visible;
            btnPlay.Visibility = Visibility.Visible;
            btnStop.Visibility = Visibility.Visible;
            tbxNameSong.Visibility = Visibility.Visible;
            tbxVol.Visibility = Visibility.Visible;
            tbxTimer.Visibility = Visibility.Visible;
        }
        public void LoadGroupBox()
        {
            MusicOperations mo = new MusicOperations();
            var songs = mo.DownloadGroups();
            songs = songs.Distinct().ToList();

            foreach (string song in songs)
            {
                lbGroup.Items.Add(song);
            }
        }

        private void lbGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectItem;
            if (lbGroup.SelectedItem != null)
            {
                selectItem = lbGroup.SelectedItem.ToString();
                MusicOperations mo = new MusicOperations();
                var songs = mo.DownloadSongs(selectItem);
                songs = songs.Distinct().ToList();

                lbSong.Items.Clear();

                foreach (string song in songs)
                {
                    lbSong.Items.Add(song);
                }
            }
        }
        private void btnPlaySong_Click(object sender, RoutedEventArgs e)
        {
            string songpath;
            if (lbPrivate.SelectedItem != null)
            {
                timelineSlider.Value = 0;
                VisibleElements();

                for (int i = 0; i < lbPrivate.Items.Count; i++)
                {
                    if (Global.LoggedInUser.PersonalMusic[i] == lbPrivate.SelectedItem.ToString())
                    {
                        mainMediaElement.Source = null;

                        MusicOperations mo = new MusicOperations();
                        string selectMusic = lbPrivate.SelectedItem.ToString();
                        //string filetype = selectMusic.Remove(0, selectMusic.Length -3)
                        byte[] filesong = mo.DownloadPrivateFile(Global.LoggedInUser.PersonalMusicId[i], Global.LoggedInUser.PersonalMusicFileType[i]);
                        songpath = startupPath + selectMusic;
                        File.WriteAllBytes(songpath, filesong);


                        tbxNameSong.Text = lbPrivate.SelectedItem.ToString();
                        mainMediaElement.Source = new Uri(songpath);
                        mainMediaElement.Play();
                    }
                }
            }
            else if (lbSong.SelectedItem != null && lbGroup.SelectedItem != null)
            {
                mainMediaElement.Source = null;

                timelineSlider.Value = 0;
                VisibleElements();

                MusicOperations mo = new MusicOperations();
                string namegroup = lbGroup.SelectedItem.ToString();
                string namesong = lbSong.SelectedItem.ToString();
                string fullnamesong = namegroup + " - " + namesong;
                string idFile = mo.GetIdFile(namegroup, namesong);
                byte[] filesong = mo.DownloadFile(idFile);
                songpath = startupPath + fullnamesong + ".mp3";
                File.WriteAllBytes(songpath, filesong);

                tbxNameSong.Text = fullnamesong;
                mainMediaElement.Source = new Uri(songpath);
                mainMediaElement.Play();
            }
            else MessageBox.Show("Choose group and song!");

            lbSong.SelectedIndex = -1;
            lbPrivate.SelectedIndex = -1;
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mainMediaElement.Pause();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            mainMediaElement.Play();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mainMediaElement.Stop();
        }

        private void volSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mainMediaElement.Volume = (double)volSlider.Value;
        }


        private void timelineSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mainMediaElement.Position = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(timelineSlider.Value));

        }
        public string Milliseconds_to_Minute(long milliseconds)
        {
            int seconds = (int)(milliseconds / 1000);
            int minute = (int)(milliseconds / (1000 * 60));
            if(seconds >59)
            {
                seconds -=minute * 60;
            }
            return (minute + " : " + seconds);
        }

        private void mainMediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            timelineSlider.Minimum = 0;
            timelineSlider.Maximum = mainMediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;

            ticks.Interval = TimeSpan.FromMilliseconds(1);
            ticks.Tick += ticks_Tick;
            tick = new timerTick(Tick);
            ticks.Start();
        }
        void ticks_Tick(object sender, object e)
        {
            Dispatcher.Invoke(tick);
        }

        void Tick()
        {
            timelineSlider.Value = mainMediaElement.Position.TotalMilliseconds;
            tbxTimer.Text = Milliseconds_to_Minute((long)mainMediaElement.Position.TotalMilliseconds);
        }

        private void RemoveFiles()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(startupPath);

            foreach (FileInfo file in dirInfo.GetFiles())
            {
                file.Delete();
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RemoveFiles();
        }


        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            HiddenElements();
            lbSong.Items.Clear();
            lbGroup.Items.Clear();
            mainMediaElement.Source = null;
            LoadGroupBox();
            lbPrivate.Items.Clear();
            //LoadToPrivateListBox();
        }
        public void LoadToPrivateListBox()
        {
            try
            {
                UsersOperations ops = new UsersOperations();
                Global.LoggedInUser.PersonalMusic = ops.GetListPersonalMusic(Global.LoggedInUser.Id);
                Global.LoggedInUser.PersonalMusicId = ops.GetListPersonalMusicId(Global.LoggedInUser.Id);
                Global.LoggedInUser.PersonalMusicFileType = ops.GetListPersonalMusicFileType(Global.LoggedInUser.Id);

                if (Global.LoggedInUser.PersonalMusic != null)
                {
                    foreach (string song in Global.LoggedInUser.PersonalMusic)
                    {
                        lbPrivate.Items.Add(song);
                    }
                }
            }
            catch (Exception) { };
        }

        private void btnLoadPersonalList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Global.LoggedInUser.Mail != null)
                {
                    lbPrivate.Visibility = Visibility.Visible;
                    tbxPersonalList.Visibility = Visibility.Visible;
                    LoadToPrivateListBox();
                }
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("You needed log in!");
            }
        }
    }
}
