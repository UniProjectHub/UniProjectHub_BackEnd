using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data
{
    public class RefreshToken
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
