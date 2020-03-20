using System.Collections.Generic;

namespace TSPProject
{
    public class Individual
    {
        public Individual(List<int> genotype)
        {
            Genotype = genotype;
        }

        public List<int> Genotype { get; set; }

        public override string ToString()
        {
            string s = "";

            foreach (int i in Genotype)
                s += i + " ";

            return s;
        }
    }
}
