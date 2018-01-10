using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    /// <summary>
    /// zawiera informacje o planszy dla aktualnej rozgrywki
    /// </summary>
    class MemoryBoard : INotifyPropertyChanged
    {


        public int ClickedCount { get; set; }
        public int RevealedCards { get; set; }
        public string TimePassedString { get; set; } = "00:00:00";

        public event PropertyChangedEventHandler PropertyChanged;

        // przechowuje czas sekund od rozpoczecia rozgrywki
        private int timePassed;
        public int TimePassed
        {
            get { return timePassed; }
            set {
                timePassed = value;
                TimePassedString = TimeSpan.FromSeconds(value).ToString();
                OnPropertyChanged("TimePassedString"); // aktywuje event powiazany z labelem od czasu w UI
            }
        }


        private int clicks;
        public int Clicks
        {
            get { return clicks; }
            set
            {
                clicks = value;
                OnPropertyChanged("Clicks");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public MemoryBoard()
        {
        }



    }
}
