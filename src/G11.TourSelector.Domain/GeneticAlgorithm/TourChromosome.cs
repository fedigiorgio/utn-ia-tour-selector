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
        private readonly int _amountOfActivities;

        public TourChromosome(IActivityRepository repository, int amountOfActivities)
        {
            _repository = repository;
            _amountOfActivities = amountOfActivities;
            Generate();
        }

        public TourChromosome(IActivityRepository repository, IList<Activity> tour, int amountOfActivities)
        {
            _repository = repository;
            Tour = tour;
           _amountOfActivities = amountOfActivities; 
        }

        public IList<Activity> Tour { get; private set; }

        public override IChromosome Clone() => new TourChromosome(_repository, Tour, _amountOfActivities);

        public override IChromosome CreateNew() => new TourChromosome(_repository, _amountOfActivities);

        public override void Crossover(IChromosome pair)
        {
            var otherChromsome = pair as TourChromosome; // TODO: Checkear si no se puede utilizar un ChromosomeBase<T> u otra clase base para evitar estos casteos.

            for (int i = 0; i < Tour.Count; i++)
            {
                var shouldSwitch = _random.Next(2) == 1; // 50% de probabilidades de cruza.

                if (shouldSwitch)
                {
                    var activity = otherChromsome.Tour[i];
                    SwitchActivity(i, activity);
                }
            }
        }

        private void SwitchActivity(int i, Activity activity)
        {
            var index = Tour.IndexOf(activity);
            //Si no lo tiene lo cambio
            if (index == -1)
            {
                Tour[i] = activity;
            }
        }

        public override void Generate()
        {
            var totalActivities = _repository.Get();
            var hashSetTour = new HashSet<Activity>();

            while (hashSetTour.Count < _amountOfActivities)
            {
                var randomIndex = _random.Next(totalActivities.Count - 1);
                var randomActivity = totalActivities[randomIndex];
                hashSetTour.Add(randomActivity);
            }

            Tour = new List<Activity>(hashSetTour);
        }

        public override void Mutate()
        {
            var totalActivities = _repository.Get();
            var maxValue = Tour.Count;

            var randomIndex1 = _random.Next(totalActivities.Count);
            var randomIndex2 = _random.Next(maxValue);

            var activity = totalActivities[randomIndex1];
            SwitchActivity(randomIndex2, activity);
        }
    }
}
