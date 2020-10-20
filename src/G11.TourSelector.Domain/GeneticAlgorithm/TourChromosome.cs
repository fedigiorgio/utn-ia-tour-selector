using Accord.Genetic;

using G11.TourSelector.Domain.Entities;
using G11.TourSelector.Domain.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;

namespace G11.TourSelector.Domain.GeneticAlgorithm
{
    public class TourChromosome : ChromosomeBase
    {
        private static Random _random = new Random();
        private readonly IActivityRepository _repository;
        private int amountOfActivities;

        public TourChromosome(IActivityRepository repository, int amountOfActivities)
        {
            _repository = repository;
            this.amountOfActivities = amountOfActivities;
            Generate();
        }

        public TourChromosome(IActivityRepository repository, IList<Activity> tour, int amountOfActivities)
        {
            _repository = repository;
            Tour = tour;
            this.amountOfActivities = amountOfActivities; 
        }

        public IList<Activity> Tour { get; private set; }

        public override IChromosome Clone() => new TourChromosome(_repository, Tour, amountOfActivities);

        public override IChromosome CreateNew() => new TourChromosome(_repository, amountOfActivities);

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
            SortTour();
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

            var totalRepository = _repository.Get().Count;
            var HashTour = new HashSet<Activity>();

            while (HashTour.Count != this.amountOfActivities)
            {
                var randomIndex = _random.Next(totalRepository - 1);
                var activity = _repository.Get()[randomIndex];
                HashTour.Add(activity);
            }
            Tour = new List<Activity>(HashTour);
            SortTour();
        }

        private void SortTour()
        {
            Tour = Tour.OrderBy(activity => activity.StartDate.ToLocalTime()).ThenBy(activity => activity.EndDate.ToLocalTime()).ToList<Activity>();
        }

        public override void Mutate()
        {
            var maxValue = Tour.Count;
            var randomIndex1 = _random.Next(_repository.Get().Count);
            var randomIndex2 = _random.Next(maxValue);

            var activity = _repository.Get()[randomIndex1];
            SwitchActivity(randomIndex2, activity);
            SortTour();
        }
    }
}
