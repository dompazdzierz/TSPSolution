
namespace TSPProject.Resolvers
{
    public class Solution
    {
        public Solution(Individual individual, double distance)
        {
            Individual = individual;
            Distance = distance;
        }

        public Individual Individual { get; set; }
        public double Distance { get; set; }
        public string ResolverName { get; set; }
    }
}
