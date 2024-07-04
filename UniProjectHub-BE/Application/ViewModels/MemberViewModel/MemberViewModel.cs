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
        //public Users UserId;
        public int ProjectId { get; set; }
        public string MemberId { get; set; }
        public bool IsOwner { get; set; }
        public int Role { get; set; }
    }
}