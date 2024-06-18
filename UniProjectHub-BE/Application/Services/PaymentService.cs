using Application.InterfaceRepositories;
using Application.InterfaceServies;
using Domain.Models;

namespace Infracstructures.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment> ProcessPaymentAsync(Payment payment)
        {
            payment.Status = "Pending";
            payment.PaymentDate = DateTime.UtcNow;

            await _paymentRepository.AddAsync(payment);
            return payment;
        }

        public async Task<Payment> UpdatePaymentStatusAsync(string orderId, string status)
        {
            var payment = await _paymentRepository.GetByOrderIdAsync(orderId);
            if (payment == null)
            {
                throw new Exception("Payment not found");
            }

            if (!payment.Status.Equals("Pending")) {
                throw new Exception("Payment has been processed");
            }

            payment.Status = status;
            _paymentRepository.Update(payment);
            return payment;
        }

        public async Task<Payment> GetPaymentByOrderIdAsync(string orderId)
        {
            return await _paymentRepository.GetByOrderIdAsync(orderId);
        }

        public async Task<List<Payment>> GetPaymentsByUserIdAsync(string userId)
        {
            return await _paymentRepository.GetAllAsync(p => p.Where(payment => payment.UserId == userId));
        }
    }
}
