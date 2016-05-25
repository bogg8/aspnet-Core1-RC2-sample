namespace OdeToFood.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using OdeToFood.Entities;
    using OdeToFood.Services;
    using OdeToFood.ViewModels;

    [Authorize]
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantData;

        private IGreeter _greeter;

        public HomeController(IRestaurantData restaurantData, IGreeter greeter)
        {
            _restaurantData = restaurantData;
            _greeter = greeter;
        }

        [AllowAnonymous]
        public ViewResult Index()
        {
            var model = new HomePageViewModel
                            {
                                Restaurants = this._restaurantData.GetAll(),
                                CurrentGreeting = this._greeter.GetGreeting()
                            };

            return this.View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = this._restaurantData.Get(id);
            if (model == null)
            {
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, RestaurantEditViewModel input)
        {
            var restaurant = this._restaurantData.Get(id);
            if (restaurant != null && ModelState.IsValid)
            {
                restaurant.Name = input.Name;
                restaurant.Cuisine = input.Cuisine;
                this._restaurantData.Commit();

                return RedirectToAction("Details", new { id = restaurant.Id });
            }

            return this.View(restaurant);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(RestaurantEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var restaurant = new Restaurant { Cuisine = model.Cuisine, Name = model.Name };
                this._restaurantData.Add(restaurant);
                this._restaurantData.Commit();

                return RedirectToAction("Details", new { id = restaurant.Id });
            }

            return this.View();
        }

        public IActionResult Details(int id)
        {
            var model = this._restaurantData.Get(id);
            if (model == null)
            {
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }
    }
}
