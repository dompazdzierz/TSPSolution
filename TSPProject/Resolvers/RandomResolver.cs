using System.Collections.Generic;
using System.Linq;
using TSPProject.Extensions;

namespace TSPProject.Resolvers
{
    class RandomResolver : ResolverBase
    {
        public int TriesNumber { get; }

        public RandomResolver(double[,] adjacencyMatrix, int triesNumber) : base(adjacencyMatrix, "RANDOM") 
        {
            TriesNumber = triesNumber;
        }

        public override List<Record> ResolveProblem()
        {
            var bestSolution = GenerateSolution();

            for (int i = 1; i < TriesNumber; i++)
            {
                var solution = GenerateSolution();
                if (solution.Distance < bestSolution.Distance)
                    bestSolution = solution;
            }

            bestSolution.ResolverName = "Random Resolver";
            return new List<Record>();
            //return bestSolution;
        }

        private Solution GenerateSolution()
        {
            var genotype = Enumerable.Range(0, GenotypeLength).ToList().Shuffle();
            var individual = new Individual(genotype);
            return new Solution(individual, individual.CalcFittness(AdjacencyMatrix));
        }
    }
}
