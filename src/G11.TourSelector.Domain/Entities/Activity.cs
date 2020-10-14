using System;
using System.Collections.Generic;
using System.Text;

namespace G11.TourSelector.Domain.Entities
{
    public class Activity
    {
        public int Id { get; set; }

        public int Name { get; set; }

        public TimeSpan Schedule { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public Neighborhood Neighborhood { get; set; }
    }
}
