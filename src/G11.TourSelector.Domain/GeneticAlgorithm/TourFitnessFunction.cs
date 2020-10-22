using Accord.Genetic;

using G11.TourSelector.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;

namespace G11.TourSelector.Domain.GeneticAlgorithm
{
    public class TourFitnessFunction : IFitnessFunction
    {
        private const int CommonInterestsMultiplier = 90;
        private const int DistanceMultiplier = 10;
        private const int PenaltyOverlapActivity = 500;
        private const int PenaltyInvalidPair = 1000;

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

            var GetOverlaps = new Func<Activity, List<Activity>>(current => tour
            .Except(new[] { current })
            .Where(a => current.IsOverlap(a))
            .ToList());

            double score = 0;

            for (int i = 0; i < (tour.Count - 1); i++)
            {
                var activity = tour[i];
                Activity previosActivity = tour.Where(x => x.EndDate <= activity.StartDate).OrderByDescending(x => x.EndDate.ToString("yyyyMMdd HH:mm:SS")).FirstOrDefault();
                Activity nextActivity = tour.Where(x => x.StartDate >= activity.EndDate).OrderBy(x => x.StartDate.ToString("yyyyMMdd HH:mm:SS")).FirstOrDefault();

                //var activityHappensBeforeNextActivity = activity.HappensBefore(nextActivity);
                var activityIsInRange = activity.IsRange(_startDateAvailability, _endDateAvailability);
                var overlaps = GetOverlaps(activity);
                if (activityIsInRange)
                {
                    score -= overlaps.Count() * PenaltyOverlapActivity;
                    score -= previosActivity != null ? activity.Neighborhood.Distance(previosActivity.Neighborhood) * DistanceMultiplier : 0;
                    score -= nextActivity != null ? activity.Neighborhood.Distance(nextActivity.Neighborhood) * DistanceMultiplier : 0;
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
