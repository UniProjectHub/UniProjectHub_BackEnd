using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Account
{
    public class UpdateUserProfileDto
    {
        public string? Id { get; set; }
        public string? Username { get; set; }

        // Add other profile properties as needed
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsStudent { get; set; }
        public string? University { get; set; }
        public bool IsMale { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
