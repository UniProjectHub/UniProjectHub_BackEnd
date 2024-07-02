using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? NameLeader {  get; set; }
        public string? TypeOfSpace { get; set; }
        public string? Img { get; set; }
        public int Status { get; set; }
        public bool IsGroup { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Member>? Members { get; set; }
        public ICollection<GroupChat>? GroupChats { get; set; }
        public ICollection<Task>? Tasks { get; set; }
    }
}
