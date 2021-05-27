namespace ExamTimer
{
    class Part
    {
        public Part(string partName, string subpartName, double time)
        {
            PartName = partName;
            SubpartName = subpartName;
            Time = time;
        }

        public Part(string partName, string subpartName)
        {
            PartName = partName;
            SubpartName = subpartName;
        }

        public Part(string partName, double time)
        {
            PartName = partName;
            Time = time;
        }

        public Part(string partName)
        {
            PartName = partName;
        }

        public string PartName { get; }
        public string SubpartName { get; }
        public double Time { get; }
    }
}