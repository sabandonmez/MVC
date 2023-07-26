using Repositories.Contracts;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        private readonly IOrderRepository _orderRepository;

        public RepositoryManager(IProductRepository productRepository, RepositoryContext context , ICategoryRepository categoryRepository,IOrderRepository orderRepository)
        {
            this._productRepository = productRepository;
            this._context = context;
            this._categoryRepository = categoryRepository;
            this._orderRepository=orderRepository;
        }

        public IProductRepository Product => _productRepository;

        public ICategoryRepository Category => _categoryRepository;

        public IOrderRepository Order =>_orderRepository;
        
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}