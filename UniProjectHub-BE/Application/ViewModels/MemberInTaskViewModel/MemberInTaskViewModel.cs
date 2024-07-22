using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.MemberInTaskViewModel
{
    public class MemberInTaskViewModel
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string? MemberId { get; set; }
        public DateTime JoinTime { get; set; }
    }
    public class MemberInTaskCreateModel
    {
        [Required]
        public int TaskId { get; set; }
        [Required]
        public string MemberId { get; set; }
        public DateTime JoinTime { get; set; } = TimeHelper.GetVietnamTime();
    }

    public class MemberInTaskUpdateModel
    {
        [Required]
        public int TaskId { get; set; }
        [Required]
        public string MemberId { get; set; }
    }
}
