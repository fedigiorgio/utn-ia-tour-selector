using Accord.Genetic;

using G11.TourSelector.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;

namespace G11.TourSelector.Domain.GeneticAlgorithm
{
    public class TourFitnessFunction : IFitnessFunction
    {
        private readonly IEnumerable<Category> _interests;
        private readonly DateTime _startDateAvailability;
        private readonly DateTime _endDateAvailability;
        private readonly double _commonInterestsMultiplier, _distanceMultiplier, _penaltyInvalidPair;
        public TourFitnessFunction(IEnumerable<Category> interests,
            DateTime startDateAvailability,
            DateTime endDateAvailability,
            double commonInterestsMultiplier,
            double distanceMultiplier,
            double penaltyInvalidPair
            )
        {
            _interests = interests;
            _startDateAvailability = startDateAvailability;
            _endDateAvailability = endDateAvailability;
            _commonInterestsMultiplier = commonInterestsMultiplier;
            _distanceMultiplier = distanceMultiplier;
            _penaltyInvalidPair = penaltyInvalidPair;
        }

        public double Evaluate(IChromosome chromosome)
        {
            var tourChromosome = chromosome as TourChromosome;
            var tour = tourChromosome.Tour;
            bool hasAtLeastAValidPair = false;
            double score = 0;
            var activitiesInCommon = tour
                .Where(a => a.IsRange(_startDateAvailability, _endDateAvailability))
                .Where(a => a.HasCategoriesInCommon(_interests)).ToList();

            for (int i = 0; i < (activitiesInCommon.Count - 1); i++)
            {
                var activity = activitiesInCommon[i];
                var nextActivity = activitiesInCommon[i + 1];

                var activityHappensBeforeNextActivity = activity.HappensBefore(nextActivity);

                if (activityHappensBeforeNextActivity)
                {
                    hasAtLeastAValidPair = true;
                    score -= activity.Neighborhood.Distance(nextActivity.Neighborhood) * _distanceMultiplier;
                    score += activity.CategoriesInCommon(_interests) * _commonInterestsMultiplier;
                }
                else
                {
                    score -= _penaltyInvalidPair;
                }
            }

            if (!hasAtLeastAValidPair)
            {
                score = double.MinValue;
            }

            return score;
        }
    }
}
