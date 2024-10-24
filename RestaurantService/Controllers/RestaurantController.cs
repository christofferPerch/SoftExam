using Microsoft.AspNetCore.Mvc;
using RestaurantService.Interfaces;
using RestaurantService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        // GET: api/restaurant/{id}
        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurantById(int id)
        {
            var restaurant = await _restaurantService.GetRestaurantById(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        // GET: api/restaurant/all
        [HttpGet("all")]
        public async Task<ActionResult<List<Restaurant>>> GetAllRestaurants()
        {
            var restaurants = await _restaurantService.GetAllRestaurants();
            return Ok(restaurants);
        }

        // POST: api/restaurant/add
        [HttpPost("add")]
        public async Task<ActionResult> AddRestaurant([FromBody] Restaurant restaurant)
        {
            if (restaurant == null)
            {
                return BadRequest("Restaurant data is invalid.");
            }

            var restaurantId = await _restaurantService.AddRestaurant(restaurant);

            return CreatedAtAction(nameof(GetRestaurantById), new { id = restaurantId }, restaurant);
        }

        // PUT: api/restaurant/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] Restaurant restaurant)
        {
            if (restaurant == null || restaurant.Id != id)
            {
                return BadRequest("Restaurant data is invalid.");
            }

            var result = await _restaurantService.UpdateRestaurant(restaurant);

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/restaurant/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var result = await _restaurantService.DeleteRestaurant(id);

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
