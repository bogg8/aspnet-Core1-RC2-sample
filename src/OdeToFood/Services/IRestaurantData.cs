using System.Collections.Generic;

namespace OdeToFood.Services
{
    using OdeToFood.Entities;

    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();

        Restaurant Get(int id);

        void Add(Restaurant restaurant);

        int Commit();
    }
}
