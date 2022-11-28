// See https://aka.ms/new-console-template for more information

using IMS.Plugins.EFCore;
using Microsoft.EntityFrameworkCore;

var options = new DbContextOptionsBuilder<IMSContext>();
options.UseInMemoryDatabase("IMS");

using (var db = new IMSContext(options.Options))
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();

    //var products = db.Products.Include(x => x.ProductInventories).ToList();

    var products = db.Products.Include(x => x.ProductInventories).ThenInclude(x => x.Inventory).ToList();
    foreach (var product in products)
    {
        Console.WriteLine("- " + product.ProductName);
        var invs = product?.ProductInventories?.Select(x => x.Inventory);
        if (invs != null)
        {
            foreach (var inv in invs)
            {
                Console.WriteLine("   " + inv.InventoryName);
            }
        }        
        
        Console.WriteLine();

    }


}
