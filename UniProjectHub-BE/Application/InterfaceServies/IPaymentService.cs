using Application.InterfaceRepositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceServies
{
    public interface IPaymentService
    {
        Task<Payment> ProcessPaymentAsync(Payment payment);
        Task<Payment> UpdatePaymentStatusAsync(string orderId, string status);
        Task<Payment> GetPaymentByOrderIdAsync(string transactionId);
        Task<List<Payment>> GetPaymentsByUserIdAsync(string userId);
    }
}
