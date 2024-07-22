using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.MemberViewModel
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public int ProjectId { get; set; }
        public bool IsOwner { get; set; }
        public int Role { get; set; }
    }
}