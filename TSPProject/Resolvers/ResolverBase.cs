
using System;
using System.Collections.Generic;
using TSPProject.Resolvers;

namespace TSPProject
{
    public abstract class ResolverBase
    {
        protected double[,] AdjacencyMatrix { get; set; }
        protected int GenotypeLength { get; set; }
        public string Name { get; set; }

        public ResolverBase(double[,] adjacencyMatrix, string name)
        {
            AdjacencyMatrix = adjacencyMatrix;
            GenotypeLength = adjacencyMatrix.GetLength(0);
            Name = name;
        }

        public abstract List<Record> ResolveProblem();
    }
}
