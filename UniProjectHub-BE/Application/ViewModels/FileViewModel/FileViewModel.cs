using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Domain.Models.Task;

namespace Application.ViewModels.FileViewModel
{
    public class FileViewModel
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string? UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Filename { get; set; }
        public Users? Users { get; set; }
        public Task? Task { get; set; }
    }
}
