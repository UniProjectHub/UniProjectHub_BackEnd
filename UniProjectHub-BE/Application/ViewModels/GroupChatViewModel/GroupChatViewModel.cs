using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.GroupChatViewModel
{
    public class GroupChatViewModel
    {

        public int ProjectId { get; set; }
        public string MemberId { get; set; }
        public string Messenger { get; set; }
        public int Status { get; set; }
    }
}