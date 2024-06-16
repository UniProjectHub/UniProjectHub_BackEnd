using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        [Required]
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } // e.g., CreditCard, PayPal, etc.
        public string Status { get; set; } // e.g., Completed, Pending, Failed
        [ForeignKey("UserId")]
        public Users User { get; set; }  // Reference to the User table
    }
}
