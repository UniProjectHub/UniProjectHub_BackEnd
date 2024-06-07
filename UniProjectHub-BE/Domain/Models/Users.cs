using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Users : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsStudent { get; set; }
        public bool IsTeacher { get; set; }
        public string? University { get; set; }
        public bool IsMale { get; set; }
        public string? AvatarURL { get; set; }
        public int Status { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public ICollection<Schedule>? Schedules { get; set; }
        public ICollection<Blog>? Blogs { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
        public ICollection<GroupChat>? GroupChats { get; set; }
        public ICollection<Member>? Members { get; set; }
        public ICollection<MemberInTask>? MemberInTasks { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<File>? files { get; set; }

    }

}
