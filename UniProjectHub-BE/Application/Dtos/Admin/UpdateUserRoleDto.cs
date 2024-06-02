using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Admin
{
    public class UpdateUserRoleDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public List<string> Roles { get; set; }
    }
}
