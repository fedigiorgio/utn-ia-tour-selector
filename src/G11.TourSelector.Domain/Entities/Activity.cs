using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G11.TourSelector.Domain.Entities
{
    public class Activity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public Neighborhood Neighborhood { get; set; }

        public bool IsRange(DateTime startDateAvailability, DateTime endDateAvailability)
        {
            return StartDate >= startDateAvailability && EndDate < endDateAvailability;
        }

        public bool HasCategoriesInCommon(IEnumerable<Category> otherCategories)
        {
            return Categories.Any(c => otherCategories.Contains(c));
        }

        public int CategoriesInCommon(IEnumerable<Category> otherCategories)
        {
            return Categories.Intersect(otherCategories).Count();
        }

        public bool HappensBefore(Activity nextActivity)
        {
            return EndDate < nextActivity.StartDate;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Nombre: {Name}")
                .AppendLine("Categorías: ");

            foreach (var category in Categories)
            {
                stringBuilder.AppendLine(category.ToString());
            }

            stringBuilder.AppendLine($"Barrio: {Neighborhood.Name}")
                .AppendLine($"Horario: {StartDate} - {EndDate}");

            return stringBuilder.ToString();
        }
    }
}
