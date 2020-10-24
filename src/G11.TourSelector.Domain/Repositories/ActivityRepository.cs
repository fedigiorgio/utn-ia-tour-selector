using G11.TourSelector.Domain.Entities;

using System;
using System.Collections.Generic;

namespace G11.TourSelector.Domain.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private static DateTime _now = DateTime.Now.Date;
        private readonly IList<Activity> _activities;

        public ActivityRepository()
        {
            _activities = Initialize();
        }

        //TODO: De momento estan harcodeadas, considerar si armar un JSON y leerlas de ahi, me parece muy exagerado usar una DB real.
        public IList<Activity> Get() => _activities;

        public void Add(Activity activity) => _activities.Add(activity);

        private IList<Activity> Initialize()
        {
            return new List<Activity>
            {
                new Activity
                {
                    Id = 1,
                    Neighborhood = new Neighborhood
                    {
                        Id = 1,
                        Name = "San Telmo",
                        PosX = 0,
                        PosY = 1
                    },
                    StartDate = _now.AddHours(10).AddMinutes(30),
                    EndDate = _now.AddHours(13),
                    Name = "El Viejo Almacén Tango.",
                    Categories = new List<Category>
                    {
                        Category.Gastronomic,
                        Category.Historic,
                        Category.Music
                    },
                },
                new Activity
                {
                    Id = 2,
                    Neighborhood = new Neighborhood
                    {
                      Id = 2,
                      Name = "Boedo",
                      PosX = 1,
                      PosY = 3
                    },
                    StartDate = _now.AddHours(10),
                    EndDate = _now.AddHours(12),
                    Name = "Bares notables.",
                    Categories = new List<Category>
                    {
                        Category.Gastronomic,
                        Category.Historic
                    },
                },
                new Activity
                {
                    Id = 3,
                    Neighborhood = new Neighborhood
                    {
                        Id = 3,
                        Name = "Núñez",
                        PosY = 4,
                        PosX = 6
                    },
                    StartDate = _now.AddHours(16),
                    EndDate = _now.AddHours(18),
                    Categories = new List<Category>
                    {
                        Category.Sports
                    },
                    Name = "Recorrido cancha y museo de River Plate."
                },
                new Activity
                {
                    Id = 4,
                    Neighborhood = new Neighborhood
                    {
                        Id = 4,
                        Name = "Monserrat",
                        PosX = 2,
                        PosY = 0
                    },
                    StartDate = _now.AddHours(14),
                    EndDate = _now.AddHours(16),
                    Categories = new List<Category>
                    {
                        Category.Historic
                    },
                    Name = "Visita al Cabildo."
                },
                new Activity
                {
                    Id = 5,
                    Neighborhood = new Neighborhood
                    {
                        Id = 5,
                        Name = "La Boca",
                        PosY = 0,
                        PosX = 0
                    },
                    StartDate = _now.AddHours(9),
                    EndDate = _now.AddHours(10),
                    Categories = new List<Category>
                    {
                        Category.Sports
                    },
                    Name = "Recorrido cancha y Museo de Boca Juniors."
                },
                new Activity
                {
                    Id = 6,
                    Neighborhood = new Neighborhood
                    {
                        Id = 6,
                        Name = "San Cristobal",
                        PosX = 2,
                        PosY = 1
                    },
                    StartDate = _now.AddHours(14),
                    EndDate = _now.AddHours(16),
                    Categories = new List<Category>{ Category.Gastronomic, Category.Historic },
                    Name = "Visita bar notable: Miramar"
                },
                new Activity
                {
                    Id = 7,
                    Neighborhood = new Neighborhood
                    {
                        Id = 7,
                        Name = "Parque Patricios",
                        PosX = 1,
                        PosY = 5
                    },
                    Categories = new List<Category> { Category.Sports },
                    StartDate = _now.AddHours(12),
                    EndDate = _now.AddHours(14),
                    Name = "Visita a Estadio Tomás Adolfo Ducó"
                },
                new Activity
                {
                    Id = 8,
                    Neighborhood = new Neighborhood
                    {
                        Id = 8,
                        Name = "Palermo",
                        PosX = 4,
                        PosY = 4,
                    },
                    Name = "Visita al jardín japonés",
                    Categories = new List<Category> { Category.Cultural },
                    StartDate = _now.Date.AddHours(9),
                    EndDate = _now.Date.AddHours(11)
                },
                new Activity
                {
                    Id = 8,
                    Neighborhood = new Neighborhood
                    {
                        Id = 9,
                        Name = "Tribunales",
                        PosX = 3,
                        PosY = 0,
                    },
                    Name = "Visita a Pizzeria Guerrin",
                    Categories = new List<Category> { Category.Gastronomic },
                    StartDate = _now.Date.AddHours(12),
                    EndDate = _now.Date.AddHours(14)
                },
                new Activity
                {
                    Id = 9,
                    Neighborhood = new Neighborhood
                    {
                        Id = 10,
                        Name = "Palermo",
                        PosX = 4,
                        PosY = 4
                    },
                    Name = "Visita al Planetario",
                    Categories = new List<Category> { Category.Cultural },
                    StartDate = _now.AddHours(15),
                    EndDate = _now.AddHours(17)
                },
                new Activity
                {
                    Id = 10,
                    Neighborhood = new Neighborhood
                    {
                        Id = 11,
                        Name = "Villa Crespo",
                        PosX = 3,
                        PosY = 4,
                    },
                    Name = "Club Cultural Matienzo",
                    Categories = new List<Category> { Category.Cultural, Category.Music },
                    StartDate = _now.AddHours(13),
                    EndDate = _now.AddHours(14)
                }
            };
        }
    }
}
