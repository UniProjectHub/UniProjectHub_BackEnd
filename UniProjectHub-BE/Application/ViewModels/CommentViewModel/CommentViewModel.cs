using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.CommentViewModel
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string? OwnerId { get; set; }
        public int BlogId { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
