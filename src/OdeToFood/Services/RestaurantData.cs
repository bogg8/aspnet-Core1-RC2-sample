using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Services
{
    using OdeToFood.Entities;

    public class SqlRestaurantData : IRestaurantData
    {
        private OdeToFoodDbContext _context;

        public SqlRestaurantData(OdeToFoodDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return this._context.Restaurants.ToList();
        }

        public Restaurant Get(int id)
        {
            return this._context.Restaurants.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Saves changes to the entities associated to a context (unit of work pattern).
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            return this._context.SaveChanges();
        }

        public void Add(Restaurant restaurant)
        {
            this._context.Add(restaurant);
            //this._context.SaveChanges();
        }
    }
}
