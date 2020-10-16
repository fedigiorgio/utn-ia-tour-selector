using Accord.Genetic;

using G11.TourSelector.Domain.Entities;
using G11.TourSelector.Domain.Repositories;

using System;
using System.Collections.Generic;

namespace G11.TourSelector.Domain.GeneticAlgorithm
{
    public class TourChromosome : ChromosomeBase
    {
        private static Random _random = new Random();
        private readonly IActivityRepository _repository;

        public TourChromosome(IActivityRepository repository)
        {
            _repository = repository;
            Generate();
        }

        public TourChromosome(IActivityRepository repository, IList<Activity> tour)
        {
            _repository = repository;
            Tour = tour;
        }

        public IList<Activity> Tour { get; private set; }

        public override IChromosome Clone() => new TourChromosome(_repository, Tour);

        public override IChromosome CreateNew() => new TourChromosome(_repository);

        public override void Crossover(IChromosome pair)
        {
            var otherChromsome = pair as TourChromosome; // TODO: Checkear si no se puede utilizar un ChromosomeBase<T> u otra clase base para evitar estos casteos.
            for (int i = 0; i < Tour.Count; i++)
            {
                var shouldSwitch = _random.Next(2) == 1; // 50% de probabilidades de cruza.

                if (shouldSwitch)
                {
                    var activity = otherChromsome.Tour[i];
                    var index = Tour.IndexOf(activity);
                    var auxActivity = Tour[i];
                    Tour[i] = activity;
                    Tour[index] = auxActivity;
                }
            }
        }

        public override void Generate()
        {
            Tour = new List<Activity>(_repository.Get());
            var n = Tour.Count;

            while (n > 1)
            {
                n--;
                var randomIndex = _random.Next(n + 1);
                var activity = Tour[randomIndex];
                Tour[randomIndex] = Tour[n];
                Tour[n] = activity;
            }
        }

        public override void Mutate()
        {
            var maxValue = Tour.Count;
            var randomIndex1 = _random.Next(maxValue);
            var randomIndex2 = _random.Next(maxValue);

            var activity = Tour[randomIndex1];
            Tour[randomIndex1] = Tour[randomIndex2];
            Tour[randomIndex2] = activity;
        }
    }
}
