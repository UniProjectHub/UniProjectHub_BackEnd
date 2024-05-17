using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class GroupChat
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string? MemberId { get; set; }
        public string? Messenger {  get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public Users? User { get; set; }
        public Project? Project { get; set; }
    }
}
