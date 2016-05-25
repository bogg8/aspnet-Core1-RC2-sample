namespace OdeToFood.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using OdeToFood.Entities;

    public class InMemoryRestaurantData : IRestaurantData
    {
        static InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>
                                    {
                                        new Restaurant { Id = 1, Name = "Bravo's" },
                                        new Restaurant { Id = 2, Name = "Papa Gino's" },
                                        new Restaurant { Id = 3, Name = "Milano's" },
                                        new Restaurant { Id = 4, Name = "Wilton House of Pizza" }
                                    };
        }
        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurants;
        }

        public Restaurant Get(int id)
        {
            return _restaurants.FirstOrDefault(x => x.Id == id);
        }

        public int Commit()
        {
            return 0;
        }

        public void Add(Restaurant restaurant)
        {
            restaurant.Id = _restaurants.Max(x => x.Id) + 1;
            _restaurants.Add(restaurant);
        }

        static List<Restaurant> _restaurants;
    }
}