using Dapper;
using DataAccess;
using RestaurantService.Interfaces;
using RestaurantService.Models;
using System.Data;

namespace RestaurantService.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IDataAccess _dataAccess;

        public RestaurantService(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Restaurant?> GetRestaurantById(int id)
        {
            var sql = @"
                    SELECT r.*, a.*, o.*
                    FROM Restaurant r
                    INNER JOIN Address a ON r.AddressId = a.Id
                    INNER JOIN OperatingHours o ON r.OperatingHoursId = o.Id
                    WHERE r.Id = @Id;
                    ";

            var restaurant = await _dataAccess.GetById<Restaurant>(sql, new { Id = id });

            if (restaurant == null)
            {
                return null;
            }

            var menuItemsSql = "SELECT * FROM MenuItem WHERE RestaurantId = @RestaurantId;";
            var menuItems = await _dataAccess.GetAll<MenuItem>(menuItemsSql, new { RestaurantId = id });
            restaurant.MenuItems = menuItems;

            var feeStructuresSql = "SELECT * FROM FeeStructure WHERE RestaurantId = @RestaurantId;";
            var feeStructures = await _dataAccess.GetAll<FeeStructure>(feeStructuresSql, new { RestaurantId = id });
            restaurant.FeeStructures = feeStructures;

            return restaurant;
        }

        public async Task<List<Restaurant>> GetAllRestaurants()
        {
            var sql = @"
                    SELECT r.*, a.*, o.*
                    FROM Restaurant r
                    INNER JOIN Address a ON r.AddressId = a.Id
                    INNER JOIN OperatingHours o ON r.OperatingHoursId = o.Id;
                    ";
            var restaurants = await _dataAccess.GetAll<Restaurant>(sql);
            return restaurants;
        }

        public async Task<int> AddRestaurant(Restaurant restaurant)
        {
            // Create DataTable for MenuItems to pass as table-valued parameter
            var menuItemsTable = new DataTable();
            menuItemsTable.Columns.Add("Name", typeof(string));
            menuItemsTable.Columns.Add("Description", typeof(string));
            menuItemsTable.Columns.Add("Price", typeof(decimal));

            foreach (var item in restaurant.MenuItems)
            {
                menuItemsTable.Rows.Add(item.Name, item.Description, item.Price);
            }

            // Create DataTable for FeeStructures to pass as table-valued parameter
            var feeStructuresTable = new DataTable();
            feeStructuresTable.Columns.Add("MinimumOrderAmount", typeof(decimal));
            feeStructuresTable.Columns.Add("MaximumOrderAmount", typeof(decimal));
            feeStructuresTable.Columns.Add("FeePercentage", typeof(decimal));

            foreach (var fee in restaurant.FeeStructures)
            {
                feeStructuresTable.Rows.Add(fee.MinimumOrderAmount, fee.MaximumOrderAmount, fee.FeePercentage);
            }

            // Prepare the parameters for the stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("@Name", restaurant.Name);
            parameters.Add("@Street", restaurant.Address.Street);
            parameters.Add("@City", restaurant.Address.City);
            parameters.Add("@State", restaurant.Address.State);
            parameters.Add("@PostalCode", restaurant.Address.PostalCode);
            parameters.Add("@Country", restaurant.Address.Country);
            parameters.Add("@ContactInformation", restaurant.ContactInformation);
            parameters.Add("@OpeningTime", restaurant.OperatingHours.OpeningTime);
            parameters.Add("@ClosingTime", restaurant.OperatingHours.ClosingTime);
            parameters.Add("@MenuItems", menuItemsTable.AsTableValuedParameter("TVP_MenuItem"));
            parameters.Add("@FeeStructures", feeStructuresTable.AsTableValuedParameter("TVP_FeeStructure"));
            parameters.Add("@RestaurantId", dbType: DbType.Int32, direction: ParameterDirection.Output); // Output parameter for the new RestaurantId

            // Call the stored procedure using your DataAccess method
            await _dataAccess.ExecuteStoredProcedure<int>("AddRestaurant", parameters);

            // Get the RestaurantId from the output parameter
            return parameters.Get<int>("@RestaurantId");
        }

        public async Task<int> UpdateRestaurant(Restaurant restaurant)
        {
            var sql = @"
                    UPDATE Address
                    SET Street = @Street, City = @City, State = @State, PostalCode = @PostalCode, Country = @Country
                    WHERE Id = @AddressId;

                    UPDATE OperatingHours
                    SET OpeningTime = @OpeningTime, ClosingTime = @ClosingTime
                    WHERE Id = @OperatingHoursId;

                    UPDATE Restaurant
                    SET Name = @Name, ContactInformation = @ContactInformation
                    WHERE Id = @Id;
                    ";

            var parameters = new
            {
                restaurant.Address.Street,
                restaurant.Address.City,
                restaurant.Address.State,
                restaurant.Address.PostalCode,
                restaurant.Address.Country,
                restaurant.OperatingHours.OpeningTime,
                restaurant.OperatingHours.ClosingTime,
                restaurant.Name,
                restaurant.ContactInformation,
                restaurant.AddressId,
                restaurant.OperatingHoursId,
                restaurant.Id
            };

            return await _dataAccess.Update(sql, parameters);
        }

        public async Task<int> DeleteRestaurant(int id)
        {
            var sql = @"
                    DELETE FROM Restaurant WHERE Id = @Id;
                    DELETE FROM Address WHERE Id = (SELECT AddressId FROM Restaurant WHERE Id = @Id);
                    DELETE FROM OperatingHours WHERE Id = (SELECT OperatingHoursId FROM Restaurant WHERE Id = @Id);
                    ";
            return await _dataAccess.Delete(sql, new { Id = id });
        }


        public async Task<int> AddMenuItem(int restaurantId, MenuItem menuItem) {
            var sql = @"
                INSERT INTO MenuItem (RestaurantId, Name, Description, Price)
                VALUES (@RestaurantId, @Name, @Description, @Price);
                SELECT CAST(SCOPE_IDENTITY() as int);";

            var parameters = new {
                RestaurantId = restaurantId,
                menuItem.Name,
                menuItem.Description,
                menuItem.Price
            };

            int newMenuItemId = (await _dataAccess.InsertAndGetId<int?>(sql, parameters)) ?? 0;
            return newMenuItemId;
        }

        public async Task<int> RemoveMenuItem(int menuItemId) {
            var sql = "DELETE FROM MenuItem WHERE Id = @Id";
            return await _dataAccess.Delete(sql, new { Id = menuItemId });
        }
    }

}
