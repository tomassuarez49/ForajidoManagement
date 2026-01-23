using Microsoft.EntityFrameworkCore;

public class SaleService
{
    private readonly AppDbContext _context;

    public SaleService(AppDbContext context)
    {
        _context = context;
    }

    // =========================
    // CREATE SALE
    // =========================
    public SaleResponseDto Create(CreateSaleDto dto)
    {
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            Date = DateTime.UtcNow,
            PaymentMethod = dto.PaymentMethod,
            Items = new List<SaleItem>()
        };

        decimal total = 0;

        foreach (var dtoItem in dto.Items)
        {
            var product = _context.Products
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == dtoItem.ProductId)
                ?? throw new Exception("Producto no existe");

            var stock = _context.StockMovements
                .AsNoTracking()
                .Where(m => m.ProductId == dtoItem.ProductId)
                .Sum(m => m.Type == "IN" ? m.Quantity : -m.Quantity);

            if (stock < dtoItem.Quantity)
                throw new Exception("Stock insuficiente");

            var item = new SaleItem
            {
                Id = Guid.NewGuid(),
                SaleId = sale.Id,
                ProductId = dtoItem.ProductId,
                Quantity = dtoItem.Quantity,
                UnitPrice = product.SalePrice
            };

            sale.Items.Add(item);
            total += item.Quantity * item.UnitPrice;
        }

        sale.Total = total;

        _context.Sales.Add(sale);

        _context.CashMovements.Add(new CashMovement
        {
            Id = Guid.NewGuid(),
            Amount = sale.Total,
            Type = "IN",
            Description = "Venta"
        });

        foreach (var item in sale.Items)
        {
            _context.StockMovements.Add(new StockMovement
            {
                Id = Guid.NewGuid(),
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Type = "OUT",
                Reason = "Sale",
                Date = DateTime.UtcNow
            });
        }

        _context.SaveChanges();

        return MapToResponse(sale);
    }

    // =========================
    // GET ALL SALES
    // =========================
    public List<SaleResponseDto> GetAll()
    {
        var sales = _context.Sales
            .Include(s => s.Items)
            .AsNoTracking()
            .OrderByDescending(s => s.Date)
            .ToList();

        var productNames = _context.Products
            .AsNoTracking()
            .ToDictionary(p => p.Id, p => p.Name);

        return sales.Select(s => MapToResponse(s, productNames)).ToList();
    }

    // =========================
    // GET SALE BY ID
    // =========================
    public SaleResponseDto? GetById(Guid id)
    {
        var sale = _context.Sales
            .Include(s => s.Items)
            .AsNoTracking()
            .FirstOrDefault(s => s.Id == id);

        if (sale == null)
            return null;

        var productNames = _context.Products
            .AsNoTracking()
            .ToDictionary(p => p.Id, p => p.Name);

        return MapToResponse(sale, productNames);
    }

    // =========================
    // ADD ITEM (VENTA INCREMENTAL)
    // =========================
    public SaleResponseDto AddItem(
        string saleGroup,
        string paymentMethod,
        int productId,
        int quantity
    )
    {
        var sale = _context.Sales
            .Include(s => s.Items)
            .FirstOrDefault(s => s.SaleGroup == saleGroup);

        decimal previousTotal = sale?.Total ?? 0;

        if (sale == null)
        {
            sale = new Sale
            {
                Id = Guid.NewGuid(),
                Date = DateTime.UtcNow,
                PaymentMethod = paymentMethod,
                SaleGroup = saleGroup,
                Total = 0,
                Items = new List<SaleItem>()
            };

            _context.Sales.Add(sale);
        }

        var product = _context.Products
            .FirstOrDefault(p => p.Id == productId)
            ?? throw new Exception("Producto no existe");

        var stock = _context.StockMovements
            .Where(m => m.ProductId == productId)
            .Sum(m => m.Type == "IN" ? m.Quantity : -m.Quantity);

        if (stock < quantity)
            throw new Exception("Stock insuficiente");

        var item = new SaleItem
        {
            Id = Guid.NewGuid(),
            SaleId = sale.Id,
            ProductId = productId,
            Quantity = quantity,
            UnitPrice = product.SalePrice
        };

        sale.Items.Add(item);
        _context.SaleItems.Add(item);

        _context.StockMovements.Add(new StockMovement
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            Quantity = quantity,
            Type = "OUT",
            Reason = "Sale",
            Date = DateTime.UtcNow
        });

        sale.Total = sale.Items.Sum(i => i.Quantity * i.UnitPrice);

        var diff = sale.Total - previousTotal;

        if (diff > 0)
        {
            _context.CashMovements.Add(new CashMovement
            {
                Id = Guid.NewGuid(),
                Amount = diff,
                Type = "IN",
                Description = "Venta"
            });
        }

        _context.SaveChanges();

        var productNames = _context.Products
            .AsNoTracking()
            .ToDictionary(p => p.Id, p => p.Name);

        return MapToResponse(sale, productNames);
    }

    // =========================
    // MAPPING (DTO PURO)
    // =========================
    private SaleResponseDto MapToResponse(
        Sale sale,
        Dictionary<int, string> productNames
    )
    {
        return new SaleResponseDto
        {
            Id = sale.Id,
            Date = sale.Date,
            Total = sale.Total,
            PaymentMethod = sale.PaymentMethod,
            Items = sale.Items.Select(i => new SaleItemResponseDto
            {
                ProductId = i.ProductId,
                ProductName = productNames.GetValueOrDefault(i.ProductId, ""),
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        };
    }

    // Overload para Create (cuando aún no hay diccionario)
    private SaleResponseDto MapToResponse(Sale sale)
    {
        var productNames = _context.Products
            .AsNoTracking()
            .ToDictionary(p => p.Id, p => p.Name);

        return MapToResponse(sale, productNames);
    }
}
