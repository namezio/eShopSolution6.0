namespace eShop.Application.Catalog.Product.Dtos;

public class ProductViewModel<T>
{
    public List<T> Items { get; set; }
    public  int TotalRecord { get; set; }
}