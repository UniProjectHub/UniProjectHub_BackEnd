using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.DashboardSummaryViewModel
{
    public class DashboardSummaryViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalProjects { get; set; }
        public int TotalBlogs { get; set; }
        public int TotalComments { get; set; }
        public int TotalFiles { get; set; }
        public int TotalTasks { get; set; }
        public int TotalNotifications { get; set; }
        public decimal TotalPayments { get; set; }
    }
}