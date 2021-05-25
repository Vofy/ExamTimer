using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTimer
{
    class Part
    {
        private string name;
        private int time;
        private Part[] subparts;

        public Part(string name, int time)
        {
            this.name = name;
            this.time = time;
        }

        public Part(string name, int time, Part[] subparts)
        {
            this.name = name;
            this.time = time;
            this.subparts = subparts;
        }

        public string Name { get => name; }
        public int Time { get => time; }
        internal Part[] Subparts { get => subparts; }
    }
}
