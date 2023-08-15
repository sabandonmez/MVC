namespace Services.Contracts
{
    public interface IServiceManager
    {
        IProductService ProductService {get;}
        ICategoryService CategoryService {get;}
        IAuthService AuthService {get;}

        IOrderService OrderService {get;}
    }
}