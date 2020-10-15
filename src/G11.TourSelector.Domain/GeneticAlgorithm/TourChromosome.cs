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

        public TourChromosome()
        {
        }

        public override IChromosome Clone()
        {
            throw new System.NotImplementedException();
        }

        public override IChromosome CreateNew()
        {
            throw new System.NotImplementedException();
        }

        public override void Crossover(IChromosome pair)
        {
            throw new System.NotImplementedException();
        }

        public override void Generate()
        {
            _tour = new List<Activity>();
            var count = _activities.Count;

            for (int i = 0; i < count; i++)
            {
                var randomPosition = _random.Next(count - i);
                _tour.Insert(i, _activities[randomPosition]);
            }
        }

        public override void Mutate()
        {
            throw new System.NotImplementedException();
        }
    }
}
