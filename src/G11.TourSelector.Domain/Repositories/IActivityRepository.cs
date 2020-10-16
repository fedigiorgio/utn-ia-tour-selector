using G11.TourSelector.Domain.Entities;

using System.Collections.Generic;

namespace G11.TourSelector.Domain.Repositories
{
    public interface IActivityRepository
    {
        IList<Activity> Get();

        void Add(Activity activity);
    }
}
