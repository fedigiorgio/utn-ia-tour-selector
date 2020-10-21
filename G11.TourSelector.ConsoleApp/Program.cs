using Accord.Genetic;

using G11.TourSelector.Domain.Entities;
using G11.TourSelector.Domain.GeneticAlgorithm;
using G11.TourSelector.Domain.Repositories;

using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace G11.TourSelector.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string categories;
            var interests = new List<Category>();
            Console.WriteLine("---------------SELECCIONAR CATEGORIAS-------------");
            Console.WriteLine("1. GASTRONOMIA");
            Console.WriteLine("2. HISTORIA");
            Console.WriteLine("3. MUSICA");
            Console.WriteLine("4. DEPORTES");
            Console.WriteLine("0. PARA INICIAR");
            
            categories = Console.ReadLine();
            while (!categories.Equals("0")) {
                interests.Add((Category) int.Parse(categories));
                categories = Console.ReadLine();
            }

            Console.WriteLine("---------------PARAMETROS---------------");
            //var interests = new List<Category> { Category.Sports, Category.Historic, Category.Music };
            var start = DateTime.Now.Date.AddHours(9);
            var end = DateTime.Now.Date.AddHours(17);

            WriteParameters(interests, start, end);

            Console.WriteLine("---------------ACTIVIDADES DISPONIBLES---------------");
            var repository = new ActivityRepository();
            var numberOfActivities = 3;
            WriteActivities(repository.Get());
            var population = new Population(10, new TourChromosome(repository, numberOfActivities),
                     new TourFitnessFunction(interests, start, end), new EliteSelection());

            int i = 0;

            while (true)
            {
                population.RunEpoch();
                i++;
                Console.WriteLine("---------------INFO EPOCH---------------");
                Console.WriteLine($"Epoch: {i}");
                Console.WriteLine($"FitnessMax: {population.FitnessMax}");
                Console.WriteLine($"FitnessAvg: {population.FitnessAvg}");
                if (population.FitnessMax >= 50 || i >= 1000)
                {
                    Console.WriteLine("---------------NUMERO DE EPOCHS---------------");
                    Console.WriteLine(i);
                    break;
                }
            }

            var best = population.BestChromosome as TourChromosome;
            var tour = best.Tour;


            Console.WriteLine("---------------ACTIVIDADES SELECCIONADAS---------------");
            Console.WriteLine(best.Fitness);
            WriteActivities(tour);
        }

        static void WriteParameters(List<Category> interests, DateTime start, DateTime end)
        {
            for (int i = 0; i < interests.Count; i++)
            {
                Console.WriteLine(interests[i].ToString());
            }

            Console.WriteLine($"Disponibilidad: {start} - {end}");
        }

        static void WriteActivities(IList<Activity> activities)
        {
            for (int i = 0; i < activities.Count; i++)
            {
                Console.WriteLine(activities[i].ToString());
            }
        }
    }
}
