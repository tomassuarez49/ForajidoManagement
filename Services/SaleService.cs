public class SaleService
{
    private static List<Sale> sales = new();
    private static int nextId = 1;

    private readonly StockMovementService _stockService = new();
    private readonly InventoryService _productService = new();


    public Sale Create(Sale sale)
    {
        sale.Id = nextId++;
        sale.Date = DateTime.UtcNow;

        decimal total = 0;

        foreach (var item in sale.Items)
        {
            var stock = _stockService.GetStock(item.ProductId);

            var product = _productService.GetById(item.ProductId);
            if (product == null)
                throw new Exception("Producto no existe");

            if (stock < item.Quantity)
                throw new Exception("Stock insuficiente");

            total += item.Quantity * product.SalePrice;

            _stockService.AddMovement(new StockMovement
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Type = "OUT",
                Reason = "Sale"
            });
        }

        sale.Total = total;
        sales.Add(sale);

        return sale;
    }

    public List<Sale> GetAll() => sales;
}
