using Accord.Genetic;

using G11.TourSelector.Domain.Entities;
using G11.TourSelector.Domain.GeneticAlgorithm;
using G11.TourSelector.Domain.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;

namespace G11.TourSelector.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------PARAMETROS---------------");
            var interests = new List<Category> { Category.Sports, Category.Historic, Category.Music };
            var start = DateTime.Now.Date.AddHours(9);
            var end = DateTime.Now.Date.AddHours(17);

            WriteParameters(interests, start, end);

            Console.WriteLine("---------------ACTIVIDADES SELECCIONADAS---------------");
            var repository = new ActivityRepository();
            WriteActivities(repository.Get());

            var population = new Population(1000, new TourChromosome(repository),
                     new TourFitnessFunction(interests, start, end), new EliteSelection());

            int i = 0;

            while (true)
            {
                population.RunEpoch();
                i++;
                if (population.FitnessMax >= 0.99 || i >= 1000)
                {
                    break;
                }
            }

            var best = population.BestChromosome as TourChromosome;
            var tour = best.Tour;

            var activities = tour.Where(a => a.IsRange(start, end))
                .Where(a => a.HasCategoriesInCommon(interests)).ToList();

            Console.WriteLine("---------------ACTIVIDADES SELECCIONADAS---------------");
            WriteActivities(activities);
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
