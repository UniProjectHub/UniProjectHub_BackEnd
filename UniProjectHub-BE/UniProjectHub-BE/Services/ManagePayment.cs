using Application.InterfaceRepositories;
using Domain.Data;
using Microsoft.Extensions.Options;
using Net.payOS.Types;
using Net.payOS;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using System;
using System.Numerics;

namespace UniProjectHub_BE.Services
{
    public class ManagePayment
    {
        private readonly PayOS _payOS;
        private readonly PayOSSettings _payOSSettings;
        private readonly IPaymentRepository _paymentRepository;

        private static readonly Random random = new Random();
        private const string digits = "0123456789";

        public ManagePayment(IOptions<PayOSSettings> settings)
        {
            _payOSSettings = settings.Value;
            _payOS = new PayOS(_payOSSettings.ClientId, _payOSSettings.ApiKey, _payOSSettings.ChecksumKey);
        }

        private string ComputeHmacSha256(string data, string checksumKey)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(checksumKey)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        public async Task<CreatePaymentResult> CreatePaymentUrlRegisterCreator(Buyer buyer)
        {
            try
            {
                int amount = 49000;
                var orderCode = GenerateOrderCode();
                var description = "APN" + orderCode;
                var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(orderCode + ""));
                var returnUrl = _payOSSettings.ReturnUrl + "/payment-success/" + code;
                var returnUrlFail = _payOSSettings.ReturnUrl + "/payment-fail/" + code; 

                var signatureData = new Dictionary<string, object>
                {
                    { "amount", amount },
                    { "cancelUrl", returnUrlFail },
                    { "description", description },
                    { "expiredAt", DateTimeOffset.Now.ToUnixTimeSeconds() },
                    { "orderCode", orderCode },
                    { "returnUrl", returnUrl }
                };

                var sortedSignatureData = new SortedDictionary<string, object>(signatureData);

                var dataForSignature = string.Join("&", sortedSignatureData.Select(p => $"{p.Key}={p.Value}"));

                var signature = ComputeHmacSha256(dataForSignature, _payOSSettings.ChecksumKey);
                DateTimeOffset expiredAt = DateTimeOffset.Now.AddMinutes(10);

                var paymentData = new PaymentData(
                    orderCode: orderCode,
                    amount: amount,
                    description: description,
                    items: new List<ItemData>(),
                    cancelUrl: returnUrlFail,
                    returnUrl: returnUrl,
                    signature: signature,
                    buyerName: buyer.FirstName + buyer.LastName,
                    buyerPhone: buyer.PhoneNumber,
                    buyerEmail: buyer.Email,
                    buyerAddress: "",
                    expiredAt: (int)expiredAt.ToUnixTimeSeconds()
                );

                var createPaymentResult = await _payOS.createPaymentLink(paymentData);

                return createPaymentResult;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public static long GenerateOrderCode(int length = 12)
        {
            // Ensure the length does not exceed 19 to fit into a long
            if (length < 1 || length > 19)
            {
                throw new ArgumentOutOfRangeException(nameof(length), "Length must be between 1 and 19.");
            }

            var orderCode = new StringBuilder(length);

            // Ensure the first digit is not zero if the length is greater than 1
            orderCode.Append(digits[random.Next(1, digits.Length)]);

            for (int i = 1; i < length; i++)
            {
                orderCode.Append(digits[random.Next(digits.Length)]);
            }

            // Convert the generated string to a long
            return long.Parse(orderCode.ToString());
        }
    }
}
