public class InventoryService
{
    private static List<Product> products = new();
    private static int nextId = 1;

    // CREATE
    public Product Create(Product product)
    {
        product.Id = nextId++;
        products.Add(product);
        return product;
    }

    // READ - ALL
    public List<Product> GetAll()
    {
        return products;
    }

    // READ - BY ID
    public Product? GetById(int id)
    {
        return products.FirstOrDefault(p => p.Id == id);
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

        return true;
    }

    // DELETE
    public bool Delete(int id)
    {
        var product = GetById(id);
        if (product == null)
            return false;

        products.Remove(product);
        return true;
    }
}
