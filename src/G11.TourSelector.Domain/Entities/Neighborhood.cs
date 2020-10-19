using System;

namespace G11.TourSelector.Domain.Entities
{
    public class Neighborhood
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int PosX { get; set; }

        public int PosY { get; set; }

        public int Distance(Neighborhood otherNeighborhood)
        {
            var distanceX = Math.Abs(PosX - otherNeighborhood.PosX);
            var distanceY = Math.Abs(PosY - otherNeighborhood.PosY);

            return distanceY + distanceX;
        }
    }
}