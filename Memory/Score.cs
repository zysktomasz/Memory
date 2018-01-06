using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    public class Score
    {
        private int clicks;

        public int Clicks
        {
            get { return clicks; }
            set { clicks = value; }
        }

        private TimeSpan time;

        public TimeSpan Time
        {
            get { return time; }
            set { time = value; }
        }

        // constructor

        public Score(int clicks, TimeSpan time)
        {
            Clicks = clicks;
            Time = time;
        }


    }
}
