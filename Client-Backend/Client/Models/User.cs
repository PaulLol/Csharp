using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Client.Models
{
    class User : INotifyPropertyChanged
    {
        private List<string> personalMusic;
        public string Id { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string access_token { get; set; }
        public List<string> PersonalMusic 
        {
            get { return personalMusic; }
            set
            {
                if (value ==personalMusic) return;
                personalMusic = value;
                OnPropertyChanged("PersonalMusic");
            }
        }
        public List<string> PersonalMusicId { get; set; }
        public List<string> PersonalMusicFileType { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
