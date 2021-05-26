namespace ExamTimer
{
    class Part
    {
        private string title;
        private string subtitle;
        private double time;

        public Part(string title, string subtitle, double time)
        {
            this.title = title;
            this.subtitle = subtitle;
            this.time = time;
        }

        public Part(string title, double time)
        {
            this.title = title;
            this.time = time;
        }

        public Part(string title)
        {
            this.title = title;
        }

        public string Title { get => title; }
        public string Subtitle { get => subtitle; }
        public double Time { get => time; }
    }
}