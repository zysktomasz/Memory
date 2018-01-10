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
        public event PropertyChangedEventHandler PropertyChanged;

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
