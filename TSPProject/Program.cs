

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

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
                        iterationNumber: 1500, 
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
                        iterationNumber: 1500,
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
                        iterationNumber: 1500,
                        isTournament: true,
                        selectionParameter: 10
                   )
                ),
                CreateThreadForResolvingProblem (
                    new Problem("kroA200.tsp"),
                    new GeneticAlgorithm (
                        new Problem("kroA200.tsp").AdjacencyMatrix,
                        populationSize: 300,
                        crossProb: 0.7,
                        mutProb: 0.1,
                        iterationNumber: 1500,
                        isTournament: true,
                        selectionParameter: 30
                   )
                ),
                CreateThreadForResolvingProblem (
                    new Problem("kroA150.tsp"),
                    new GeneticAlgorithm (
                        new Problem("kroA150.tsp").AdjacencyMatrix,
                        populationSize: 300,
                        crossProb: 0.7,
                        mutProb: 0.1,
                        iterationNumber: 1500,
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
                        iterationNumber: 1500,
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
                        iterationNumber: 1500,
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
                        iterationNumber: 1500,
                        isTournament: true,
                        selectionParameter: 30
                   )
                ),
                CreateThreadForResolvingProblem (
                    new Problem("kroA100.tsp"),
                    new GeneticAlgorithm (
                        new Problem("kroA100.tsp").AdjacencyMatrix,
                        populationSize: 300,
                        crossProb: 0.7,
                        mutProb: 0.1,
                        iterationNumber: 1500,
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
                        iterationNumber: 1500,
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
                        iterationNumber: 1500,
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
                        iterationNumber: 1500,
                        isTournament: true,
                        selectionParameter: 30
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
                ResolveProblem( problem, resolver);
            });
        }

        static void ResolveProblem(Problem problem, ResolverBase resolver)
        {           
            var recordsLists = new List<Record>[10];
            for(int i = 0; i < 10; i++)
            {
                recordsLists[i] = resolver.ResolveProblem();
                Console.WriteLine(i + "___" + resolver.Name);
            }

            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(currentDirectory, "output", problem.FileName + "_" + resolver.Name +  ".csv");

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
