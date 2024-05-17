using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Member
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string? MenberId { get; set; }
        public bool IsOwner { get; set; }
        public int Role {  get; set; }
        public DateTime JoinTime { get; set; }
        public DateTime LeftTime { get; set; }
        public Users? User { get; set; }
        public Project? Project { get; set; }
    }
}
