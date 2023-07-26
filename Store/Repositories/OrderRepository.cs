using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {

        public OrderRepository(RepositoryContext context) : base(context)
        {
            
        }

        public IQueryable<Order> Orders => _repositoryContext.Orders
            .Include(o=>o.Lines)
            .ThenInclude(c1=>c1.Product)
            .OrderBy(o=>o.Shipped)
            .ThenByDescending(o=>o.OrderId);
        public int NumberOfInProcess => _repositoryContext.Orders.Count(o=>o.Shipped.Equals(false));

        public void Complete(int id)
        {
           var order = FindByCondition(o=>o.OrderId.Equals(id),true);
           if (order is null)
           {
            throw new Exception("Order colud not found !");
           }
           else
           {
            order.Shipped=true;
           }
        }

        public Order? GetOneOrder(int id)
        {
            return FindByCondition(o=> o.OrderId.Equals(id),false);
        }

        public void SaveOrder(Order order)
        {
            _repositoryContext.AttachRange(order.Lines.Select(l=>l.Product));
            if (order.OrderId==0)
                _repositoryContext.Add(order);
            _repositoryContext.SaveChanges();
            
            
        }
    }
}