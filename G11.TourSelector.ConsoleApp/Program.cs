using Accord.Genetic;

using G11.TourSelector.Domain.Entities;
using G11.TourSelector.Domain.GeneticAlgorithm;
using G11.TourSelector.Domain.Repositories;

using System;
using System.Collections.Generic;

namespace G11.TourSelector.ConsoleApp
{
    class Program
    {
        private static IList<Category> _categories;
        private static DateTime _start = DateTime.Now.Date.AddHours(9);
        private static DateTime _end = DateTime.Now.Date.AddHours(17);
        private static int _numberOfActivities = 3;
        private static int _numberOfEpochs;
        private static int _initialPopulation;
        private static ActivityRepository _repository;
        private static IList<Activity> _activities;
        private static Population _population;

        static void Main(string[] args)
        {
            LoadAndWriteActivities();
            LoadParameters();
            WriteParameters();
            LoadPopulation();
            Run();
            WriteResults();
        }

        private static void LoadParameters()
        {
            Console.WriteLine("---------------SELECCIONAR CATEGORIAS-------------");
            Console.WriteLine("1. GASTRONOMIA");
            Console.WriteLine("2. HISTORIA");
            Console.WriteLine("3. MUSICA");
            Console.WriteLine("4. DEPORTES");
            Console.WriteLine("0. PARA INICIAR");

            _categories = new List<Category>();
            var category = Console.ReadLine();

            while (!category.Equals("0"))
            {
                _categories.Add((Category)int.Parse(category));
                category = Console.ReadLine();
            }

            Console.WriteLine("---------------SELECCIONE LA CANTIDAD DE CORRIDAS-------------");
            _numberOfEpochs = Convert.ToInt32(Console.ReadLine());


            Console.WriteLine("---------------SELECCIONE LA POBLACIÓN INICIAL-------------");
            _initialPopulation = Convert.ToInt32(Console.ReadLine());
        }

        private static void WriteParameters()
        {
            Console.WriteLine("---------------PARAMETROS---------------");
            _categories.WriteInConsole();
            Console.WriteLine($"Disponibilidad: {_start} - {_end}");
            Console.WriteLine($"Cantidad de corridas: { _numberOfEpochs }");
            Console.WriteLine($"Población inicial: { _initialPopulation }");

            Console.WriteLine("Presione cualquier tecla para iniciar");
            Console.ReadKey();
        }

        private static void LoadPopulation()
        {
            _population = new Population(_initialPopulation, new TourChromosome(_repository, _numberOfActivities),
                new TourFitnessFunction(_categories, _start, _end), new EliteSelection());
        }

        private static void LoadAndWriteActivities()
        {
            _repository = new ActivityRepository();
            _activities = _repository.Get();

            Console.WriteLine("---------------ACTIVIDADES DISPONIBLES-------------");
            _activities.WriteInConsole();
        }

        private static void Run()
        {
            for (int i = 0; i < _numberOfEpochs; i++)
            {
                _population.RunEpoch();
                Console.WriteLine("---------------INFO DE LA CORRIDA---------------");
                Console.WriteLine($"Corrida Nº: {i}");
                Console.WriteLine($"FitnessMax: {_population.FitnessMax}");
                Console.WriteLine($"FitnessAvg: {_population.FitnessAvg}");
            }
        }

        private static void WriteResults()
        {
            var best = _population.BestChromosome as TourChromosome;
            var tour = best.Tour;
            Console.WriteLine("---------------CROMOSOMA SELECCIONADO---------------");
            tour.WriteInConsole();
        }
    }
}
