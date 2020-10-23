using Accord.Genetic;

using G11.TourSelector.Domain.Entities;

using System;
using System.Collections.Generic;

namespace G11.TourSelector.Domain.GeneticAlgorithm
{
    public class TourFitnessFunction : IFitnessFunction
    {
        private const int CommonInterestsMultiplier = 600;
        private const int DistanceMultiplier = 100;
        private const int PenaltyInvalidPair = 1100;

        private readonly IEnumerable<Category> _interests;
        private readonly DateTime _startDateAvailability;
        private readonly DateTime _endDateAvailability;

        public TourFitnessFunction(IEnumerable<Category> interests,
            DateTime startDateAvailability,
            DateTime endDateAvailability)
        {
            _interests = interests;
            _startDateAvailability = startDateAvailability;
            _endDateAvailability = endDateAvailability;
        }

        public double Evaluate(IChromosome chromosome)
        {
            var tourChromosome = chromosome as TourChromosome;
            var tour = tourChromosome.Tour;
            double score = 0;

            for (int i = 0; i < (tour.Count - 1); i++)
            {
                var activity = tour[i];
                var nextActivity = tour[i + 1];

                var activityHappensBeforeNextActivity = activity.HappensBefore(nextActivity);
                var activityIsInRange = activity.IsInRange(_startDateAvailability, _endDateAvailability);

                if (activityHappensBeforeNextActivity && activityIsInRange)
                {
                    score -= activity.Neighborhood.Distance(nextActivity.Neighborhood) * DistanceMultiplier;
                    score += activity.CategoriesInCommon(_interests) * CommonInterestsMultiplier;
                }
                else
                {
                    score -= PenaltyInvalidPair;
                }
            }


            return score;
        }
    }
}
