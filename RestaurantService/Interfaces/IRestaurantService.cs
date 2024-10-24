using RestaurantService.Models;
using RestaurantService.DTOs;

namespace RestaurantService.Interfaces
{
    public interface IRestaurantService
    {
        Task<Restaurant?> GetRestaurantById(int id);
        Task<List<Restaurant>> GetAllRestaurants();
        Task<int> AddRestaurant(Restaurant restaurant);
        Task<int> UpdateRestaurant(Restaurant restaurant);
        Task<int> DeleteRestaurant(int id);
        Task<int> AddMenuItem(int restaurantId, MenuItem menuItem);
        Task<int> RemoveMenuItem(int menuItemId);
    }
}
