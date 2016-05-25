namespace OdeToFood.Entities
{
    using System.ComponentModel.DataAnnotations;

    public enum CuisineType
    {
        None,
        Italian,
        French,
        American
    }

    public class Restaurant
    {
        public int Id { get; set; }

        [Display(Name = "Restaurant Name"), Required, MaxLength(80)]
        public string Name { get; set; }

        public CuisineType Cuisine { get; set; }
    }
}
