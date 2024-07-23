using Application.ViewModels.DashboardSummaryViewModel;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceServies
{
    public interface IDashboardService
    {         
        Task<DashboardSummaryViewModel> GetDashboardSummaryAsync();
    }

}
