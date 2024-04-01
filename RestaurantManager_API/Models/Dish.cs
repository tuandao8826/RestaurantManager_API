namespace RestaurantManager_API.Models
{
    public class Dish
    {
        public int DishId { get; set; }
        public string Name { get; set; }
        public int MenuId { get; set; }
        public string DateCreate { get; set; }
        public string Image { get; set; }
    }
}
