using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.SubTaskViewModel
{
    public class SubTaskViewModel
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int TaskId { get; set; }
        public string? Tags { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Deadline { get; set; }
    }
    public class CreateSubTaskRequest
    {
        public string Description { get; set; }
        [Required]
        public int TaskId { get; set; }
        public string? Tags { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Deadline { get; set; }
    }

    public class UpdateSubTaskRequest
    {
        public string Description { get; set; }
        public string? Tags { get; set; }

        public DateTime? Deadline { get; set; }
    }
}
