using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.BlogModelView
{
    public class BlogModelView
    {
        public int Id { get; set; }
        public string? OwnerId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
        public int CategoryID { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class BlogCreateModel
    {
        [Required]
        public string? OwnerId { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; } = 1;
        public int CategoryID { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }

    public class BlogUpdateModel
    {
        public int Id { get; set; }
        [Required]
        public string? OwnerId { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; } 
        public int CategoryID { get; set; }
    }
}
