namespace OdeToFood.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using OdeToFood.Entities;

    public class RestaurantEditViewModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }

        public CuisineType Cuisine { get; set; }
    }
}
