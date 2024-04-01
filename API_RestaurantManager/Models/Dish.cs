namespace API_RestaurantManager.Models
{
    public class Dish
    {
        public int DishId { get; set; }
        public string Name { get; set; }
        public int MenuId { get; set; }
        public string DateCreate { get; set; }
        public string Images { get; set; }
    }
}
