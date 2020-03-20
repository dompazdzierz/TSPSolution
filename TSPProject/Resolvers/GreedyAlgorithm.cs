using System.Collections.Generic;
using System.Linq;
using TSPProject.Resolvers;
using TSPProject.Extensions;
using System;

namespace TSPProject
{
    public class GreedyAlgorithm : ResolverBase
    {
        public GreedyAlgorithm(double[,] adjacencyMatrix) : base(adjacencyMatrix, "GREEDY") { }

        public override List<Record> ResolveProblem()
        {
            var bestSolution = BestSolutionForStartCity(0);

            for(int i = 1; i < GenotypeLength; i++)
            {
                var solution = BestSolutionForStartCity(i);
                if (solution.Distance < bestSolution.Distance)
                    bestSolution = solution;
            }
            bestSolution.ResolverName = "Greedy Algorithm";
            return new List<Record>();
            //return bestSolution;
        }

        private Solution BestSolutionForStartCity(int startCity)
        {
            Console.Write(startCity + ": ");
            double distance = 0;
            var notVisited = Enumerable.Range(0, GenotypeLength).ToList();
            var visited = new List<int>();
            visited.Add(startCity);
            notVisited.Remove(startCity);

            for (int i = 0; i < GenotypeLength - 1; i++)
            {
                double bestValue = AdjacencyMatrix[startCity, notVisited[0]];
                int bestIndex = notVisited[0];

                for (int j = 1; j < notVisited.Count; j++)
                {
                    var value = AdjacencyMatrix[startCity, notVisited[j]];
                    if (value < bestValue)
                    {
                        bestValue = value;
                        bestIndex = notVisited[j];
                    }
                }

                distance += bestValue;
                visited.Add(bestIndex);
                notVisited.Remove(bestIndex);
                startCity = bestIndex;
            }
            distance += AdjacencyMatrix[startCity, visited[0]];
            Console.WriteLine(distance);

            return new Solution(new Individual(visited), distance);
        }
    }
}
