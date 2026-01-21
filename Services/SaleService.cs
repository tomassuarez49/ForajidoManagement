using Microsoft.EntityFrameworkCore;

public class SaleService
{
    private readonly AppDbContext _context;

    public SaleService(AppDbContext context)
    {
        _context = context;
    }

    //  CREATE SALE
    public Sale Create(Sale sale)
    {
        sale.Id = Guid.NewGuid();
        sale.Date = DateTime.UtcNow;

        decimal total = 0;

        foreach (var item in sale.Items)
        {
            //  Producto
            var product = _context.Products
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == item.ProductId);

            if (product == null)
                throw new Exception("Producto no existe");

            //  Stock actual
            var stock = _context.StockMovements
                .AsNoTracking()
                .Where(m => m.ProductId == item.ProductId)
                .Sum(m => m.Type == "IN" ? m.Quantity : -m.Quantity);

            if (stock < item.Quantity)
                throw new Exception("Stock insuficiente");

            //  Precio y relación
            item.Id = Guid.NewGuid();
            item.SaleId = sale.Id;
            item.UnitPrice = product.SalePrice;

            total += item.Quantity * item.UnitPrice;
        }

        sale.Total = total;

        //  Guardar venta
        _context.Sales.Add(sale);

        //  Movimientos de stock
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

        //  Un solo SaveChanges (sin batching peligroso)
        _context.SaveChanges();

        return sale;
    }

    //  GET ALL SALES
    public List<object> GetAll()
    {
        return _context.Sales
            .Include(s => s.Items)
            .AsNoTracking()
            .OrderByDescending(s => s.Date)
            .Select(s => new
            {
                s.Id,
                s.Date,
                s.Total,
                s.PaymentMethod,
                Items = s.Items.Select(i => new
                {
                    i.ProductId,
                    ProductName = _context.Products
                        .Where(p => p.Id == i.ProductId)
                        .Select(p => p.Name)
                        .FirstOrDefault(),
                    i.Quantity,
                    i.UnitPrice
                })
            })
            .ToList<object>();
    }



    //  GET SALE BY ID (GUID)
    public object? GetById(Guid id)
    {
        return _context.Sales
            .Include(s => s.Items)
            .AsNoTracking()
            .Where(s => s.Id == id)
            .Select(s => new
            {
                s.Id,
                s.Date,
                s.Total,
                s.PaymentMethod,
                Items = s.Items.Select(i => new
                {
                    i.ProductId,
                    ProductName = _context.Products
                        .Where(p => p.Id == i.ProductId)
                        .Select(p => p.Name)
                        .FirstOrDefault(),
                    i.Quantity,
                    i.UnitPrice
                })
            })
            .FirstOrDefault();
    }


    public Sale AddItem(
    string saleGroup,
    string paymentMethod,
    int productId,
    int quantity
)
    {
        // 1️⃣ Buscar venta existente (TRACKED)
        var sale = _context.Sales
            .Include(s => s.Items)
            .FirstOrDefault(s => s.SaleGroup == saleGroup);

        // 2️⃣ Si no existe, crearla
        if (sale == null)
        {
            sale = new Sale
            {
                Id = Guid.NewGuid(),
                Date = DateTime.UtcNow,
                PaymentMethod = paymentMethod,
                SaleGroup = saleGroup,
                Total = 0,
                Items = new List<SaleItem>() // 🔥 IMPORTANTE
            };

            _context.Sales.Add(sale);
        }

        // 3️⃣ Producto (TRACKED)
        var product = _context.Products
            .FirstOrDefault(p => p.Id == productId);

        if (product == null)
            throw new Exception("Producto no existe");

        // 4️⃣ Stock actual
        var stock = _context.StockMovements
            .Where(m => m.ProductId == productId)
            .Sum(m => m.Type == "IN" ? m.Quantity : -m.Quantity);

        if (stock < quantity)
            throw new Exception("Stock insuficiente");

        // 5️⃣ Crear item
        var item = new SaleItem
        {
            Id = Guid.NewGuid(),
            SaleId = sale.Id,
            ProductId = productId,
            Quantity = quantity,
            UnitPrice = product.SalePrice
        };

        sale.Items.Add(item);
        _context.SaleItems.Add(item); // 🔥 CLAVE ABSOLUTA

        // 6️⃣ Stock OUT
        _context.StockMovements.Add(new StockMovement
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            Quantity = quantity,
            Type = "OUT",
            Reason = "Sale",
            Date = DateTime.UtcNow
        });

        // 7️⃣ Recalcular total
        sale.Total = sale.Items.Sum(i => i.Quantity * i.UnitPrice);

        // 🔥 FORZAR UPDATE DE SALE
        _context.Entry(sale).State = EntityState.Modified;

        _context.SaveChanges();

        return sale;
    }



}
