using Accord.Genetic;

using G11.TourSelector.Domain.Entities;

using System;
using System.Collections.Generic;

namespace G11.TourSelector.Domain.GeneticAlgorithm
{
    public class TourChromosome : ChromosomeBase
    {
        private static Random _random = new Random();
        private readonly IList<Activity> _activities;
        private IList<Activity> _tour;

        public TourChromosome(IList<Activity> activities)
        {
            _activities = activities;
            Generate();
        }

        public override IChromosome Clone()
        {
            throw new System.NotImplementedException();
        }

        public override IChromosome CreateNew() => new TourChromosome(_activities);

        public override void Crossover(IChromosome pair)
        {
            throw new System.NotImplementedException();
        }

        public override void Generate()
        {
            _tour = new List<Activity>(_activities);
            var n = _tour.Count;

            while (n > 1)
            {
                n--;
                var randomIndex = _random.Next(n + 1);
                var activity = _tour[randomIndex];
                _tour[randomIndex] = _tour[n];
                _tour[n] = activity;
            }
        }

        public override void Mutate()
        {
            var maxValue = _tour.Count - 1;
            var randomIndex1 = _random.Next(maxValue);
            var randomIndex2 = _random.Next(maxValue);

            var activity = _tour[randomIndex1];
            _tour[randomIndex1] = _tour[randomIndex2];
            _tour[randomIndex2] = activity;
        }
    }
}
