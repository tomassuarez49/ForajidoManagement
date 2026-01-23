public class InventoryService
{
    private readonly AppDbContext _context;

    public InventoryService(AppDbContext context)
    {
        _context = context;
    }

    // ➕ CREATE
    public ProductResponseDto Create(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Category = dto.Category,
            PurchasePrice = dto.PurchasePrice,
            SalePrice = dto.SalePrice
        };

        _context.Products.Add(product);
        _context.SaveChanges();

        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Category = product.Category,
            SalePrice = product.SalePrice
        };
    }

    // 📄 GET ALL
    public List<ProductResponseDto> GetAll()
    {
        return _context.Products
            .Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                SalePrice = p.SalePrice
            })
            .ToList();
    }

    // 🔍 GET BY ID
    public ProductResponseDto? GetById(int id)
    {
        return _context.Products
            .Where(p => p.Id == id)
            .Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                SalePrice = p.SalePrice
            })
            .FirstOrDefault();
    }

    // ✏️ UPDATE
    public bool Update(int id, UpdateProductDto dto)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return false;

        product.Name = dto.Name;
        product.Category = dto.Category;
        product.SalePrice = dto.SalePrice;

        _context.SaveChanges();
        return true;
    }

    // 🗑️ DELETE
    public bool Delete(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return false;

        _context.Products.Remove(product);
        _context.SaveChanges();
        return true;
    }
}
