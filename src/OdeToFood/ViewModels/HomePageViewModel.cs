using System.Collections.Generic;

namespace OdeToFood.ViewModels
{
    using OdeToFood.Entities;

    public class HomePageViewModel
    {
        public IEnumerable<Restaurant> Restaurants { get; set; }

        public string CurrentGreeting { get; set; }
    }
}
