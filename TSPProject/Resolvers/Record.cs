namespace TSPProject
{
    public class Record
    {
        public Record(double best, double avg, double worst)
        {
            Best = best;
            Avg = avg;
            Worst = worst;
        }
        public double Best { get; set; }
        public double Avg { get; set; }
        public double Worst { get; set; }
    }
}
