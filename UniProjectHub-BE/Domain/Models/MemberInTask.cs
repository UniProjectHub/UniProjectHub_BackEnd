using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class MemberInTask
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string? MemberId { get; set; }
        public DateTime JoinTime { get; set; }
        public Users? User { get; set; }
        public Task? Task { get; set; }
    }
}
