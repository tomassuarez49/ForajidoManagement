public class InventoryService
{
    private readonly AppDbContext _context;

    public InventoryService(AppDbContext context)
    {
        _context = context;
    }
    // CREATE
    public Product Create(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return product;
    }

    // READ - ALL
    public List<Product> GetAll()
    {
        return _context.Products.ToList();
    }

    // READ - BY ID
    public Product? GetById(int id)
    {
        return _context.Products.FirstOrDefault(p => p.Id == id);
    }

    // UPDATE
    public bool Update(int id, Product updatedProduct)
    {
        var product = GetById(id);
        if (product == null)
            return false;

        product.Name = updatedProduct.Name;
        product.Category = updatedProduct.Category;
        product.SalePrice = updatedProduct.SalePrice;
        product.PurchasePrice = updatedProduct.PurchasePrice;

        _context.SaveChanges();

        return true;
    }

    // DELETE
    public bool Delete(int id)
    {
        var product = GetById(id);
        if (product == null)
            return false;

        _context.Products.Remove(product);
        _context.SaveChanges();
        return true;
    }
}
