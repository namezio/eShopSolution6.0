using eShop.Application.Catalog.Product.Dtos;

namespace eShop.Application.Catalog.Product;

public interface IProductService
{
    int Create(ProductCreateRequest request);

    int Update(ProductEditRequest request);

    int Delete(int productId);

    Task<List<ProductViewModel>> GetAll();
    Task<List<ProductViewModel>> GetAllPaging(string keyword, int pageIndex, int pageSize);
    
}