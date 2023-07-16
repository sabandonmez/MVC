using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ProductManager : IProductService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper mapper;

        public ProductManager(IRepositoryManager manager ,IMapper _mapper)
        {
            _manager = manager;
            mapper = _mapper;
        }

        public void CreateProduct(ProductDtoForInsertion productDtoForInsertion)
        {
            Product product =mapper.Map<Product>(productDtoForInsertion);
            _manager.Product.Create(product);
            _manager.Save();
        }

        public void DeleteOneProduct(int id)
        {
            var product = GetOneProduct(id, false);
            if (product is not null)
            {
                _manager.Product.DeleteOneProduct(product);
                _manager.Save();
            }
        }

        public IEnumerable<Product> GetAllProducts(bool trackChanges)
        {
            return _manager.Product.GetAllProducts(trackChanges);
        }

        public Product? GetOneProduct(int id, bool trackChanges)
        {
            var product = _manager.Product.GetOneProduct(id, trackChanges);
            if (product is null)
                throw new Exception("PRODUCT NOT FOUND!");
            return product;
        }

        public void UpdateOneProduct(Product product)
        {
            _manager.Product.Update(product);
            _manager.Save();

        }
    }
}