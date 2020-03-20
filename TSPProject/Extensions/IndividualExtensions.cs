
namespace TSPProject.Extensions
{
    public static class IndividualExtensions
    {
        public static double CalcFittness(this Individual individual, double[,] adjacencyMatrix)
        {
            double distance = 0;
            int genotypeLength = individual.Genotype.Count;

            for (int i = 0; i < genotypeLength - 1; i++)
            {
                distance += adjacencyMatrix[individual.Genotype[i], individual.Genotype[i + 1]];

            }
            distance += adjacencyMatrix[individual.Genotype[0], individual.Genotype[genotypeLength - 1]];

            return distance;
        }
    }
}
