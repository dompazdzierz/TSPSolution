

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using TSPProject.Resolvers;

namespace TSPProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var threads = new Thread[] {
                CreateThreadForResolvingProblem (
                    new Problem("kroA200.tsp"), 
                    new GeneticAlgorithm (
                        new Problem("kroA200.tsp").AdjacencyMatrix,
                        populationSize: 300, 
                        crossProb: 0.7,
                        mutProb: 0.1, 
                        iterationNumber: 600, 
                        isTournament: true,
                        selectionParameter: 1
                   )
                ),
                CreateThreadForResolvingProblem (
                    new Problem("kroA200.tsp"),
                    new GeneticAlgorithm (
                        new Problem("kroA200.tsp").AdjacencyMatrix,
                        populationSize: 300,
                        crossProb: 0.7,
                        mutProb: 0.1,
                        iterationNumber: 600,
                        isTournament: true,
                        selectionParameter: 2
                   )
                ),
                CreateThreadForResolvingProblem (
                    new Problem("kroA200.tsp"),
                    new GeneticAlgorithm (
                        new Problem("kroA200.tsp").AdjacencyMatrix,
                        populationSize: 300,
                        crossProb: 0.7,
                        mutProb: 0.1,
                        iterationNumber: 600,
                        isTournament: true,
                        selectionParameter: 5
                   )
                ),
                CreateThreadForResolvingProblem (
                    new Problem("kroA200.tsp"),
                    new GeneticAlgorithm (
                        new Problem("kroA200.tsp").AdjacencyMatrix,
                        populationSize: 300,
                        crossProb: 0.7,
                        mutProb: 0.1,
                        iterationNumber: 600,
                        isTournament: true,
                        selectionParameter: 10
                   )
                ),
                CreateThreadForResolvingProblem (
                    new Problem("kroA150.tsp"),
                    new GeneticAlgorithm (
                        new Problem("kroA150.tsp").AdjacencyMatrix,
                        populationSize: 300,
                        crossProb: 0.7,
                        mutProb: 0.1,
                        iterationNumber: 600,
                        isTournament: true,
                        selectionParameter: 1
                   )
                ),
                CreateThreadForResolvingProblem (
                    new Problem("kroA150.tsp"),
                    new GeneticAlgorithm (
                        new Problem("kroA150.tsp").AdjacencyMatrix,
                        populationSize: 300,
                        crossProb: 0.7,
                        mutProb: 0.1,
                        iterationNumber: 600,
                        isTournament: true,
                        selectionParameter: 2
                   )
                ),
                CreateThreadForResolvingProblem (
                    new Problem("kroA150.tsp"),
                    new GeneticAlgorithm (
                        new Problem("kroA150.tsp").AdjacencyMatrix,
                        populationSize: 300,
                        crossProb: 0.7,
                        mutProb: 0.1,
                        iterationNumber: 600,
                        isTournament: true,
                        selectionParameter: 5
                   )
                ),
                CreateThreadForResolvingProblem (
                    new Problem("kroA150.tsp"),
                    new GeneticAlgorithm (
                        new Problem("kroA150.tsp").AdjacencyMatrix,
                        populationSize: 300,
                        crossProb: 0.7,
                        mutProb: 0.1,
                        iterationNumber: 600,
                        isTournament: true,
                        selectionParameter: 10
                   )
                ),
                CreateThreadForResolvingProblem (
                    new Problem("kroA100.tsp"),
                    new GeneticAlgorithm (
                        new Problem("kroA100.tsp").AdjacencyMatrix,
                        populationSize: 300,
                        crossProb: 0.7,
                        mutProb: 0.1,
                        iterationNumber: 600,
                        isTournament: true,
                        selectionParameter: 1
                   )
                ),
                CreateThreadForResolvingProblem (
                    new Problem("kroA100.tsp"),
                    new GeneticAlgorithm (
                        new Problem("kroA100.tsp").AdjacencyMatrix,
                        populationSize: 300,
                        crossProb: 0.7,
                        mutProb: 0.1,
                        iterationNumber: 600,
                        isTournament: true,
                        selectionParameter: 2
                   )
                ),
                CreateThreadForResolvingProblem (
                    new Problem("kroA100.tsp"),
                    new GeneticAlgorithm (
                        new Problem("kroA100.tsp").AdjacencyMatrix,
                        populationSize: 300,
                        crossProb: 0.7,
                        mutProb: 0.1,
                        iterationNumber: 600,
                        isTournament: true,
                        selectionParameter: 5
                   )
                ),
                CreateThreadForResolvingProblem (
                    new Problem("kroA100.tsp"),
                    new GeneticAlgorithm (
                        new Problem("kroA100.tsp").AdjacencyMatrix,
                        populationSize: 300,
                        crossProb: 0.7,
                        mutProb: 0.1,
                        iterationNumber: 600,
                        isTournament: true,
                        selectionParameter: 10
                   )
                )
            };

           foreach(Thread thread in threads)
           {
                thread.Start();
           }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }
        }


        static Thread CreateThreadForResolvingProblem(Problem problem, ResolverBase resolver)
        {
            return new Thread(() =>
            {
                ResolveProblem(new List<Problem> { problem }, new List<ResolverBase> { resolver });
            });
        }

        static void ResolveProblem(List<Problem> problems, List<ResolverBase> resolvers)
        {
            foreach(Problem problem in problems)
            {
                for (int a = 0; a < resolvers.Count; a++)
                {
                    var recordsLists = new List<Record>[10];
                    for(int i = 0; i < 10; i++)
                    {
                        recordsLists[i] = resolvers[a].ResolveProblem();
                    }

                    string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string filePath = Path.Combine(currentDirectory, "output", problem.FileName + "_" + resolvers[a].Name + "_" + a +  ".csv");
                    using (StreamWriter writer = File.CreateText(filePath))
                    {
                        writer.Write("id;");
                        for(int i = 0; i < 10; i++)
                        {
                            writer.Write($"best_{i + 1};avg_{i + 1};worst_{i + 1}");
                            if (i != 9)
                                writer.Write(";");
                        }
                        writer.WriteLine();

                        for (int i = 0; i < recordsLists[0].Count; i++)
                        {
                            writer.Write(i + ";");
                            
                            for (int j = 0; j < 10; j++)
                            {
                                writer.Write(recordsLists[j][i].Best + ";" + recordsLists[j][i].Avg + ";" + recordsLists[j][i].Worst);
                                if (j != 9)
                                    writer.Write(";");
                            }
                            writer.WriteLine();
                        }
                    }
                    Console.WriteLine(filePath);
                }
            }
        }

        static List<Problem> PrepareProblems()
        {
            var problems = new List<Problem>();
            problems.Add(new Problem("kroA200.tsp"));
            //problems.Add(new Problem("kroA100.tsp"));
            //problems.Add(new Problem("kroA150.tsp"));

            return problems;
        }

        static List<ResolverBase> PrepareResolversForProblem(double[,] adjacencyMatrix)
        {
            var resolvers = new List<ResolverBase>();
            //resolvers.Add( new GeneticAlgorithm(
            //    adjacencyMatrix, populationSize: 300, crossProb: 0.7, mutProb: 0.1, iterationNumber: 600, selectionParameter: 0.5
            //));
            //resolvers.Add(new GeneticAlgorithm(
            //    adjacencyMatrix, populationSize: 300, crossProb: 0.7, mutProb: 0.1, iterationNumber: 600, selectionParameter: 2
            //));
            //resolvers.Add(new GeneticAlgorithm(
            //    adjacencyMatrix, populationSize: 300, crossProb: 0.7, mutProb: 0.1, iterationNumber: 600, selectionParameter: 5
            //));
            //resolvers.Add(new GeneticAlgorithm(
            //    adjacencyMatrix, populationSize: 300, crossProb: 0.7, mutProb: 0.1, iterationNumber: 600, selectionParameter: 10
            //));

            return resolvers;
        }

        static void WriteSolutions(List<Solution> solutions)
        {
            foreach(Solution solution in solutions)
            {
                Console.WriteLine("Algorithm: " + solution.ResolverName);
                Console.WriteLine("Best individual: " + solution.Individual);
                Console.WriteLine("Shortest distnace: " + solution.Distance);
                Console.WriteLine();
            }
        }
    }
}
