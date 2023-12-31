using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
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

        public IQueryable<Product> GetAllProductsWithDetails(ProductRequestParameters p)
        {
           return _manager.Product.GetAllProductsWithDetails(p);
        }

        public IEnumerable<Product> GetLastestProducts(int n, bool trackChanges)
        {
            return _manager.Product.FindAll(trackChanges).OrderByDescending(prd=>prd.ProductId).Take(n);
        }

        public Product? GetOneProduct(int id, bool trackChanges)
        {
            var product = _manager.Product.GetOneProduct(id, trackChanges);
            if (product is null)
                throw new Exception("PRODUCT NOT FOUND!");
            return product;
        }

        public ProductDtoForUpdate GetOneProductForUpdate(int id, bool trackChanges)
        {
           var product=GetOneProduct(id,trackChanges);
           var productDto= mapper.Map<ProductDtoForUpdate>(product);
           return productDto;

        }

        public IEnumerable<Product> GetShowcaseProducts(bool trackChanges)
        {
            return _manager.Product.GetShowcaseProducts(trackChanges);
        }

        public void UpdateOneProduct(ProductDtoForUpdate productDto)
        {
            var entity = mapper.Map<Product>(productDto);
            _manager.Product.UpdateOneProduct(entity);
            _manager.Save();

        }
    }
}