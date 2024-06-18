using Application.InterfaceRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstructures.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext context) : base(context) { }

        public async Task<Payment> GetByOrderIdAsync(string orderId)
        {
            return await dbSet.FirstOrDefaultAsync(p => p.OrderId == orderId);
        }

        public System.Threading.Tasks.Task UpdateAsync(GroupChat groupChat)
        {
            throw new NotImplementedException();
        }
    }
}
