using System;
using System.Collections.Generic;

namespace G11.TourSelector.Domain.Entities
{
    public class Activity
    {
        public int Id { get; set; }

        public int Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public Neighborhood Neighborhood { get; set; }
    }
}
