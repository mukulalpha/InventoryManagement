using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCore
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IMSContext db;

        public InventoryRepository(IMSContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Inventory>> GetInventoriesByName(string name)
        {
            return await this.db.Inventories.Where(x => x.InventoryName.ToLower().IndexOf(name.ToLower()) >= 0).ToListAsync();

            //return await this.db.Inventories.Where(x => x.InventoryName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
            //                                        string.IsNullOrWhiteSpace(name)).ToListAsync();
        }

        public async Task AddInventoryAsync(Inventory inventory)
        {
            // To prevent different inventories from having the same name
            if (db.Inventories.Any(x => x.InventoryName.ToLower() == inventory.InventoryName.ToLower())) return;            

            this.db.Inventories.Add(inventory);
            await this.db.SaveChangesAsync();
        }

        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            // To prevent different inventories from having the same name
            //if (db.Inventories.Any(x => x.InventoryId != inventory.InventoryId &&
            //                        x.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase))) return;

            if (db.Inventories.Any(x => x.InventoryId != inventory.InventoryId &&
                                    x.InventoryName.ToLower() == inventory.InventoryName.ToLower())) return;


            var inv = await this.db.Inventories.FindAsync(inventory.InventoryId);
            if (inv != null)
            {
                inv.InventoryName = inventory.InventoryName;
                inv.Price = inventory.Price;
                inv.Quantity = inventory.Quantity;

                await db.SaveChangesAsync();
            }
        }

        public async Task<Inventory?> GetInventoryByIdAsync(int inventoryId)
        {
            return await this.db.Inventories.FindAsync(inventoryId);
        }
    }
}