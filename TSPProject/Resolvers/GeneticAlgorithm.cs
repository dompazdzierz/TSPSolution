using System;
using System.Collections.Generic;
using System.Linq;
using TSPProject.Extensions;

namespace TSPProject
{

    class GeneticAlgorithm : ResolverBase
    {
        private Random Rng { get; }
        private List<Individual> Population { get; set; }
        private int PopulationSize { get; }
        private double CrossProb { get; }
        private double MutProb { get; }
        private int IterationNumber { get; }
        private double SelectionParameter { get; }
        private bool IsTournament { get;  }

        public GeneticAlgorithm ( 
                double[,] adjacencyMatrix,
                int populationSize,
                double crossProb,
                double mutProb,
                int iterationNumber,
                bool isTournament,
                double selectionParameter = 0
            ) : base(adjacencyMatrix, "GA_ps" + populationSize + "_cp" + crossProb + "_mp" + mutProb + "_in" + iterationNumber + "_sp" + selectionParameter + "_t" + isTournament) 
        {
            PopulationSize = populationSize;
            CrossProb = crossProb;
            MutProb = mutProb;
            IterationNumber = iterationNumber;
            SelectionParameter = selectionParameter;
            Rng = new Random();
            IsTournament = isTournament;
        }

        public override List<Record> ResolveProblem()
        {
            var records = new List<Record>();
            Population = GeneratePopulation();

            for(int i = 0; i < IterationNumber; i++)
            {
                var best = Population[0].CalcFittness(AdjacencyMatrix);
                double sum = 0;
                var worst = Population[0].CalcFittness(AdjacencyMatrix);
                for(int j = 0; j < PopulationSize; j++)
                {
                    double fitness = Population[j].CalcFittness(AdjacencyMatrix);
                    sum += fitness;
                    if (fitness < best)
                        best = fitness;
                    if (fitness > worst)
                        worst = fitness;
                }

                records.Add(new Record(best, sum / PopulationSize, worst));
                GAIteration();
            }

            return records;
        }

        public void GAIteration()
        {
            Crossover();
            Mutation();
        }

        private List<Individual> GeneratePopulation()
        {
            var population = new List<Individual>();
            
            for(int i = 0; i < PopulationSize; i++)
            {
                var genotype = Enumerable.Range(0, GenotypeLength).ToList().Shuffle();
                population.Add(new Individual(genotype));
            }

            return population;
        }

        private Individual TournamentSelectionHelper()
        {
            var winner = Population[Rng.Next(0, PopulationSize)];

            for (int j = 1; j < SelectionParameter; j++)
            {
                var competitor = Population[Rng.Next(0, PopulationSize)];
                    
                if(competitor.CalcFittness(AdjacencyMatrix) < winner.CalcFittness(AdjacencyMatrix))  
                    winner = competitor;
            }            

            return winner;
        }

        private List<Individual> TournamentSelection()
        {
            var individuals = new List<Individual>();

            for(int i = 0; i < PopulationSize  * 2 ; i++)
            {
                individuals.Add(TournamentSelectionHelper());
            }

            return individuals;
        }

        private List<Individual> RouletteSelection()
        {
            var fitnessList = new List<double>();
            var weightList = new List<double>();
            var weightSum = 0.0;

            var maxFitness = Population[0].CalcFittness(AdjacencyMatrix);
            var minFitness = Population[0].CalcFittness(AdjacencyMatrix);
            foreach (Individual individual in Population)
            {
                var fitness = individual.CalcFittness(AdjacencyMatrix);
                fitnessList.Add(fitness);
                if(fitness > maxFitness)
                {
                    maxFitness = fitness;
                }
                if(fitness < minFitness)
                {
                    minFitness = fitness;
                }
            }

            for (int i = 0; i < PopulationSize; i++)
            {
                var percentBetter = 100 * (maxFitness - fitnessList[i]) / maxFitness;
                var weight = Math.Pow(percentBetter, 5) / 10000 + SelectionParameter;
                weightSum += weight;
                weightList.Add(weightSum);
            }


            List<Individual> individuals = new List<Individual>();

            while (individuals.Count < PopulationSize * 2)
            {
                var searchedProbability = Rng.NextDouble() * weightSum;

                for (int i = 0; i < PopulationSize; i++)
                {
                    if (searchedProbability <= weightList[i])
                    {
                        individuals.Add(Population[i]);
                        break;
                    }
                }
            }
          
            return individuals;
        }

        private void Crossover()
        {
            var newPopulation = new List<Individual>();
            List<Individual> parents;

            if (IsTournament)
                parents = TournamentSelection();
            else
                parents = RouletteSelection();

            int parentIndex = 0;

            while (newPopulation.Count < PopulationSize)
            {
                Individual oneParent = parents[parentIndex];
                Individual otherParent = parents[parentIndex + 1];

                if (Rng.NextDouble() < CrossProb)
                {
                    var onePivot = Rng.Next(0, GenotypeLength);
                    var otherPivot = Rng.Next(0, GenotypeLength);

                    if(onePivot != otherPivot)
                    {
                        if(onePivot > otherPivot)
                        {
                            var temp = onePivot;
                            onePivot = otherPivot;
                            otherPivot = temp;
                        }

                        List<int> preservedGenes = oneParent.Genotype.GetRange(onePivot, otherPivot - onePivot);
                        List<int> otherGenes = otherParent.Genotype.Where(x => !preservedGenes.Contains(x)).ToList();
                        List<int> newGenotype = new List<int>();

                        newGenotype.AddRange(otherGenes.GetRange(0, onePivot));
                        newGenotype.AddRange(preservedGenes);
                        newGenotype.AddRange(otherGenes.GetRange(onePivot, otherGenes.Count - onePivot));

                        newPopulation.Add(new Individual(newGenotype));
                    }
                    else
                    {
                        newPopulation.Add(oneParent);
                        newPopulation.Add(otherParent);
                    }
                }
                else
                {
                    newPopulation.Add(oneParent);
                    newPopulation.Add(otherParent);
                }

                parentIndex = parentIndex + 2;
            }

            if (newPopulation.Count > PopulationSize)
                newPopulation.RemoveAt(newPopulation.Count - 1);

            Population = newPopulation;
        }

        private void Mutation()
        {
            for(int i = 0; i < PopulationSize; i++)
            {
                if (Rng.NextDouble() < MutProb)
                {
                    var oneIndex = Rng.Next(0, GenotypeLength);
                    var otherIndex = Rng.Next(0, GenotypeLength);

                    if (oneIndex != otherIndex)
                    {
                        if (oneIndex > otherIndex)
                        {
                            var tempIndex = oneIndex;
                            oneIndex = otherIndex;
                            otherIndex = tempIndex;
                        }

                        while(oneIndex < otherIndex)
                        {
                            var temp = Population[i].Genotype[oneIndex];
                            Population[i].Genotype[oneIndex] = Population[i].Genotype[otherIndex];
                            Population[i].Genotype[otherIndex] = temp;
                            oneIndex++;
                            otherIndex--;
                        }
                    }
                }
            }
        }

    }
}
